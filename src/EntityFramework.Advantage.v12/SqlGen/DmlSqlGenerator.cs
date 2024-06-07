using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Metadata.Edm;
using System.Globalization;
using System.Text;

namespace Advantage.Data.Provider.SqlGen
{
    internal static class DmlSqlGenerator
    {
        private const int s_commandTextBuilderInitialCapacity = 256;

        internal static string GenerateUpdateSql(
            DbUpdateCommandTree tree,
            AdsProviderManifest providerManifest,
            AdsStoreVersion sqlVersion,
            out List<AdsParameter> parameters)
        {
            var commandText = new StringBuilder(256);
            var translator = new ExpressionTranslator(commandText,
                tree, null != tree.Returning, sqlVersion);
            commandText.Append("UPDATE ");
            tree.Target.Expression.Accept(translator);
            commandText.AppendLine();
            var flag = true;
            commandText.Append("SET ");
            foreach (DbSetClause setClause in tree.SetClauses)
            {
                if (flag)
                    flag = false;
                else
                    commandText.Append(", ");
                setClause.Property.Accept(translator);
                commandText.Append(" = ");
                setClause.Value.Accept(translator);
            }

            if (flag)
                throw new NotSupportedException(
                    "UPDATE statements without SET clauses are not supported by Advantage.");
            commandText.AppendLine();
            commandText.Append("WHERE ");
            tree.Predicate.Accept(translator);
            commandText.AppendLine(";");
            GenerateReturningSql(commandText, tree, translator,
                tree.Returning, providerManifest, sqlVersion);
            parameters = translator.Parameters;
            return commandText.ToString();
        }

        internal static string GenerateDeleteSql(
            DbDeleteCommandTree tree,
            AdsProviderManifest providerManifest,
            AdsStoreVersion sqlVersion,
            out List<AdsParameter> parameters)
        {
            var commandText = new StringBuilder(256);
            var expressionTranslator =
                new ExpressionTranslator(commandText, tree, false,
                    sqlVersion);
            commandText.Append("DELETE FROM ");
            tree.Target.Expression.Accept(expressionTranslator);
            commandText.AppendLine();
            commandText.Append("WHERE ");
            tree.Predicate.Accept(expressionTranslator);
            parameters = expressionTranslator.Parameters;
            return commandText.ToString();
        }

        internal static string GenerateInsertSql(
            DbInsertCommandTree tree,
            AdsProviderManifest providerManifest,
            AdsStoreVersion sqlVersion,
            out List<AdsParameter> parameters)
        {
            var commandText = new StringBuilder(256);
            var translator = new ExpressionTranslator(commandText,
                tree, null != tree.Returning, sqlVersion);
            commandText.Append("INSERT INTO ");
            tree.Target.Expression.Accept(translator);
            commandText.Append("(");
            var flag1 = true;
            foreach (DbSetClause setClause in tree.SetClauses)
            {
                if (flag1)
                    flag1 = false;
                else
                    commandText.Append(", ");
                setClause.Property.Accept(translator);
            }

            commandText.AppendLine(")");
            var flag2 = true;
            commandText.Append("VALUES (");
            foreach (DbSetClause setClause in tree.SetClauses)
            {
                if (flag2)
                    flag2 = false;
                else
                    commandText.Append(", ");
                setClause.Value.Accept(translator);
                translator.RegisterMemberValue(setClause.Property, setClause.Value);
            }

            commandText.AppendLine(");");
            GenerateReturningSql(commandText, tree, translator,
                tree.Returning, providerManifest, sqlVersion);
            parameters = translator.Parameters;
            return commandText.ToString();
        }

        private static string GenerateMemberSql(EdmMember member)
        {
            return SqlGenerator.QuoteIdentifier(member.Name);
        }

        private static void GenerateReturningSql(
            StringBuilder commandText,
            DbModificationCommandTree tree,
            ExpressionTranslator translator,
            DbExpression returning,
            AdsProviderManifest providerManifest,
            AdsStoreVersion sqlVersion)
        {
            if (returning == null)
                return;
            commandText.Append("SELECT ");
            returning.Accept(translator);
            commandText.AppendLine();
            commandText.Append("FROM ");
            tree.Target.Expression.Accept(translator);
            commandText.AppendLine();
            commandText.Append("WHERE ::stmt.UpdateCount > 0");
            var target = ((DbScanExpression)tree.Target.Expression).Target;
            var flag = false;
            foreach (var keyMember in target.ElementType.KeyMembers)
            {
                commandText.Append(" and ");
                commandText.Append(GenerateMemberSql(keyMember));
                commandText.Append(" = ");
                AdsParameter adsParameter;
                if (translator.MemberValues.TryGetValue(keyMember, out adsParameter))
                {
                    commandText.Append(":" + adsParameter.ParameterName);
                }
                else
                {
                    if (flag)
                        throw new NotSupportedException(string.Format(
                            "Server-generated keys are only supported for identity columns. More than one key column is marked as server generated in table '{0}'.",
                            target.Name));
                    commandText.Append("LASTAUTOINC( STATEMENT )");
                    flag = true;
                }
            }

            commandText.Append(";");
        }

