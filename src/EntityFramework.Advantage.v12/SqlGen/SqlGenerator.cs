using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Metadata.Edm;
using System.Globalization;
using System.Text;

namespace Advantage.Data.Provider.SqlGen
{
    internal sealed class SqlGenerator : DbExpressionVisitor<ISqlFragment>
    {
        private const byte defaultDecimalPrecision = 18;
        private Stack<SqlSelectStatement> selectStatementStack;
        private Stack<bool> isParentAJoinStack;
        private Dictionary<string, int> allExtentNames;
        private Dictionary<string, int> allColumnNames;
        private SymbolTable symbolTable = new SymbolTable();
        private bool isVarRefSingle;

        private static readonly Dictionary<string, FunctionHandler> _canonicalFunctionHandlers =
            InitializeCanonicalFunctionHandlers();

        private static readonly char[] hexDigits = new char[16]
        {
            '0',
            '1',
            '2',
            '3',
            '4',
            '5',
            '6',
            '7',
            '8',
            '9',
            'A',
            'B',
            'C',
            'D',
            'E',
            'F'
        };

        private AdsStoreVersion _sqlVersion;
        private AdsProviderManifest _providerManifest;

        private SqlSelectStatement CurrentSelectStatement => selectStatementStack.Peek();

        private bool IsParentAJoin
        {
            get => isParentAJoinStack.Count != 0 && isParentAJoinStack.Peek();
        }

        internal Dictionary<string, int> AllExtentNames => allExtentNames;

        internal Dictionary<string, int> AllColumnNames => allColumnNames;

        private static Dictionary<string, FunctionHandler> InitializeCanonicalFunctionHandlers()
        {
            return new Dictionary<string, FunctionHandler>(16,
                StringComparer.Ordinal)
            {
                {
                    "Left",
                    HandleCanonicalFunctionLeft
                },
                {
                    "Right",
                    HandleCanonicalFunctionRight
                },
                {
                    "IndexOf",
                    HandleCanonicalFunctionIndexOf
                },
                {
                    "Substring",
                    HandleCanonicalFunctionSubstring
                },
                {
                    "Length",
                    HandleCanonicalFunctionLength
                },
                {
                    "Round",
                    HandleCanonicalFunctionRound
                },
                {
                    "ToLower",
                    HandleCanonicalFunctionToLower
                },
                {
                    "ToUpper",
                    HandleCanonicalFunctionToUpper
                },
                {
                    "Ceiling",
                    HandleCanonicalFunctionCeiling
                },
                {
                    "Trim",
                    HandleCanonicalFunctionTrim
                },
                {
                    "Year",
                    HandleCanonicalFunctionDatepart
                },
                {
                    "Month",
                    HandleCanonicalFunctionDatepart
                },
                {
                    "Day",
                    HandleCanonicalFunctionDatepart
                },
                {
                    "Hour",
                    HandleCanonicalFunctionDatepart
                },
                {
                    "Minute",
                    HandleCanonicalFunctionDatepart
                },
                {
                    "Second",
                    HandleCanonicalFunctionDatepart
                },
                {
                    "Millisecond",
                    HandleCanonicalFunctionDatepart
                },
                {
                    "CurrentDateTime",
                    HandleCanonicalFunctionCurrentDateTime
                },
                {
                    "Concat",
                    HandleConcatFunction
                },
                {
                    "BitwiseAnd",
                    HandleCanonicalFunctionBitwise
                },
                {
                    "BitwiseNot",
                    HandleCanonicalFunctionBitwise
                },
                {
                    "BitwiseOr",
                    HandleCanonicalFunctionBitwise
                },
                {
                    "BitwiseXor",
                    HandleCanonicalFunctionBitwise
                },
                {
                    "Reverse",
                    HandleCanonicalFunctionReverse
                },
                {
                    "CurrentUtcDateTime",
                    HandleCanonicalFunctionCurrentDateTime
                },
                {
                    "CurrentDateTimeOffset",
                    HandleCanonicalFunctionCurrentDateTime
                },
                {
                    "GetTotalOffsetMinutes",
                    HandleCanonicalFunctionGetTotalOffsetMinutes
                },
                {
                    "NewGuid",
                    HandleCanonicalFunctionNewGuid
                },
                {
                    "CreateDateTime",
                    HandleCanonicalFunctionCreateDateTime
                },
                {
                    "CreateDateTimeOffset",
                    HandleUnsupportedCanonicalFunction
                },
                {
                    "CreateTime",
                    HandleUnsupportedCanonicalFunction
                },
                {
                    "TruncateTime",
                    HandleUnsupportedCanonicalFunction
                },
                {
                    "AddYears",
                    HandleCanonicalFunctionTimestampAdd
                },
                {
                    "AddMonths",
                    HandleCanonicalFunctionTimestampAdd
                },
                {
                    "AddDays",
                    HandleCanonicalFunctionTimestampAdd
                },
                {
                    "AddHours",
                    HandleCanonicalFunctionTimestampAdd
                },
                {
                    "AddMinutes",
                    HandleCanonicalFunctionTimestampAdd
                },
                {
                    "AddSeconds",
                    HandleCanonicalFunctionTimestampAdd
                },
                {
                    "AddMilliseconds",
                    HandleCanonicalFunctionTimestampAdd
                },
                {
                    "AddMicroseconds",
                    HandleCanonicalFunctionTimestampAdd
                },
                {
                    "AddNanoseconds",
                    HandleCanonicalFunctionTimestampAdd
                },
                {
                    "DiffYears",
                    HandleCanonicalFunctionTimestampDiff
                },
                {
                    "DiffMonths",
                    HandleCanonicalFunctionTimestampDiff
                },
                {
                    "DiffDays",
                    HandleCanonicalFunctionTimestampDiff
                },
                {
                    "DiffHours",
                    HandleCanonicalFunctionTimestampDiff
                },
                {
                    "DiffMinutes",
                    HandleCanonicalFunctionTimestampDiff
                },
                {
                    "DiffSeconds",
                    HandleCanonicalFunctionTimestampDiff
                },
                {
                    "DiffMilliseconds",
                    HandleCanonicalFunctionTimestampDiff
                },
                {
                    "DiffMicroseconds",
                    HandleCanonicalFunctionTimestampDiff
                },
                {
                    "DiffNanoseconds",
                    HandleCanonicalFunctionTimestampDiff
                },
                {
                    "StartsWith",
                    HandleCanonicalFunctionStartsWith
                },
                {
                    "EndsWith",
                    HandleCanonicalFunctionEndsWith
                },
                {
                    "Contains",
                    HandleCanonicalFunctionContains
                }
            };
        }

        private SqlGenerator(AdsProviderManifest providerManifest, AdsStoreVersion sqlVersion)
        {
            _providerManifest = providerManifest;
            _sqlVersion = sqlVersion;
        }

        internal static string GenerateSql(
            DbCommandTree tree,
            AdsProviderManifest providerManifest,
            AdsStoreVersion sqlVersion,
            out List<AdsParameter> parameters,
            out CommandType commandType)
        {
            SqlGenerator sqlGenerator = null;
            commandType = CommandType.Text;
            parameters = null;
            switch (tree)
            {
                case DbQueryCommandTree _:
                    return new SqlGenerator(providerManifest, sqlVersion).GenerateSql((DbQueryCommandTree)tree);
                case DbInsertCommandTree _:
                    return DmlSqlGenerator.GenerateInsertSql((DbInsertCommandTree)tree, providerManifest, sqlVersion,
                        out parameters);
                case DbDeleteCommandTree _:
                    return DmlSqlGenerator.GenerateDeleteSql((DbDeleteCommandTree)tree, providerManifest, sqlVersion,
                        out parameters);
                case DbUpdateCommandTree _:
                    return DmlSqlGenerator.GenerateUpdateSql((DbUpdateCommandTree)tree, providerManifest, sqlVersion,
                        out parameters);
                case DbFunctionCommandTree _:
                    sqlGenerator = new SqlGenerator(providerManifest, sqlVersion);
                    return GenerateFunctionSql((DbFunctionCommandTree)tree, out commandType,
                        out parameters);
                default:
                    parameters = null;
                    return null;
            }
        }

        private static string GenerateFunctionSql(
            DbFunctionCommandTree tree,
            out CommandType commandType,
            out List<AdsParameter> parameters)
        {
            var edmFunction = tree.EdmFunction;
            parameters = null;
            var metadataProperty1 =
                MetadataHelpers.GetMetadataProperty<string>(edmFunction, "CommandTextAttribute");
            var metadataProperty2 = MetadataHelpers.GetMetadataProperty<string>(edmFunction, "Schema");
            var metadataProperty3 =
                MetadataHelpers.GetMetadataProperty<string>(edmFunction, "StoreFunctionNameAttribute");
            var metadataProperty4 = MetadataHelpers.GetMetadataProperty<string>(edmFunction,
                "EFOracleProviderExtensions:CursorParameterName");
            if (!string.IsNullOrEmpty(metadataProperty4))
            {
                parameters = new List<AdsParameter>();
                var adsParameter = new AdsParameter();
                adsParameter.ParameterName = metadataProperty4;
                adsParameter.Direction = ParameterDirection.Output;
                parameters.Add(adsParameter);
            }

            if (string.IsNullOrEmpty(metadataProperty1))
            {
                commandType = CommandType.StoredProcedure;
                var str = QuoteIdentifier(string.IsNullOrEmpty(metadataProperty3)
                    ? edmFunction.Name
                    : metadataProperty3);
                return !string.IsNullOrEmpty(metadataProperty2)
                    ? QuoteIdentifier(metadataProperty2) + "." + str
                    : str;
            }

            commandType = CommandType.Text;
            return metadataProperty1;
        }

        private string GenerateSql(DbQueryCommandTree tree)
        {
            var queryCommandTree = tree;
            selectStatementStack = new Stack<SqlSelectStatement>();
            isParentAJoinStack = new Stack<bool>();
            allExtentNames =
                new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            allColumnNames =
                new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            ISqlFragment sqlStatement;
            if (MetadataHelpers.IsCollectionType(queryCommandTree.Query.ResultType.EdmType))
            {
                var sqlSelectStatement = VisitExpressionEnsureSqlStatement(queryCommandTree.Query);
                sqlSelectStatement.IsTopMost = true;
                sqlStatement = sqlSelectStatement;
            }
            else
            {
                var sqlBuilder = new SqlBuilder();
                sqlBuilder.Append("SELECT ");
                sqlBuilder.Append(
                    queryCommandTree.Query.Accept(this));
                sqlStatement = sqlBuilder;
            }

            if (isVarRefSingle)
                throw new NotSupportedException();
            return WriteSql(sqlStatement);
        }

        private string WriteSql(ISqlFragment sqlStatement)
        {
            var b = new StringBuilder(1024);
            using (var writer = new SqlWriter(b))
                sqlStatement.WriteSql(writer, this);
            return b.ToString();
        }

