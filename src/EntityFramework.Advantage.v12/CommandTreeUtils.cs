using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;

namespace Advantage.Data.Provider
{
    internal class CommandTreeUtils
    {
        private static readonly HashSet<DbExpressionKind> _associativeExpressionKinds = new HashSet<DbExpressionKind>(
            new DbExpressionKind[4]
            {
                (DbExpressionKind)41,
                (DbExpressionKind)1,
                (DbExpressionKind)44,
                (DbExpressionKind)34
            });

        internal static IEnumerable<DbExpression> FlattenAssociativeExpression(DbExpression expression)
        {
            return FlattenAssociativeExpression(expression.ExpressionKind, expression);
        }

        internal static IEnumerable<DbExpression> FlattenAssociativeExpression(
            DbExpressionKind expressionKind,
            params DbExpression[] arguments)
        {
            if (!_associativeExpressionKinds.Contains(expressionKind))
                return arguments;
            var argumentList = new List<DbExpression>();
            foreach (var expression in arguments)
                ExtractAssociativeArguments(expressionKind, argumentList, expression);
            return argumentList;
        }

        private static void ExtractAssociativeArguments(
            DbExpressionKind expressionKind,
            List<DbExpression> argumentList,
            DbExpression expression)
        {
            if (expression.ExpressionKind != expressionKind)
                argumentList.Add(expression);
            else if (expression is DbBinaryExpression binaryExpression)
            {
                ExtractAssociativeArguments(expressionKind, argumentList, binaryExpression.Left);
                ExtractAssociativeArguments(expressionKind, argumentList, binaryExpression.Right);
            }
            else
            {
                var arithmeticExpression = (DbArithmeticExpression)expression;
                ExtractAssociativeArguments(expressionKind, argumentList,
                    arithmeticExpression.Arguments[0]);
                ExtractAssociativeArguments(expressionKind, argumentList,
                    arithmeticExpression.Arguments[1]);
            }
        }
    }
}