        private static bool IsValidIdentityColumnType(TypeUsage typeUsage)
        {
            if (typeUsage.EdmType.BuiltInTypeKind != (BuiltInTypeKind)26)
                return false;
            switch (typeUsage.EdmType.Name)
            {
                case "tinyint":
                case "smallint":
                case "int":
                case "bigint":
                    return true;
                case "decimal":
                case "numeric":
                    Facet facet;
                    return typeUsage.Facets.TryGetValue("Scale", false, out facet) &&
                           Convert.ToInt32(facet.Value, CultureInfo.InvariantCulture) == 0;
                default:
                    return false;
            }
        }

        private class ExpressionTranslator : BasicExpressionVisitor
        {
            private readonly StringBuilder _commandText;
            private readonly DbModificationCommandTree _commandTree;
            private readonly List<AdsParameter> _parameters;
            private readonly Dictionary<EdmMember, AdsParameter> _memberValues;
            private readonly AdsStoreVersion _version;
            private int parameterNameCount;

            internal ExpressionTranslator(
                StringBuilder commandText,
                DbModificationCommandTree commandTree,
                bool preserveMemberValues,
                AdsStoreVersion version)
            {
                _commandText = commandText;
                _commandTree = commandTree;
                _version = version;
                _parameters = new List<AdsParameter>();
                _memberValues = preserveMemberValues
                    ? new Dictionary<EdmMember, AdsParameter>()
                    : null;
            }

            internal List<AdsParameter> Parameters => _parameters;

            internal Dictionary<EdmMember, AdsParameter> MemberValues => _memberValues;

            internal AdsParameter CreateParameter(DbType type, ParameterDirection direction)
            {
                var parameter = new AdsParameter();
                parameter.ParameterName = NextName();
                parameter.Direction = direction;
                _parameters.Add(parameter);
                return parameter;
            }

            internal AdsParameter CreateParameter(object value, TypeUsage type)
            {
                var adsParameter =
                    AdsProviderServices.CreateAdsParameter(NextName(), type, 0, value);
                _parameters.Add(adsParameter);
                return adsParameter;
            }

            private string NextName()
            {
                var str = ":p" + parameterNameCount.ToString(CultureInfo.InvariantCulture);
                ++parameterNameCount;
                return str;
            }

            public override void Visit(DbAndExpression expression)
            {
                VisitBinary(expression, " and ");
            }

            public override void Visit(DbOrExpression expression)
            {
                VisitBinary(expression, " or ");
            }

            public override void Visit(DbComparisonExpression expression)
            {
                VisitBinary(expression, " = ");
                RegisterMemberValue(expression.Left, expression.Right);
            }

            internal void RegisterMemberValue(DbExpression propertyExpression, DbExpression value)
            {
                if (_memberValues == null)
                    return;
                var property = ((DbPropertyExpression)propertyExpression).Property;
                if (value.ExpressionKind == (DbExpressionKind)38)
                    return;
                _memberValues[property] = _parameters[_parameters.Count - 1];
            }

            public override void Visit(DbIsNullExpression expression)
            {
                expression.Argument.Accept(this);
                _commandText.Append(" is null");
            }

            public override void Visit(DbNotExpression expression)
            {
                _commandText.Append("not (");
                expression.Accept(this);
                _commandText.Append(")");
            }

            public override void Visit(DbConstantExpression expression)
            {
                _commandText.Append(":" +
                                         CreateParameter(expression.Value,
                                             expression.ResultType).ParameterName);
            }

            public override void Visit(DbScanExpression expression)
            {
                if (MetadataHelpers.GetMetadataProperty<string>(expression.Target, "DefiningQuery") !=
                    null)
                {
                    var str = !(_commandTree is DbDeleteCommandTree)
                        ? (!(_commandTree is DbInsertCommandTree) ? "UpdateFunction" : "InsertFunction")
                        : "DeleteFunction";
                    throw new InvalidOperationException(string.Format(
                        "Unable to update the EntitySet '{0}' because it has a DefiningQuery and no <{1}> element exists in the <{2}> element to support the current operation.",
                        expression.Target.Name, str, "ModificationFunctionMapping"));
                }

                _commandText.Append(SqlGenerator.GetTargetTSql(expression.Target));
            }

            public override void Visit(DbPropertyExpression expression)
            {
                _commandText.Append(GenerateMemberSql(expression.Property));
            }

            public override void Visit(DbNullExpression expression) => _commandText.Append("null");

            public override void Visit(DbNewInstanceExpression expression)
            {
                var flag = true;
                foreach (var dbExpression in expression.Arguments)
                {
                    if (flag)
                        flag = false;
                    else
                        _commandText.Append(", ");
                    dbExpression.Accept(this);
                }
            }

            private void VisitBinary(DbBinaryExpression expression, string separator)
            {
                _commandText.Append("(");
                expression.Left.Accept(this);
                _commandText.Append(separator);
                expression.Right.Accept(this);
                _commandText.Append(")");
            }
        }
    }
}