        public override ISqlFragment Visit(DbAndExpression e)
        {
            return VisitBinaryExpression(" AND ", (DbExpressionKind)1, e.Left,
                e.Right);
        }

        public override ISqlFragment Visit(DbApplyExpression e)
        {
            throw new NotSupportedException("APPLY operator is not supported by Advantage.");
        }

        public override ISqlFragment Visit(DbArithmeticExpression e)
        {
            var expressionKind = e.ExpressionKind;
            SqlBuilder sqlBuilder;
            if (expressionKind <= (DbExpressionKind)34)
            {
                if (expressionKind != (DbExpressionKind)10)
                {
                    switch ((int)expressionKind - 32)
                    {
                        case 0:
                            sqlBuilder = VisitBinaryExpression(" - ", e.ExpressionKind,
                                e.Arguments[0], e.Arguments[1]);
                            goto label_12;
                        case 1:
                            sqlBuilder = new SqlBuilder();
                            sqlBuilder.Append("MOD(");
                            sqlBuilder.Append(e.Arguments[0]
                                .Accept(this));
                            sqlBuilder.Append(",");
                            sqlBuilder.Append(e.Arguments[1]
                                .Accept(this));
                            sqlBuilder.Append(")");
                            goto label_12;
                        case 2:
                            sqlBuilder = VisitBinaryExpression(" * ", e.ExpressionKind,
                                e.Arguments[0], e.Arguments[1]);
                            goto label_12;
                    }
                }
                else
                {
                    sqlBuilder = VisitBinaryExpression(" / ", e.ExpressionKind, e.Arguments[0],
                        e.Arguments[1]);
                    goto label_12;
                }
            }
            else if (expressionKind != (DbExpressionKind)44)
            {
                if (expressionKind == (DbExpressionKind)54)
                {
                    sqlBuilder = new SqlBuilder();
                    sqlBuilder.Append(" -(");
                    sqlBuilder.Append(e.Arguments[0]
                        .Accept(this));
                    sqlBuilder.Append(")");
                    goto label_12;
                }
            }
            else
            {
                sqlBuilder = VisitBinaryExpression(" + ", e.ExpressionKind, e.Arguments[0],
                    e.Arguments[1]);
                goto label_12;
            }

            throw new InvalidOperationException(string.Empty);
            label_12:
            return sqlBuilder;
        }

        public override ISqlFragment Visit(DbCaseExpression e)
        {
            var sqlBuilder = new SqlBuilder();
            sqlBuilder.Append("CASE");
            for (var index = 0; index < e.When.Count; ++index)
            {
                sqlBuilder.Append(" WHEN (");
                sqlBuilder.Append(e.When[index].Accept(this));
                sqlBuilder.Append(") THEN ");
                sqlBuilder.Append(e.Then[index].Accept(this));
            }

            if (e.Else != null && !(e.Else is DbNullExpression))
            {
                sqlBuilder.Append(" ELSE ");
                sqlBuilder.Append(e.Else.Accept(this));
            }

            sqlBuilder.Append(" END");
            return sqlBuilder;
        }

        public override ISqlFragment Visit(DbCastExpression e)
        {
            var sqlBuilder = new SqlBuilder();
            GetSqlPrimitiveType(e.ResultType).ToLowerInvariant();
            sqlBuilder.Append(
                e.Argument.Accept(this));
            return sqlBuilder;
        }

        public override ISqlFragment Visit(DbComparisonExpression e)
        {
            var expressionKind = e.ExpressionKind;
            if (expressionKind <= (DbExpressionKind)19)
            {
                if (expressionKind == (DbExpressionKind)13)
                    return VisitComparisonExpression(" = ", e.Left,
                        e.Right);
                switch ((int)expressionKind - 18)
                {
                    case 0:
                        return VisitComparisonExpression(" > ", e.Left,
                            e.Right);
                    case 1:
                        return VisitComparisonExpression(" >= ", e.Left,
                            e.Right);
                }
            }
            else
            {
                switch ((int)expressionKind - 28)
                {
                    case 0:
                        return VisitComparisonExpression(" < ", e.Left,
                            e.Right);
                    case 1:
                        return VisitComparisonExpression(" <= ", e.Left,
                            e.Right);
                    default:
                        if (expressionKind == (DbExpressionKind)37)
                            return VisitComparisonExpression(" <> ", e.Left,
                                e.Right);
                        break;
                }
            }

            throw new InvalidOperationException(string.Empty);
        }

        private ISqlFragment VisitConstant(DbConstantExpression e, bool isCastOptional)
        {
            var result = new SqlBuilder();
            PrimitiveTypeKind typeKind;
            if (!MetadataHelpers.TryGetPrimitiveTypeKind(e.ResultType, out typeKind))
                throw new NotSupportedException();
            switch ((int)typeKind)
            {
                case 0:
                    result.Append("X'");
                    result.Append(ByteArrayToBinaryString((byte[])e.Value));
                    result.Append("'");
                    break;
                case 1:
                    result.Append(e.Value.ToString());
                    break;
                case 2:
                    WrapWithCastIfNeeded(!isCastOptional, e.Value.ToString(),
                        ConvertPrimitiveTypetoCastType((PrimitiveTypeKind)2, 0, 0), result);
                    break;
                case 3:
                    WrapWithCastIfNeeded(true,
                        EscapeSingleQuote(
                            ((DateTime)e.Value).ToString("yyyy-MM-dd HH:mm:ss.fff",
                                CultureInfo.InvariantCulture), false),
                        ConvertPrimitiveTypetoCastType((PrimitiveTypeKind)3, 0, 0), result);
                    break;
                case 4:
                    var str = ((Decimal)e.Value).ToString(CultureInfo.InvariantCulture);
                    if (-1 == str.IndexOf('.'))
                    {
                        var length = str.TrimStart('-').Length;
                    }

                    var num = Math.Max((byte)str.Length, (byte)18);
                    var typeName = "SQL_NUMERIC(" +
                                   str.Length.ToString(CultureInfo.InvariantCulture) + ", " +
                                   num.ToString(CultureInfo.InvariantCulture) + ")";
                    WrapWithCastIfNeeded(!isCastOptional, str, typeName, result);
                    break;
                case 5:
                    WrapWithCastIfNeeded(!isCastOptional,
                        ((double)e.Value).ToString("R", CultureInfo.InvariantCulture),
                        ConvertPrimitiveTypetoCastType((PrimitiveTypeKind)5, 0, 0), result);
                    break;
                case 6:
                    WrapWithCastIfNeeded(true, EscapeSingleQuote(e.Value.ToString(), false),
                        "uniqueidentifier", result);
                    break;
                case 7:
                    WrapWithCastIfNeeded(!isCastOptional,
                        ((float)e.Value).ToString("R", CultureInfo.InvariantCulture),
                        ConvertPrimitiveTypetoCastType((PrimitiveTypeKind)7, 0, 0), result);
                    break;
                case 9:
                    WrapWithCastIfNeeded(!isCastOptional, e.Value.ToString(),
                        ConvertPrimitiveTypetoCastType((PrimitiveTypeKind)9, 0, 0), result);
                    break;
                case 10:
                    result.Append(e.Value.ToString());
                    break;
                case 11:
                    WrapWithCastIfNeeded(!isCastOptional, e.Value.ToString(),
                        ConvertPrimitiveTypetoCastType((PrimitiveTypeKind)11, 0, 0), result);
                    break;
                case 12:
                    bool isUnicode;
                    MetadataHelpers.TryGetIsUnicode(e.ResultType, out isUnicode);
                    result.Append(EscapeSingleQuote(e.Value as string, isUnicode));
                    break;
                case 13:
                    WrapWithCastIfNeeded(true,
                        EscapeSingleQuote(((TimeSpan)e.Value).ToString(), false),
                        ConvertPrimitiveTypetoCastType((PrimitiveTypeKind)13, 0, 0), result);
                    break;
                case 14:
                    throw new NotSupportedException("DateTimeOffset literals are not supported by Advantage");
                default:
                    throw new NotSupportedException();
            }

            return result;
        }

        private static void WrapWithCastIfNeeded(
            bool cast,
            string value,
            string typeName,
            SqlBuilder result)
        {
            if (!cast)
            {
                result.Append(value);
            }
            else
            {
                result.Append("CAST(");
                result.Append(value);
                result.Append(" AS ");
                result.Append(typeName);
                result.Append(")");
            }
        }

        private string ConvertPrimitiveTypetoCastType(
            PrimitiveTypeKind typeKind,
            int Length,
            int Precision)
        {
            switch ((int)typeKind)
            {
                case 0:
                    return "SQL_BINARY( " + Length.ToString(CultureInfo.InvariantCulture) + " ) ";
                case 1:
                    return "SQL_BIT";
                case 2:
                case 10:
                    return "SQL_INTEGER";
                case 3:
                    return "SQL_TIMESTAMP";
                case 4:
                    return "SQL_NUMERIC( " + Length.ToString(CultureInfo.InvariantCulture) + ", " +
                           Precision.ToString(CultureInfo.InvariantCulture) + ")";
                case 5:
                case 7:
                    return "SQL_DOUBLE";
                case 6:
                    return "SQL_BINARY( 16 )";
                case 9:
                    return "SQL_NUMERIC( 5, 0 )";
                case 11:
                    return "SQL_NUMERIC( 20, 0 )";
                case 12:
                    return "SQL_CHAR( " + Length.ToString(CultureInfo.InvariantCulture) + " )";
                case 13:
                    return "SQL_TIME";
                case 14:
                    throw new NotSupportedException("DateTimeOffsets are not supported by Advantage");
                default:
                    throw new NotSupportedException();
            }
        }

        public override ISqlFragment Visit(DbConstantExpression e) => VisitConstant(e, false);

        public override ISqlFragment Visit(DbDerefExpression e) => throw new NotSupportedException();

        public override ISqlFragment Visit(DbDistinctExpression e)
        {
            var sqlSelectStatement =
                VisitExpressionEnsureSqlStatement(e.Argument);
            if (!IsCompatible(sqlSelectStatement, e.ExpressionKind))
            {
                var elementTypeUsage =
                    MetadataHelpers.GetElementTypeUsage(e.Argument.ResultType);
                Symbol fromSymbol;
                sqlSelectStatement =
                    CreateNewSelectStatement(sqlSelectStatement, "distinct", elementTypeUsage, out fromSymbol);
                AddFromSymbol(sqlSelectStatement, "distinct", fromSymbol, false);
            }

            sqlSelectStatement.IsDistinct = true;
            return sqlSelectStatement;
        }

        public override ISqlFragment Visit(DbElementExpression e)
        {
            var sqlBuilder = new SqlBuilder();
            sqlBuilder.Append("(");
            sqlBuilder.Append(VisitExpressionEnsureSqlStatement(e.Argument));
            sqlBuilder.Append(")");
            return sqlBuilder;
        }

        public override ISqlFragment Visit(DbExceptExpression e)
        {
            throw new NotSupportedException("MINUS operator is not supported by Advantage.");
        }

        public override ISqlFragment Visit(DbExpression e)
        {
            throw new InvalidOperationException(string.Empty);
        }

        public override ISqlFragment Visit(DbScanExpression e)
        {
            var target = e.Target;
            if (IsParentAJoin)
            {
                var sqlBuilder = new SqlBuilder();
                sqlBuilder.Append(GetTargetTSql(target));
                return sqlBuilder;
            }

            var sqlSelectStatement = new SqlSelectStatement();
            sqlSelectStatement.From.Append(GetTargetTSql(target));
            return sqlSelectStatement;
        }

        internal static string GetTargetTSql(EntitySetBase entitySetBase)
        {
            var metadataProperty1 =
                MetadataHelpers.GetMetadataProperty<string>(entitySetBase, "DefiningQuery");
            if (metadataProperty1 != null)
                return "(" + metadataProperty1 + ")";
            var metadataProperty2 =
                MetadataHelpers.GetMetadataProperty<string>(entitySetBase, "Schema");
            var stringBuilder = new StringBuilder(50);
            if (!string.IsNullOrEmpty(metadataProperty2))
            {
                stringBuilder.Append(QuoteIdentifier(metadataProperty2));
                stringBuilder.Append(".");
            }

            var metadataProperty3 =
                MetadataHelpers.GetMetadataProperty<string>(entitySetBase, "Table");
            if (!string.IsNullOrEmpty(metadataProperty3))
                stringBuilder.Append(QuoteIdentifier(metadataProperty3));
            else
                stringBuilder.Append(QuoteIdentifier(entitySetBase.Name));
            return stringBuilder.ToString();
        }

        public override ISqlFragment Visit(DbFilterExpression e)
        {
            return VisitFilterExpression(e.Input, e.Predicate, false);
        }

        public override ISqlFragment Visit(DbFunctionExpression e)
        {
            return IsSpecialCanonicalFunction(e)
                ? HandleSpecialCanonicalFunction(e)
                : HandleFunctionDefault(e);
        }

        public override ISqlFragment Visit(DbEntityRefExpression e) => throw new NotSupportedException();

        public override ISqlFragment Visit(DbRefKeyExpression e) => throw new NotSupportedException();

        public override ISqlFragment Visit(DbGroupByExpression e)
        {
            Symbol fromSymbol;
            var sqlSelectStatement = VisitInputExpression(e.Input.Expression, e.Input.VariableName,
                e.Input.VariableType, out fromSymbol);
            if (!IsCompatible(sqlSelectStatement, e.ExpressionKind))
                sqlSelectStatement = CreateNewSelectStatement(sqlSelectStatement, e.Input.VariableName,
                    e.Input.VariableType, out fromSymbol);
            selectStatementStack.Push(sqlSelectStatement);
            symbolTable.EnterScope();
            AddFromSymbol(sqlSelectStatement, e.Input.VariableName, fromSymbol);
            symbolTable.Add(e.Input.GroupVariableName, fromSymbol);
            var edmType =
                MetadataHelpers.GetEdmType<RowType>(MetadataHelpers
                    .GetEdmType<CollectionType>(e.ResultType).TypeUsage);
            var flag = GroupByAggregatesNeedInnerQuery(e.Aggregates) ||
                       GroupByKeysNeedInnerQuery(e.Keys, e.Input.VariableName);
            SqlSelectStatement selectStatement;
            if (flag)
            {
                selectStatement = CreateNewSelectStatement(sqlSelectStatement, e.Input.VariableName,
                    e.Input.VariableType, false, out fromSymbol);
                AddFromSymbol(selectStatement, e.Input.VariableName, fromSymbol, false);
            }
            else
                selectStatement = sqlSelectStatement;

            using (IEnumerator<EdmProperty> enumerator = edmType.Properties.GetEnumerator())
            {
                enumerator.MoveNext();
                var s1 = "";
                foreach (var key in e.Keys)
                {
                    var s2 = QuoteIdentifier(enumerator.Current.Name);
                    selectStatement.GroupBy.Append(s1);
                    var s3 = key.Accept(this);
                    if (!flag)
                    {
                        selectStatement.Select.Append(s1);
                        selectStatement.Select.AppendLine();
                        selectStatement.Select.Append(s3);
                        selectStatement.Select.Append(" AS ");
                        selectStatement.Select.Append(s2);
                        selectStatement.GroupBy.Append(s3);
                    }
                    else
                    {
                        sqlSelectStatement.Select.Append(s1);
                        sqlSelectStatement.Select.AppendLine();
                        sqlSelectStatement.Select.Append(s3);
                        sqlSelectStatement.Select.Append(" AS ");
                        sqlSelectStatement.Select.Append(s2);
                        selectStatement.Select.Append(s1);
                        selectStatement.Select.AppendLine();
                        selectStatement.Select.Append(fromSymbol);
                        selectStatement.Select.Append(".");
                        selectStatement.Select.Append(s2);
                        selectStatement.Select.Append(" AS ");
                        selectStatement.Select.Append(s2);
                        selectStatement.GroupBy.Append(s2);
                    }

                    s1 = ", ";
                    enumerator.MoveNext();
                }

                foreach (var aggregate in e.Aggregates)
                {
                    var s4 = QuoteIdentifier(enumerator.Current.Name);
                    var s5 = aggregate.Arguments[0]
                        .Accept(this);
                    object aggregateArgument;
                    if (flag)
                    {
                        var sqlBuilder = new SqlBuilder();
                        sqlBuilder.Append(fromSymbol);
                        sqlBuilder.Append(".");
                        sqlBuilder.Append(s4);
                        aggregateArgument = sqlBuilder;
                        sqlSelectStatement.Select.Append(s1);
                        sqlSelectStatement.Select.AppendLine();
                        sqlSelectStatement.Select.Append(s5);
                        sqlSelectStatement.Select.Append(" AS ");
                        sqlSelectStatement.Select.Append(s4);
                    }
                    else
                        aggregateArgument = s5;

                    ISqlFragment s6 = VisitAggregate(aggregate, aggregateArgument);
                    selectStatement.Select.Append(s1);
                    selectStatement.Select.AppendLine();
                    selectStatement.Select.Append(s6);
                    selectStatement.Select.Append(" AS ");
                    selectStatement.Select.Append(s4);
                    s1 = ", ";
                    enumerator.MoveNext();
                }
            }

            symbolTable.ExitScope();
            selectStatementStack.Pop();
            return selectStatement;
        }

        public override ISqlFragment Visit(DbInExpression e)
        {
            if (e.List.Count == 0)
                return Visit(DbExpressionBuilder.False);
            var sqlBuilder = new SqlBuilder();
            sqlBuilder.Append(e.Item.Accept(this));
            sqlBuilder.Append(" IN (");
            var flag = true;
            foreach (var dbExpression in e.List)
            {
                if (flag)
                    flag = false;
                else
                    sqlBuilder.Append(", ");
                sqlBuilder.Append(dbExpression.Accept(this));
            }

            sqlBuilder.Append(")");
            return sqlBuilder;
        }

        public override ISqlFragment Visit(DbIntersectExpression e)
        {
            throw new NotSupportedException("INTERSECT operator is not supported by Advantage.");
        }

        public override ISqlFragment Visit(DbIsEmptyExpression e)
        {
            return VisitIsEmptyExpression(e, false);
        }

        public override ISqlFragment Visit(DbIsNullExpression e)
        {
            return VisitIsNullExpression(e, false);
        }

        public override ISqlFragment Visit(DbIsOfExpression e) => throw new NotSupportedException();

        public override ISqlFragment Visit(DbCrossJoinExpression e) => throw new NotSupportedException();

        public override ISqlFragment Visit(DbJoinExpression e)
        {
            var expressionKind = e.ExpressionKind;
            var joinString = expressionKind == (DbExpressionKind)16
                ? "FULL OUTER JOIN"
                : (expressionKind == (DbExpressionKind)21
                    ? "INNER JOIN"
                    : (expressionKind == (DbExpressionKind)27 ? "LEFT OUTER JOIN" : null));
            return VisitJoinExpression(new List<DbExpressionBinding>(2)
            {
                e.Left,
                e.Right
            }, e.ExpressionKind, joinString, e.JoinCondition);
        }

        public override ISqlFragment Visit(DbLikeExpression e)
        {
            var sqlBuilder = new SqlBuilder();
            sqlBuilder.Append(e.Argument.Accept(this));
            sqlBuilder.Append(" LIKE ");
            sqlBuilder.Append(e.Pattern.Accept(this));
            if (e.Escape.ExpressionKind != (DbExpressionKind)38)
            {
                sqlBuilder.Append(" ESCAPE ");
                sqlBuilder.Append(e.Escape.Accept(this));
            }

            return sqlBuilder;
        }

        public override ISqlFragment Visit(DbLimitExpression e)
        {
            if (e.Limit is DbParameterReferenceExpression)
                throw new NotSupportedException();
            var sqlSelectStatement = !e.WithTies
                ? VisitExpressionEnsureSqlStatement(e.Argument, false)
                : throw new NotSupportedException();
            if (!IsCompatible(sqlSelectStatement, e.ExpressionKind))
            {
                var elementTypeUsage = MetadataHelpers.GetElementTypeUsage(e.Argument.ResultType);
                Symbol fromSymbol;
                sqlSelectStatement =
                    CreateNewSelectStatement(sqlSelectStatement, "top", elementTypeUsage, out fromSymbol);
                AddFromSymbol(sqlSelectStatement, "top", fromSymbol, false);
            }

            var topCount = HandleCountExpression(e.Limit);
            sqlSelectStatement.Top = new TopClause(topCount, e.WithTies);
            return sqlSelectStatement;
        }

        public override ISqlFragment Visit(DbNewInstanceExpression e)
        {
            if (MetadataHelpers.IsCollectionType(e.ResultType.EdmType))
                return VisitCollectionConstructor(e);
            throw new NotSupportedException();
        }

        public override ISqlFragment Visit(DbNotExpression e)
        {
            if (e.Argument is DbNotExpression dbNotExpression)
                return dbNotExpression.Argument.Accept(
                    this);
            if (e.Argument is DbIsEmptyExpression e1)
                return VisitIsEmptyExpression(e1, true);
            if (e.Argument is DbIsNullExpression e2)
                return VisitIsNullExpression(e2, true);
            if (e.Argument is DbComparisonExpression comparisonExpression &&
                comparisonExpression.ExpressionKind == (DbExpressionKind)13)
                return VisitBinaryExpression(" <> ", (DbExpressionKind)37,
                    comparisonExpression.Left, comparisonExpression.Right);
            var sqlBuilder = new SqlBuilder();
            sqlBuilder.Append(" NOT (");
            sqlBuilder.Append(
                e.Argument.Accept(this));
            sqlBuilder.Append(")");
            return sqlBuilder;
        }

        public override ISqlFragment Visit(DbNullExpression e)
        {
            var sqlBuilder = new SqlBuilder();
            sqlBuilder.Append("CAST(NULL AS ");
            var primitiveTypeKind =
                ((PrimitiveType)e.ResultType.EdmType).PrimitiveTypeKind;
            sqlBuilder.Append(ConvertPrimitiveTypetoCastType(primitiveTypeKind, 10, 10));
            sqlBuilder.Append(")");
            return sqlBuilder;
        }

        public override ISqlFragment Visit(DbOfTypeExpression e) => throw new NotSupportedException();

        public override ISqlFragment Visit(DbOrExpression e)
        {
            return VisitBinaryExpression(" OR ", e.ExpressionKind,
                e.Left, e.Right);
        }

        public override ISqlFragment Visit(DbParameterReferenceExpression e)
        {
            var sqlBuilder = new SqlBuilder();
            sqlBuilder.Append(":" + e.ParameterName);
            return sqlBuilder;
        }

        public override ISqlFragment Visit(DbProjectExpression e)
        {
            Symbol fromSymbol;
            var sqlSelectStatement = VisitInputExpression(e.Input.Expression, e.Input.VariableName,
                e.Input.VariableType, out fromSymbol);
            var aliasesNeedRenaming = false;
            if (!IsCompatible(sqlSelectStatement, e.ExpressionKind))
                sqlSelectStatement = CreateNewSelectStatement(sqlSelectStatement, e.Input.VariableName,
                    e.Input.VariableType, out fromSymbol);
            selectStatementStack.Push(sqlSelectStatement);
            symbolTable.EnterScope();
            AddFromSymbol(sqlSelectStatement, e.Input.VariableName, fromSymbol);
            if (e.Projection is DbNewInstanceExpression projection)
            {
                Dictionary<string, Symbol> newColumns;
                sqlSelectStatement.Select.Append(
                    VisitNewInstanceExpression(projection, aliasesNeedRenaming, out newColumns));
                if (aliasesNeedRenaming)
                {
                    sqlSelectStatement.OutputColumnsRenamed = true;
                    sqlSelectStatement.OutputColumns = newColumns;
                }
            }
            else
                sqlSelectStatement.Select.Append(
                    e.Projection.Accept(this));

            symbolTable.ExitScope();
            selectStatementStack.Pop();
            return sqlSelectStatement;
        }

        public override ISqlFragment Visit(DbPropertyExpression e)
        {
            var s = e.Instance.Accept(this);
            if (e.Instance is DbVariableReferenceExpression)
                isVarRefSingle = false;
            switch (s)
            {
                case JoinSymbol source:
                    return source.IsNestedJoin
                        ? new SymbolPair(source, source.NameToExtent[e.Property.Name])
                        : source.NameToExtent[e.Property.Name];
                case SymbolPair symbolPair:
                    if (symbolPair.Column is JoinSymbol column)
                    {
                        symbolPair.Column = column.NameToExtent[e.Property.Name];
                        return symbolPair;
                    }

                    if (symbolPair.Column.Columns.ContainsKey(e.Property.Name))
                    {
                        var sqlBuilder = new SqlBuilder();
                        sqlBuilder.Append(symbolPair.Source);
                        sqlBuilder.Append(".");
                        sqlBuilder.Append(symbolPair.Column.Columns[e.Property.Name]);
                        return sqlBuilder;
                    }

                    break;
            }

            var sqlBuilder1 = new SqlBuilder();
            sqlBuilder1.Append(s);
            sqlBuilder1.Append(".");
            if (s is Symbol symbol && symbol.OutputColumnsRenamed)
                sqlBuilder1.Append(symbol.Columns[e.Property.Name]);
            else
                sqlBuilder1.Append(QuoteIdentifier(e.Property.Name));
            return sqlBuilder1;
        }

        public override ISqlFragment Visit(DbQuantifierExpression e)
        {
            var sqlBuilder = new SqlBuilder();
            var negatePredicate = e.ExpressionKind == 0;
            if (e.ExpressionKind == (DbExpressionKind)2)
                sqlBuilder.Append("EXISTS (");
            else
                sqlBuilder.Append("NOT EXISTS (");
            var sqlSelectStatement = VisitFilterExpression(e.Input, e.Predicate, negatePredicate);
            if (sqlSelectStatement.Select.IsEmpty)
                AddDefaultColumns(sqlSelectStatement);
            sqlBuilder.Append(sqlSelectStatement);
            sqlBuilder.Append(")");
            return sqlBuilder;
        }

        public override ISqlFragment Visit(DbRefExpression e) => throw new NotSupportedException();

        public override ISqlFragment Visit(DbRelationshipNavigationExpression e)
        {
            throw new NotSupportedException();
        }

        public override ISqlFragment Visit(DbSkipExpression e)
        {
            if (e.Count is DbParameterReferenceExpression)
                throw new NotSupportedException();
            Symbol fromSymbol;
            var sqlSelectStatement = VisitInputExpression(e.Input.Expression, e.Input.VariableName,
                e.Input.VariableType, out fromSymbol);
            var hasTop = sqlSelectStatement.Top != null;
            if (!IsCompatible(sqlSelectStatement, e.ExpressionKind) && !hasTop)
            {
                var elementTypeUsage = MetadataHelpers.GetElementTypeUsage(e.ResultType);
                sqlSelectStatement = CreateNewSelectStatement(sqlSelectStatement, e.Input.VariableName,
                    elementTypeUsage, out fromSymbol);
            }

            selectStatementStack.Push(sqlSelectStatement);
            symbolTable.EnterScope();
            AddFromSymbol(sqlSelectStatement, e.Input.VariableName, fromSymbol);
            var startatCount = (int)((DbConstantExpression)e.Count).Value;
            sqlSelectStatement.StartAt = new StartAtClause(startatCount, hasTop);
            if (sqlSelectStatement.OrderBy.IsEmpty)
                AddSortKeys(sqlSelectStatement.OrderBy, e.SortOrder);
            symbolTable.ExitScope();
            selectStatementStack.Pop();
            return sqlSelectStatement;
        }

        public override ISqlFragment Visit(DbSortExpression e)
        {
            Symbol fromSymbol;
            var sqlSelectStatement = VisitInputExpression(e.Input.Expression, e.Input.VariableName,
                e.Input.VariableType, out fromSymbol);
            if (!IsCompatible(sqlSelectStatement, e.ExpressionKind))
                sqlSelectStatement = CreateNewSelectStatement(sqlSelectStatement, e.Input.VariableName,
                    e.Input.VariableType, out fromSymbol);
            selectStatementStack.Push(sqlSelectStatement);
            symbolTable.EnterScope();
            AddFromSymbol(sqlSelectStatement, e.Input.VariableName, fromSymbol);
            AddSortKeys(sqlSelectStatement.OrderBy, e.SortOrder);
            symbolTable.ExitScope();
            selectStatementStack.Pop();
            return sqlSelectStatement;
        }

        public override ISqlFragment Visit(DbTreatExpression e) => throw new NotSupportedException();

        public override ISqlFragment Visit(DbUnionAllExpression e)
        {
            return VisitSetOpExpression(e.Left, e.Right, "UNION ALL");
        }

        public override ISqlFragment Visit(DbVariableReferenceExpression e)
        {
            isVarRefSingle = !isVarRefSingle ? true : throw new NotSupportedException();
            var key = symbolTable.Lookup(e.VariableName);
            if (!CurrentSelectStatement.FromExtents.Contains(key))
                CurrentSelectStatement.OuterExtents[key] = true;
            return key;
        }

        private static SqlBuilder VisitAggregate(DbAggregate aggregate, object aggregateArgument)
        {
            var result = new SqlBuilder();
            if (!(aggregate is DbFunctionAggregate functionAggregate1))
                throw new NotSupportedException();
            WriteFunctionName(result, functionAggregate1.Function);
            result.Append("(");
            var functionAggregate2 = functionAggregate1;
            if (functionAggregate2 != null && functionAggregate2.Distinct)
                result.Append("DISTINCT ");
            result.Append(aggregateArgument);
            result.Append(")");
            return result;
        }

        private void ParanthesizeExpressionIfNeeded(DbExpression e, SqlBuilder result)
        {
            if (IsComplexExpression(e))
            {
                result.Append("(");
                result.Append(e.Accept(this));
                result.Append(")");
            }
            else
                result.Append(e.Accept(this));
        }

        private SqlBuilder VisitBinaryExpression(
            string op,
            DbExpressionKind expressionKind,
            DbExpression left,
            DbExpression right)
        {
            var result = new SqlBuilder();
            var flag = true;
            foreach (var e in CommandTreeUtils.FlattenAssociativeExpression(expressionKind, left, right))
            {
                if (flag)
                    flag = false;
                else
                    result.Append(op);
                ParanthesizeExpressionIfNeeded(e, result);
            }

            return result;
        }

        private SqlBuilder VisitComparisonExpression(string op, DbExpression left, DbExpression right)
        {
            var result = new SqlBuilder();
            var isCastOptional = left.ResultType.EdmType == right.ResultType.EdmType;
            if (left.ExpressionKind == (DbExpressionKind)5)
                result.Append(VisitConstant((DbConstantExpression)left, isCastOptional));
            else
                ParanthesizeExpressionIfNeeded(left, result);
            result.Append(op);
            if (right.ExpressionKind == (DbExpressionKind)5)
                result.Append(VisitConstant((DbConstantExpression)right, isCastOptional));
            else
                ParanthesizeExpressionIfNeeded(right, result);
            return result;
        }

        private SqlSelectStatement VisitInputExpression(
            DbExpression inputExpression,
            string inputVarName,
            TypeUsage inputVarType,
            out Symbol fromSymbol)
        {
            var sqlFragment = inputExpression.Accept(this);
            if (!(sqlFragment is SqlSelectStatement result))
            {
                result = new SqlSelectStatement();
                WrapNonQueryExtent(result, sqlFragment, inputExpression.ExpressionKind);
            }

            if (result.FromExtents.Count == 0)
                fromSymbol = new Symbol(inputVarName, inputVarType);
            else if (result.FromExtents.Count == 1)
            {
                fromSymbol = result.FromExtents[0];
            }
            else
            {
                fromSymbol = new JoinSymbol(inputVarName, inputVarType, result.FromExtents)
                {
                    FlattenedExtentList = result.AllJoinExtents
                };
                result.FromExtents.Clear();
                result.FromExtents.Add(fromSymbol);
            }

            return result;
        }

        private SqlBuilder VisitIsEmptyExpression(DbIsEmptyExpression e, bool negate)
        {
            var sqlBuilder = new SqlBuilder();
            if (!negate)
                sqlBuilder.Append(" NOT");
            sqlBuilder.Append(" EXISTS (");
            sqlBuilder.Append(VisitExpressionEnsureSqlStatement(e.Argument));
            sqlBuilder.AppendLine();
            sqlBuilder.Append(")");
            return sqlBuilder;
        }

        private ISqlFragment VisitCollectionConstructor(DbNewInstanceExpression e)
        {
            if (e.Arguments.Count == 1 && e.Arguments[0].ExpressionKind == (DbExpressionKind)11)
            {
                var elementExpression = e.Arguments[0] as DbElementExpression;
                var sqlSelectStatement =
                    VisitExpressionEnsureSqlStatement(elementExpression.Argument);
                if (!IsCompatible(sqlSelectStatement, (DbExpressionKind)11))
                {
                    var elementTypeUsage =
                        MetadataHelpers.GetElementTypeUsage(elementExpression.Argument.ResultType);
                    Symbol fromSymbol;
                    sqlSelectStatement = CreateNewSelectStatement(sqlSelectStatement, "element", elementTypeUsage,
                        out fromSymbol);
                    AddFromSymbol(sqlSelectStatement, "element", fromSymbol, false);
                }

                sqlSelectStatement.Top = new TopClause(1, false);
                return sqlSelectStatement;
            }

            var edmType = MetadataHelpers.GetEdmType<CollectionType>(e.ResultType);
            var flag = MetadataHelpers.IsPrimitiveType(edmType.TypeUsage.EdmType);
            var sqlBuilder = new SqlBuilder();
            var s = "";
            if (e.Arguments.Count == 0)
            {
                sqlBuilder.Append(" SELECT CAST(null as ");
                var primitiveTypeKind = ((PrimitiveType)edmType.TypeUsage.EdmType).PrimitiveTypeKind;
                sqlBuilder.Append(ConvertPrimitiveTypetoCastType(primitiveTypeKind, 10, 10));
                sqlBuilder.Append(") AS X FROM system.iota Y WHERE 1=0");
            }

            foreach (var dbExpression in e.Arguments)
            {
                sqlBuilder.Append(s);
                sqlBuilder.Append(" SELECT ");
                sqlBuilder.Append(dbExpression.Accept(this));
                if (flag)
                    sqlBuilder.Append(" FROM system.iota ");
                s = " UNION ALL ";
            }

            return sqlBuilder;
        }

        private SqlBuilder VisitIsNullExpression(DbIsNullExpression e, bool negate)
        {
            var sqlBuilder = new SqlBuilder();
            sqlBuilder.Append(
                e.Argument.Accept(this));
            if (!negate)
                sqlBuilder.Append(" IS NULL");
            else
                sqlBuilder.Append(" IS NOT NULL");
            return sqlBuilder;
        }

        private ISqlFragment VisitJoinExpression(
            IList<DbExpressionBinding> inputs,
            DbExpressionKind joinKind,
            string joinString,
            DbExpression joinCondition)
        {
            SqlSelectStatement result;
            if (!IsParentAJoin)
            {
                result = new SqlSelectStatement();
                result.AllJoinExtents = new List<Symbol>();
                selectStatementStack.Push(result);
            }
            else
                result = CurrentSelectStatement;

            symbolTable.EnterScope();
            var str = "";
            var flag = true;
            var count1 = inputs.Count;
            for (var index = 0; index < count1; ++index)
            {
                var input = inputs[index];
                if (str.Length != 0)
                    result.From.AppendLine();
                result.From.Append(str + " ");
                isParentAJoinStack.Push(input.Expression.ExpressionKind == (DbExpressionKind)50 ||
                                             flag && (IsJoinExpression(input.Expression) ||
                                                      IsApplyExpression(input.Expression)));
                var count2 = result.FromExtents.Count;
                var fromExtentFragment =
                    input.Expression.Accept(this);
                isParentAJoinStack.Pop();
                ProcessJoinInputResult(fromExtentFragment, result, input, count2);
                str = joinString;
                flag = false;
            }

            var dbExpressionKind = joinKind;
            if (dbExpressionKind == (DbExpressionKind)16 || dbExpressionKind == (DbExpressionKind)21 ||
                dbExpressionKind == (DbExpressionKind)27)
            {
                result.From.Append(" ON ");
                isParentAJoinStack.Push(false);
                result.From.Append(joinCondition.Accept(this));
                isParentAJoinStack.Pop();
            }

            symbolTable.ExitScope();
            if (!IsParentAJoin)
                selectStatementStack.Pop();
            return result;
        }

        private void ProcessJoinInputResult(
            ISqlFragment fromExtentFragment,
            SqlSelectStatement result,
            DbExpressionBinding input,
            int fromSymbolStart)
        {
            Symbol fromSymbol = null;
            if (result != fromExtentFragment)
            {
                if (fromExtentFragment is SqlSelectStatement sqlSelectStatement)
                {
                    if (sqlSelectStatement.Select.IsEmpty)
                    {
                        var symbolList = AddDefaultColumns(sqlSelectStatement);
                        if (IsJoinExpression(input.Expression) ||
                            IsApplyExpression(input.Expression))
                        {
                            var fromExtents = sqlSelectStatement.FromExtents;
                            fromSymbol = new JoinSymbol(input.VariableName, input.VariableType, fromExtents)
                            {
                                IsNestedJoin = true,
                                ColumnList = symbolList
                            };
                        }
                        else if (sqlSelectStatement.FromExtents[0] is JoinSymbol fromExtent)
                            fromSymbol = new JoinSymbol(input.VariableName, input.VariableType,
                                fromExtent.ExtentList)
                            {
                                IsNestedJoin = true,
                                ColumnList = symbolList,
                                FlattenedExtentList = fromExtent.FlattenedExtentList
                            };
                        else if (sqlSelectStatement.FromExtents[0].OutputColumnsRenamed)
                            fromSymbol = new Symbol(input.VariableName, input.VariableType,
                                sqlSelectStatement.FromExtents[0].Columns);
                    }
                    else if (sqlSelectStatement.OutputColumnsRenamed)
                        fromSymbol = new Symbol(input.VariableName, input.VariableType,
                            sqlSelectStatement.OutputColumns);

                    result.From.Append(" (");
                    result.From.Append(sqlSelectStatement);
                    result.From.Append(" )");
                }
                else if (input.Expression is DbScanExpression)
                    result.From.Append(fromExtentFragment);
                else
                    WrapNonQueryExtent(result, fromExtentFragment, input.Expression.ExpressionKind);

                if (fromSymbol == null)
                    fromSymbol = new Symbol(input.VariableName, input.VariableType);
                AddFromSymbol(result, input.VariableName, fromSymbol);
                result.AllJoinExtents.Add(fromSymbol);
            }
            else
            {
                var extents = new List<Symbol>();
                for (var index = fromSymbolStart; index < result.FromExtents.Count; ++index)
                    extents.Add(result.FromExtents[index]);
                result.FromExtents.RemoveRange(fromSymbolStart, result.FromExtents.Count - fromSymbolStart);
                Symbol symbol = new JoinSymbol(input.VariableName, input.VariableType, extents);
                result.FromExtents.Add(symbol);
                symbolTable.Add(input.VariableName, symbol);
            }
        }

        private ISqlFragment VisitNewInstanceExpression(
            DbNewInstanceExpression e,
            bool aliasesNeedRenaming,
            out Dictionary<string, Symbol> newColumns)
        {
            var sqlBuilder = new SqlBuilder();
            if (!(e.ResultType.EdmType is RowType edmType))
                throw new NotSupportedException();
            newColumns = !aliasesNeedRenaming
                ? null
                : new Dictionary<string, Symbol>(e.Arguments.Count);
            var properties = edmType.Properties;
            var s1 = "";
            for (var index = 0; index < e.Arguments.Count; ++index)
            {
                var dbExpression = e.Arguments[index];
                if (MetadataHelpers.IsRowType(dbExpression.ResultType.EdmType))
                    throw new NotSupportedException();
                var edmProperty =
                    properties[index];
                sqlBuilder.Append(s1);
                sqlBuilder.AppendLine();
                sqlBuilder.Append(dbExpression.Accept(this));
                sqlBuilder.Append(" AS ");
                if (aliasesNeedRenaming)
                {
                    var s2 = new Symbol(edmProperty.Name, edmProperty.TypeUsage);
                    s2.NeedsRenaming = true;
                    s2.NewName = "Internal_" + edmProperty.Name;
                    sqlBuilder.Append(s2);
                    newColumns.Add(edmProperty.Name, s2);
                }
                else
                    sqlBuilder.Append(QuoteIdentifier(edmProperty.Name));

                s1 = ", ";
            }

            return sqlBuilder;
        }

        private ISqlFragment VisitSetOpExpression(
            DbExpression left,
            DbExpression right,
            string separator)
        {
            var s1 = VisitExpressionEnsureSqlStatement(left);
            var s2 = VisitExpressionEnsureSqlStatement(right);
            var s3 = new SqlBuilder();
            s3.Append(s1);
            s3.AppendLine();
            s3.Append(separator);
            s3.AppendLine();
            s3.Append(s2);
            if (!s1.OutputColumnsRenamed)
                return s3;
            var selectStatement = new SqlSelectStatement();
            selectStatement.From.Append("( ");
            selectStatement.From.Append(s3);
            selectStatement.From.AppendLine();
            selectStatement.From.Append(") ");
            var fromSymbol = new Symbol("X", left.ResultType, s1.OutputColumns);
            AddFromSymbol(selectStatement, null, fromSymbol, false);
            return selectStatement;
        }

        private static bool IsSpecialCanonicalFunction(DbFunctionExpression e)
        {
            return MetadataHelpers.IsCanonicalFunction(e.Function) &&
                   _canonicalFunctionHandlers.ContainsKey(e.Function.Name);
        }

        private ISqlFragment HandleFunctionDefault(DbFunctionExpression e)
        {
            var result = new SqlBuilder();
            WriteFunctionName(result, e.Function);
            HandleFunctionArgumentsDefault(e, result);
            return result;
        }

        private ISqlFragment HandleFunctionDefaultGivenName(DbFunctionExpression e, string functionName)
        {
            var result = new SqlBuilder();
            result.Append(functionName);
            HandleFunctionArgumentsDefault(e, result);
            return result;
        }

        private void HandleFunctionArgumentsDefault(DbFunctionExpression e, SqlBuilder result)
        {
            var metadataProperty =
                MetadataHelpers.GetMetadataProperty<bool>(e.Function, "NiladicFunctionAttribute");
            if (metadataProperty && e.Arguments.Count > 0)
                throw new MetadataException(
                    "Functions listed in the provider manifest that are attributed as NiladicFunction='true' cannot have parameter declarations.");
            if (metadataProperty)
                return;
            result.Append("(");
            var s = "";
            foreach (var dbExpression in e.Arguments)
            {
                result.Append(s);
                result.Append(dbExpression.Accept(this));
                s = ", ";
            }

            result.Append(")");
        }

        private ISqlFragment HandleSpecialCanonicalFunction(DbFunctionExpression e)
        {
            return HandleSpecialFunction(_canonicalFunctionHandlers, e);
        }

        private ISqlFragment HandleSpecialFunction(
            Dictionary<string, FunctionHandler> handlers,
            DbFunctionExpression e)
        {
            return handlers[e.Function.Name](this, e);
        }

        private static ISqlFragment HandleCanonicalFunctionSubstring(
            SqlGenerator sqlgen,
            DbFunctionExpression e)
        {
            return sqlgen.HandleFunctionDefaultGivenName(e, "SUBSTRING");
        }

        private static ISqlFragment HandleCanonicalFunctionLeft(
            SqlGenerator sqlgen,
            DbFunctionExpression e)
        {
            var sqlBuilder = new SqlBuilder();
            sqlBuilder.Append("LEFT( ");
            sqlBuilder.Append(e.Arguments[0].Accept(sqlgen));
            sqlBuilder.Append(", ");
            sqlBuilder.Append(e.Arguments[1].Accept(sqlgen));
            sqlBuilder.Append(" )");
            return sqlBuilder;
        }

        private static ISqlFragment HandleCanonicalFunctionRight(
            SqlGenerator sqlgen,
            DbFunctionExpression e)
        {
            var sqlBuilder = new SqlBuilder();
            sqlBuilder.Append("RIGHT( ");
            sqlBuilder.Append(e.Arguments[0].Accept(sqlgen));
            sqlBuilder.Append(", ");
            sqlBuilder.Append(e.Arguments[1].Accept(sqlgen));
            sqlBuilder.Append(" )");
            return sqlBuilder;
        }

        private static ISqlFragment HandleConcatFunction(SqlGenerator sqlgen, DbFunctionExpression e)
        {
            var sqlBuilder = new SqlBuilder();
            sqlBuilder.Append("((");
            sqlBuilder.Append(e.Arguments[0].Accept(sqlgen));
            sqlBuilder.Append(") + (");
            sqlBuilder.Append(e.Arguments[1].Accept(sqlgen));
            sqlBuilder.Append("))");
            return sqlBuilder;
        }

        private static ISqlFragment HandleCanonicalFunctionBitwise(
            SqlGenerator sqlgen,
            DbFunctionExpression e)
        {
            var sqlBuilder = new SqlBuilder();
            switch (e.Function.Name.ToUpperInvariant())
            {
                case "BITWISEAND":
                    sqlBuilder.Append("(");
                    sqlBuilder.Append(e.Arguments[0]
                        .Accept(sqlgen));
                    sqlBuilder.Append(" & ");
                    sqlBuilder.Append(e.Arguments[1]
                        .Accept(sqlgen));
                    sqlBuilder.Append(")");
                    break;
                case "BITWISEOR":
                    sqlBuilder.Append("(");
                    sqlBuilder.Append(e.Arguments[0]
                        .Accept(sqlgen));
                    sqlBuilder.Append(" | ");
                    sqlBuilder.Append(e.Arguments[1]
                        .Accept(sqlgen));
                    sqlBuilder.Append(")");
                    break;
                case "BITWISEXOR":
                    sqlBuilder.Append("(");
                    sqlBuilder.Append(e.Arguments[0]
                        .Accept(sqlgen));
                    sqlBuilder.Append(" ^ ");
                    sqlBuilder.Append(e.Arguments[1]
                        .Accept(sqlgen));
                    sqlBuilder.Append(")");
                    break;
                case "BITWISENOT":
                    sqlBuilder.Append("(");
                    sqlBuilder.Append(" ~ ");
                    sqlBuilder.Append(e.Arguments[0]
                        .Accept(sqlgen));
                    sqlBuilder.Append(")");
                    break;
            }

            return sqlBuilder;
        }

        private static ISqlFragment HandleCanonicalFunctionDatepart(
            SqlGenerator sqlgen,
            DbFunctionExpression e)
        {
            var sqlBuilder = new SqlBuilder();
            sqlBuilder.Append("EXTRACT (");
            sqlBuilder.Append(e.Function.Name.ToUpperInvariant());
            sqlBuilder.Append(" FROM (");
            sqlBuilder.Append(e.Arguments[0].Accept(sqlgen));
            sqlBuilder.Append("))");
            return sqlBuilder;
        }

        private static ISqlFragment HandleCanonicalFunctionGetTotalOffsetMinutes(
            SqlGenerator sqlgen,
            DbFunctionExpression e)
        {
            throw new NotSupportedException(e.Function.Name.ToUpperInvariant() +
                                            " operator is not supported by Advantage.");
        }

        private static ISqlFragment HandleCanonicalFunctionCurrentDateTime(
            SqlGenerator sqlgen,
            DbFunctionExpression e)
        {
            var sqlBuilder = new SqlBuilder();
            switch (e.Function.Name.ToUpperInvariant())
            {
                case "CURRENTDATETIME":
                    sqlBuilder.Append("NOW()");
                    break;
                case "CURRENTUTCDATETIME":
                case "CURRENTDATETIMEOFFSET":
                    throw new NotSupportedException(e.Function.Name.ToUpperInvariant() +
                                                    " operator is not supported by Advantage.");
            }

            return sqlBuilder;
        }

        private static ISqlFragment HandleCanonicalFunctionCreateDateTime(
            SqlGenerator sqlgen,
            DbFunctionExpression e)
        {
            var dateTime = new SqlBuilder();
            dateTime.Append("CREATETIMESTAMP(");
            foreach (var dbExpression in e.Arguments)
            {
                dateTime.Append(dbExpression.Accept(sqlgen));
                dateTime.Append(", ");
            }

            dateTime.Append("0)");
            return dateTime;
        }

        private static ISqlFragment HandleCanonicalFunctionTimestampAdd(
            SqlGenerator sqlgen,
            DbFunctionExpression e)
        {
            var sqlBuilder = new SqlBuilder();
            sqlBuilder.Append("TIMESTAMPADD( ");
            switch (e.Function.Name.ToUpperInvariant())
            {
                case "ADDYEARS":
                    sqlBuilder.Append("SQL_TSI_YEAR");
                    break;
                case "ADDMONTHS":
                    sqlBuilder.Append("SQL_TSI_MONTH");
                    break;
                case "ADDDAYS":
                    sqlBuilder.Append("SQL_TSI_DAY");
                    break;
                case "ADDHOURS":
                    sqlBuilder.Append("SQL_TSI_HOUR");
                    break;
                case "ADDMINUTES":
                    sqlBuilder.Append("SQL_TSI_MINUTE");
                    break;
                case "ADDSECONDS":
                    sqlBuilder.Append("SQL_TSI_SECOND");
                    break;
                case "ADDMILLISECONDS":
                    sqlBuilder.Append("SQL_TSI_FRAC_SECOND");
                    break;
                default:
                    throw new NotSupportedException(e.Function.Name.ToUpperInvariant() +
                                                    " operator is not supported by Advantage.");
            }

            sqlBuilder.Append(", ");
            sqlBuilder.Append(e.Arguments[1].Accept(sqlgen));
            sqlBuilder.Append(", ");
            sqlBuilder.Append(e.Arguments[0].Accept(sqlgen));
            sqlBuilder.Append(" )");
            return sqlBuilder;
        }

        private static ISqlFragment HandleCanonicalFunctionTimestampDiff(
            SqlGenerator sqlgen,
            DbFunctionExpression e)
        {
            var sqlBuilder = new SqlBuilder();
            sqlBuilder.Append("TIMESTAMPDIFF( ");
            switch (e.Function.Name.ToUpperInvariant())
            {
                case "DIFFYEARS":
                    sqlBuilder.Append("SQL_TSI_YEAR");
                    break;
                case "DIFFMONTHS":
                    sqlBuilder.Append("SQL_TSI_MONTH");
                    break;
                case "DIFFDAYS":
                    sqlBuilder.Append("SQL_TSI_DAY");
                    break;
                case "DIFFHOURS":
                    sqlBuilder.Append("SQL_TSI_HOUR");
                    break;
                case "DIFFMINUTES":
                    sqlBuilder.Append("SQL_TSI_MINUTE");
                    break;
                case "DIFFSECONDS":
                    sqlBuilder.Append("SQL_TSI_SECOND");
                    break;
                case "DIFFMILLISECONDS":
                    sqlBuilder.Append("SQL_TSI_FRAC_SECOND");
                    break;
                default:
                    throw new NotSupportedException(e.Function.Name.ToUpperInvariant() +
                                                    " operator is not supported by Advantage.");
            }

            sqlBuilder.Append(", ");
            sqlBuilder.Append(e.Arguments[0].Accept(sqlgen));
            sqlBuilder.Append(", ");
            sqlBuilder.Append(e.Arguments[1].Accept(sqlgen));
            sqlBuilder.Append(" )");
            return sqlBuilder;
        }

        private static ISqlFragment HandleCanonicalFunctionIndexOf(
            SqlGenerator sqlgen,
            DbFunctionExpression e)
        {
            var sqlBuilder = new SqlBuilder();
            sqlBuilder.Append("POSITION( ");
            sqlBuilder.Append(e.Arguments[0].Accept(sqlgen));
            sqlBuilder.Append(" IN ");
            sqlBuilder.Append(e.Arguments[1].Accept(sqlgen));
            sqlBuilder.Append(" )");
            return sqlBuilder;
        }

        private static ISqlFragment HandleCanonicalFunctionReverse(
            SqlGenerator sqlgen,
            DbFunctionExpression e)
        {
            throw new NotSupportedException(e.Function.Name.ToUpperInvariant() +
                                            " operator is not supported by Advantage.");
        }

        private static ISqlFragment HandleCanonicalFunctionNewGuid(
            SqlGenerator sqlgen,
            DbFunctionExpression e)
        {
            throw new NotSupportedException(e.Function.Name.ToUpperInvariant() +
                                            " operator is not supported by Advantage.");
        }

        private static ISqlFragment HandleCanonicalFunctionLength(
            SqlGenerator sqlgen,
            DbFunctionExpression e)
        {
            var sqlBuilder = new SqlBuilder();
            sqlBuilder.Append("LENGTH(");
            sqlBuilder.Append(e.Arguments[0].Accept(sqlgen));
            sqlBuilder.Append(")");
            return sqlBuilder;
        }

        private static ISqlFragment HandleCanonicalFunctionRound(
            SqlGenerator sqlgen,
            DbFunctionExpression e)
        {
            var sqlBuilder = new SqlBuilder();
            sqlBuilder.Append("ROUND(");
            sqlBuilder.Append(e.Arguments[0].Accept(sqlgen));
            sqlBuilder.Append(", ");
            if (e.Arguments.Count == 1)
                sqlBuilder.Append(" 0");
            else
                sqlBuilder.Append(
                    e.Arguments[1].Accept(sqlgen));
            sqlBuilder.Append(")");
            return sqlBuilder;
        }

        private static ISqlFragment HandleCanonicalFunctionTrim(
            SqlGenerator sqlgen,
            DbFunctionExpression e)
        {
            var sqlBuilder = new SqlBuilder();
            sqlBuilder.Append("LTRIM(RTRIM(");
            sqlBuilder.Append(e.Arguments[0].Accept(sqlgen));
            sqlBuilder.Append("))");
            return sqlBuilder;
        }

        private static ISqlFragment HandleCanonicalFunctionToLower(
            SqlGenerator sqlgen,
            DbFunctionExpression e)
        {
            return sqlgen.HandleFunctionDefaultGivenName(e, "LOWER");
        }

        private static ISqlFragment HandleCanonicalFunctionToUpper(
            SqlGenerator sqlgen,
            DbFunctionExpression e)
        {
            return sqlgen.HandleFunctionDefaultGivenName(e, "UPPER");
        }

        private static ISqlFragment HandleCanonicalFunctionCeiling(
            SqlGenerator sqlgen,
            DbFunctionExpression e)
        {
            return sqlgen.HandleFunctionDefaultGivenName(e, "CEILING");
        }

        private static string EscapeLikeClause(string clause)
        {
            return !clause.Contains("_") && !clause.Contains("%")
                ? clause
                : clause.Replace("@", "@@").Replace("%", "@%").Replace("_", "@_");
        }

        private static ISqlFragment HandleCanonicalFunctionStartsWith(
            SqlGenerator sqlgen,
            DbFunctionExpression e)
        {
            var sqlBuilder = new SqlBuilder();
            sqlBuilder.Append("STARTSWITH(");
            sqlBuilder.Append(e.Arguments[0].Accept(sqlgen));
            sqlBuilder.Append(", ");
            sqlBuilder.Append(e.Arguments[1].Accept(sqlgen));
            sqlBuilder.Append(")");
            return sqlBuilder;
        }

        private static ISqlFragment HandleCanonicalFunctionEndsWith(
            SqlGenerator sqlgen,
            DbFunctionExpression e)
        {
            var sqlBuilder = new SqlBuilder();
            sqlBuilder.Append("ENDSWITH(");
            sqlBuilder.Append(e.Arguments[0].Accept(sqlgen));
            sqlBuilder.Append(", ");
            sqlBuilder.Append(e.Arguments[1].Accept(sqlgen));
            sqlBuilder.Append(")");
            return sqlBuilder;
        }

        private static ISqlFragment HandleCanonicalFunctionContains(
            SqlGenerator sqlgen,
            DbFunctionExpression e)
        {
            var sqlBuilder = new SqlBuilder();
            sqlBuilder.Append("SUBSTRINGOF(");
            sqlBuilder.Append(e.Arguments[1].Accept(sqlgen));
            sqlBuilder.Append(", ");
            sqlBuilder.Append(e.Arguments[0].Accept(sqlgen));
            sqlBuilder.Append(")");
            return sqlBuilder;
        }

        private static ISqlFragment HandleUnsupportedCanonicalFunction(
            SqlGenerator sqlgen,
            DbFunctionExpression e)
        {
            throw new NotSupportedException(e.Function.Name.ToUpperInvariant() +
                                            " operator is not supported by Advantage.");
        }

        private static void WriteFunctionName(SqlBuilder result, EdmFunction function)
        {
            var metadataProperty1 =
                MetadataHelpers.GetMetadataProperty<string>(function, "StoreFunctionNameAttribute");
            var str = metadataProperty1 == null ? function.Name : metadataProperty1;
            if (MetadataHelpers.IsCanonicalFunction(function))
                result.Append(str.ToUpperInvariant());
            else if (IsBuiltInStoreFunction(function))
            {
                result.Append(str);
            }
            else
            {
                var metadataProperty2 =
                    MetadataHelpers.GetMetadataProperty<string>(function, "Schema");
                if (string.IsNullOrEmpty(metadataProperty2))
                    result.Append(QuoteIdentifier(function.NamespaceName));
                else
                    result.Append(QuoteIdentifier(metadataProperty2));
                result.Append(".");
                result.Append(QuoteIdentifier(str));
            }
        }

        private void AddColumns(
            SqlSelectStatement selectStatement,
            Symbol symbol,
            List<Symbol> columnList,
            Dictionary<string, Symbol> columnDictionary,
            ref string separator)
        {
            if (symbol is JoinSymbol joinSymbol)
            {
                if (!joinSymbol.IsNestedJoin)
                {
                    foreach (var extent in joinSymbol.ExtentList)
                    {
                        if (extent.Type != null && !MetadataHelpers.IsPrimitiveType(extent.Type.EdmType))
                            AddColumns(selectStatement, extent, columnList, columnDictionary, ref separator);
                    }
                }
                else
                {
                    foreach (var column in joinSymbol.ColumnList)
                    {
                        selectStatement.Select.Append(separator);
                        selectStatement.Select.Append(symbol);
                        selectStatement.Select.Append(".");
                        selectStatement.Select.Append(column);
                        if (columnDictionary.ContainsKey(column.Name))
                        {
                            columnDictionary[column.Name].NeedsRenaming = true;
                            column.NeedsRenaming = true;
                        }
                        else
                            columnDictionary[column.Name] = column;

                        columnList.Add(column);
                        separator = ", ";
                    }
                }
            }
            else
            {
                if (symbol.OutputColumnsRenamed)
                {
                    selectStatement.OutputColumnsRenamed = true;
                    selectStatement.OutputColumns = new Dictionary<string, Symbol>();
                }

                if (symbol.Type == null || MetadataHelpers.IsPrimitiveType(symbol.Type.EdmType))
                {
                    AddColumn(selectStatement, symbol, columnList, columnDictionary, ref separator, "X");
                }
                else
                {
                    foreach (var property in MetadataHelpers.GetProperties(
                                 symbol.Type))
                        AddColumn(selectStatement, symbol, columnList, columnDictionary, ref separator,
                            property.Name);
                }
            }
        }

        private void AddColumn(
            SqlSelectStatement selectStatement,
            Symbol symbol,
            List<Symbol> columnList,
            Dictionary<string, Symbol> columnDictionary,
            ref string separator,
            string columnName)
        {
            allColumnNames[columnName] = 0;
            Symbol s;
            if (!symbol.Columns.TryGetValue(columnName, out s))
            {
                s = new Symbol(columnName, null);
                symbol.Columns.Add(columnName, s);
            }

            selectStatement.Select.Append(separator);
            selectStatement.Select.Append(symbol);
            selectStatement.Select.Append(".");
            if (symbol.OutputColumnsRenamed)
            {
                selectStatement.Select.Append(s);
                selectStatement.OutputColumns.Add(s.Name, s);
            }
            else
                selectStatement.Select.Append(QuoteIdentifier(columnName));

            selectStatement.Select.Append(" AS ");
            selectStatement.Select.Append(s);
            if (columnDictionary.ContainsKey(columnName))
            {
                columnDictionary[columnName].NeedsRenaming = true;
                s.NeedsRenaming = true;
            }
            else
                columnDictionary[columnName] = symbol.Columns[columnName];

            columnList.Add(s);
            separator = ", ";
        }

        private List<Symbol> AddDefaultColumns(SqlSelectStatement selectStatement)
        {
            var columnList = new List<Symbol>();
            var columnDictionary =
                new Dictionary<string, Symbol>(StringComparer.OrdinalIgnoreCase);
            var separator = "";
            if (!selectStatement.Select.IsEmpty)
                separator = ", ";
            foreach (var fromExtent in selectStatement.FromExtents)
                AddColumns(selectStatement, fromExtent, columnList, columnDictionary, ref separator);
            return columnList;
        }

        private void AddFromSymbol(
            SqlSelectStatement selectStatement,
            string inputVarName,
            Symbol fromSymbol)
        {
            AddFromSymbol(selectStatement, inputVarName, fromSymbol, true);
        }

        private void AddFromSymbol(
            SqlSelectStatement selectStatement,
            string inputVarName,
            Symbol fromSymbol,
            bool addToSymbolTable)
        {
            if (selectStatement.FromExtents.Count == 0 || fromSymbol != selectStatement.FromExtents[0])
            {
                selectStatement.FromExtents.Add(fromSymbol);
                selectStatement.From.Append(" ");
                selectStatement.From.Append(fromSymbol);
                allExtentNames[fromSymbol.Name] = 0;
            }

            if (!addToSymbolTable)
                return;
            symbolTable.Add(inputVarName, fromSymbol);
        }

        private void AddSortKeys(SqlBuilder orderByClause, IList<DbSortClause> sortKeys)
        {
            var s = "";
            foreach (var sortKey in sortKeys)
            {
                orderByClause.Append(s);
                orderByClause.Append(
                    sortKey.Expression.Accept(this));
                if (!string.IsNullOrEmpty(sortKey.Collation))
                {
                    orderByClause.Append(" COLLATE ");
                    orderByClause.Append(sortKey.Collation);
                }

                orderByClause.Append(sortKey.Ascending ? " ASC" : (object)" DESC");
                s = ", ";
            }
        }

        private SqlSelectStatement CreateNewSelectStatement(
            SqlSelectStatement oldStatement,
            string inputVarName,
            TypeUsage inputVarType,
            out Symbol fromSymbol)
        {
            return CreateNewSelectStatement(oldStatement, inputVarName, inputVarType, true, out fromSymbol);
        }

        private SqlSelectStatement CreateNewSelectStatement(
            SqlSelectStatement oldStatement,
            string inputVarName,
            TypeUsage inputVarType,
            bool finalizeOldStatement,
            out Symbol fromSymbol)
        {
            fromSymbol = null;
            if (finalizeOldStatement && oldStatement.Select.IsEmpty)
            {
                var symbolList = AddDefaultColumns(oldStatement);
                if (oldStatement.FromExtents[0] is JoinSymbol fromExtent)
                    fromSymbol = new JoinSymbol(inputVarName, inputVarType, fromExtent.ExtentList)
                    {
                        IsNestedJoin = true,
                        ColumnList = symbolList,
                        FlattenedExtentList = fromExtent.FlattenedExtentList
                    };
            }

            if (fromSymbol == null)
                fromSymbol = !oldStatement.OutputColumnsRenamed
                    ? new Symbol(inputVarName, inputVarType)
                    : new Symbol(inputVarName, inputVarType, oldStatement.OutputColumns);
            var newSelectStatement = new SqlSelectStatement();
            newSelectStatement.From.Append("( ");
            newSelectStatement.From.Append(oldStatement);
            newSelectStatement.From.AppendLine();
            newSelectStatement.From.Append(") ");
            return newSelectStatement;
        }

        private static string EscapeSingleQuote(string s, bool isUnicode)
        {
            return "'" + s.Replace("'", "''") + "'";
        }

        internal string GetSqlPrimitiveType(TypeUsage type)
        {
            return GetSqlPrimitiveType(_providerManifest, _sqlVersion, type);
        }

        internal static string GetSqlPrimitiveType(
            DbProviderManifest providerManifest,
            AdsStoreVersion sqlVersion,
            TypeUsage type)
        {
            var storeType = providerManifest.GetStoreType(type);
            var sqlPrimitiveType = storeType.EdmType.Name;
            var maxLength = 0;
            byte precision = 0;
            byte scale = 0;
            switch ((int)((PrimitiveType)storeType.EdmType).PrimitiveTypeKind)
            {
                case 0:
                    if (!MetadataHelpers.IsFacetValueConstant(storeType, "MaxLength"))
                    {
                        sqlPrimitiveType = "blob";
                    }

                    break;
                case 1:
                    sqlPrimitiveType = "logical";
                    break;
                case 3:
                    sqlPrimitiveType = "date";
                    break;
                case 4:
                    if (!MetadataHelpers.IsFacetValueConstant(storeType, "Precision"))
                    {
                        MetadataHelpers.TryGetPrecision(storeType, out precision);
                        MetadataHelpers.TryGetScale(storeType, out scale);
                        sqlPrimitiveType = sqlPrimitiveType + "(" + precision + "," + scale + ")";
                    }

                    break;
                case 5:
                case 7:
                    sqlPrimitiveType = "double";
                    break;
                case 6:
                    sqlPrimitiveType = "raw( 16 )";
                    break;
                case 12:
                    if (!MetadataHelpers.IsFacetValueConstant(storeType, "MaxLength"))
                    {
                        MetadataHelpers.TryGetMaxLength(storeType, out maxLength);
                        sqlPrimitiveType = sqlPrimitiveType + "(" +
                                           maxLength.ToString(CultureInfo.InvariantCulture) + ")";
                    }

                    break;
                case 13:
                    sqlPrimitiveType = "time";
                    break;
                case 14:
                    sqlPrimitiveType = "timestamp";
                    break;
            }

            return sqlPrimitiveType;
        }

        private ISqlFragment HandleCountExpression(DbExpression e)
        {
            ISqlFragment sqlFragment;
            if (e.ExpressionKind == (DbExpressionKind)5)
            {
                var sqlBuilder = new SqlBuilder();
                sqlBuilder.Append(((DbConstantExpression)e).Value.ToString());
                sqlFragment = sqlBuilder;
            }
            else
                sqlFragment = e.Accept(this);

            return sqlFragment;
        }

        private static bool IsApplyExpression(DbExpression e)
        {
            return 6 == (int)e.ExpressionKind || 42 == (int)e.ExpressionKind;
        }

        private static bool IsJoinExpression(DbExpression e)
        {
            return 7 == (int)e.ExpressionKind || 16 == (int)e.ExpressionKind || 21 == (int)e.ExpressionKind ||
                   27 == (int)e.ExpressionKind;
        }

        private static bool IsComplexExpression(DbExpression e)
        {
            var expressionKind = e.ExpressionKind;
            return expressionKind != (DbExpressionKind)5 && expressionKind != (DbExpressionKind)43 &&
                   expressionKind != (DbExpressionKind)46;
        }

        private static bool IsCompatible(SqlSelectStatement result, DbExpressionKind expressionKind)
        {
            var dbExpressionKind = expressionKind;
            if (dbExpressionKind <= (DbExpressionKind)20)
            {
                switch (((int)dbExpressionKind) - 9)
                {
                    case 0:
                        return result.Top == null && result.OrderBy.IsEmpty;
                    case 1:
                        goto label_26;
                    case 2:
                        break;
                    default:
                        if (dbExpressionKind != (DbExpressionKind)15)
                        {
                            if (dbExpressionKind == (DbExpressionKind)20)
                                return result.Select.IsEmpty && result.GroupBy.IsEmpty && result.OrderBy.IsEmpty &&
                                       result.Top == null;
                            goto label_26;
                        }

                        return result.Select.IsEmpty && result.Where.IsEmpty && result.GroupBy.IsEmpty &&
                               result.Top == null;
                }
            }
            else if (dbExpressionKind != (DbExpressionKind)31)
            {
                if (dbExpressionKind != (DbExpressionKind)45)
                {
                    switch ((int)dbExpressionKind - 51)
                    {
                        case 0:
                            return result.Select.IsEmpty && result.GroupBy.IsEmpty && result.OrderBy.IsEmpty &&
                                   !result.IsDistinct;
                        case 1:
                            return result.Select.IsEmpty && result.GroupBy.IsEmpty && result.OrderBy.IsEmpty &&
                                   !result.IsDistinct;
                        default:
                            goto label_26;
                    }
                }

                return result.Select.IsEmpty && result.GroupBy.IsEmpty && !result.IsDistinct;
            }

            return result.Top == null;
            label_26:
            throw new InvalidOperationException(string.Empty);
        }

        internal static string QuoteIdentifier(string name) => "\"" + name.Replace("\"", "\"\"") + "\"";

        private SqlSelectStatement VisitExpressionEnsureSqlStatement(DbExpression e)
        {
            return VisitExpressionEnsureSqlStatement(e, true);
        }

        private SqlSelectStatement VisitExpressionEnsureSqlStatement(
            DbExpression e,
            bool addDefaultColumns)
        {
            var expressionKind1 = e.ExpressionKind;
            if (expressionKind1 <= (DbExpressionKind)20)
            {
                if (expressionKind1 != (DbExpressionKind)15 && expressionKind1 != (DbExpressionKind)20)
                    goto label_4;
            }
            else if (expressionKind1 != (DbExpressionKind)45 && expressionKind1 != (DbExpressionKind)52)
                goto label_4;

            var selectStatement =
                e.Accept(this) as SqlSelectStatement;
            goto label_11;
            label_4:
            var inputVarName = "c";
            symbolTable.EnterScope();
            var expressionKind2 = e.ExpressionKind;
            if (expressionKind2 <= (DbExpressionKind)21)
            {
                switch ((int)expressionKind2 - 6)
                {
                    case 0:
                    case 1:
                        break;
                    default:
                        if (expressionKind2 == (DbExpressionKind)16 || expressionKind2 == (DbExpressionKind)21)
                            break;
                        goto label_9;
                }
            }
            else if (expressionKind2 != (DbExpressionKind)27 && expressionKind2 != (DbExpressionKind)42 &&
                     expressionKind2 != (DbExpressionKind)50)
                goto label_9;

            var inputVarType = MetadataHelpers.GetElementTypeUsage(e.ResultType);
            goto label_10;
            label_9:
            inputVarType = MetadataHelpers.GetEdmType<CollectionType>(e.ResultType).TypeUsage;
            label_10:
            Symbol fromSymbol;
            selectStatement = VisitInputExpression(e, inputVarName, inputVarType, out fromSymbol);
            AddFromSymbol(selectStatement, inputVarName, fromSymbol);
            symbolTable.ExitScope();
            label_11:
            if (addDefaultColumns && selectStatement.Select.IsEmpty)
                AddDefaultColumns(selectStatement);
            return selectStatement;
        }

        private SqlSelectStatement VisitFilterExpression(
            DbExpressionBinding input,
            DbExpression predicate,
            bool negatePredicate)
        {
            Symbol fromSymbol;
            var sqlSelectStatement = VisitInputExpression(input.Expression, input.VariableName,
                input.VariableType, out fromSymbol);
            if (!IsCompatible(sqlSelectStatement, (DbExpressionKind)15))
                sqlSelectStatement = CreateNewSelectStatement(sqlSelectStatement, input.VariableName,
                    input.VariableType, out fromSymbol);
            selectStatementStack.Push(sqlSelectStatement);
            symbolTable.EnterScope();
            AddFromSymbol(sqlSelectStatement, input.VariableName, fromSymbol);
            if (negatePredicate)
                sqlSelectStatement.Where.Append("NOT (");
            sqlSelectStatement.Where.Append(
                predicate.Accept(this));
            if (negatePredicate)
                sqlSelectStatement.Where.Append(")");
            symbolTable.ExitScope();
            selectStatementStack.Pop();
            return sqlSelectStatement;
        }

        private static void WrapNonQueryExtent(
            SqlSelectStatement result,
            ISqlFragment sqlFragment,
            DbExpressionKind expressionKind)
        {
            if (expressionKind == (DbExpressionKind)17)
            {
                result.From.Append(sqlFragment);
            }
            else
            {
                result.From.Append(" (");
                result.From.Append(sqlFragment);
                result.From.Append(")");
            }
        }

        private static bool IsBuiltInStoreFunction(EdmFunction function)
        {
            return MetadataHelpers.GetMetadataProperty<bool>(function, "BuiltInAttribute") &&
                   !MetadataHelpers.IsCanonicalFunction(function);
        }

        private static string ByteArrayToBinaryString(byte[] binaryArray)
        {
            var stringBuilder = new StringBuilder(binaryArray.Length * 2);
            for (var index = 0; index < binaryArray.Length; ++index)
                stringBuilder.Append(hexDigits[(binaryArray[index] & 240) >> 4])
                    .Append(hexDigits[binaryArray[index] & 15]);
            return stringBuilder.ToString();
        }

        private static bool GroupByAggregatesNeedInnerQuery(IList<DbAggregate> aggregates)
        {
            foreach (var aggregate in aggregates)
            {
                if (GroupByAggregateNeedsInnerQuery(aggregate.Arguments[0]))
                    return true;
            }

            return false;
        }

        private static bool GroupByAggregateNeedsInnerQuery(DbExpression expression)
        {
            if (expression.ExpressionKind == (DbExpressionKind)5)
                return false;
            if (expression.ExpressionKind == (DbExpressionKind)4)
                return GroupByAggregateNeedsInnerQuery(((DbUnaryExpression)expression).Argument);
            if (expression.ExpressionKind == (DbExpressionKind)46)
                return GroupByAggregateNeedsInnerQuery(((DbPropertyExpression)expression).Instance);
            return expression.ExpressionKind != (DbExpressionKind)56;
        }

        private static bool GroupByKeysNeedInnerQuery(IList<DbExpression> keys, string inputVarRefName)
        {
            foreach (var key in keys)
            {
                if (GroupByKeyNeedsInnerQuery(key, inputVarRefName))
                    return true;
            }

            return false;
        }

        private static bool GroupByKeyNeedsInnerQuery(DbExpression expression, string inputVarRefName)
        {
            if (expression.ExpressionKind == (DbExpressionKind)4)
                return GroupByKeyNeedsInnerQuery(((DbUnaryExpression)expression).Argument,
                    inputVarRefName);
            if (expression.ExpressionKind == (DbExpressionKind)46)
                return GroupByKeyNeedsInnerQuery(((DbPropertyExpression)expression).Instance,
                    inputVarRefName);
            return expression.ExpressionKind != (DbExpressionKind)56 ||
                   !(expression as DbVariableReferenceExpression).VariableName.Equals(inputVarRefName);
        }

        private delegate ISqlFragment FunctionHandler(
            SqlGenerator sqlgen,
            DbFunctionExpression functionExpr);
    }
}