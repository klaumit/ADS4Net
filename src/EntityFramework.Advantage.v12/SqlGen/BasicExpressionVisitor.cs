using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Metadata.Edm;

namespace Advantage.Data.Provider.SqlGen
{
    internal abstract class BasicExpressionVisitor : DbExpressionVisitor
    {
        protected virtual void VisitUnaryExpression(DbUnaryExpression expression)
        {
            VisitExpression(EntityUtils.CheckArgumentNull(expression, nameof(expression))
                .Argument);
        }

        protected virtual void VisitBinaryExpression(DbBinaryExpression expression)
        {
            EntityUtils.CheckArgumentNull(expression, nameof(expression));
            VisitExpression(expression.Left);
            VisitExpression(expression.Right);
        }

        protected virtual void VisitExpressionBindingPre(DbExpressionBinding binding)
        {
            EntityUtils.CheckArgumentNull(binding, nameof(binding));
            VisitExpression(binding.Expression);
        }

        protected virtual void VisitExpressionBindingPost(DbExpressionBinding binding)
        {
        }

        protected virtual void VisitGroupExpressionBindingPre(DbGroupExpressionBinding binding)
        {
            EntityUtils.CheckArgumentNull(binding, nameof(binding));
            VisitExpression(binding.Expression);
        }

        protected virtual void VisitGroupExpressionBindingMid(DbGroupExpressionBinding binding)
        {
        }

        protected virtual void VisitGroupExpressionBindingPost(DbGroupExpressionBinding binding)
        {
        }

        protected virtual void VisitLambdaFunctionPre(EdmFunction function, DbExpression body)
        {
            EntityUtils.CheckArgumentNull(function, nameof(function));
            EntityUtils.CheckArgumentNull(body, nameof(body));
        }

        protected virtual void VisitLambdaFunctionPost(EdmFunction function, DbExpression body)
        {
        }

        public virtual void VisitExpression(DbExpression expression)
        {
            EntityUtils.CheckArgumentNull(expression, nameof(expression))
                .Accept(this);
        }

        public virtual void VisitExpressionList(IList<DbExpression> expressionList)
        {
            EntityUtils.CheckArgumentNull(expressionList, nameof(expressionList));
            for (var index = 0; index < expressionList.Count; ++index)
                VisitExpression(expressionList[index]);
        }

        public virtual void VisitAggregateList(IList<DbAggregate> aggregates)
        {
            EntityUtils.CheckArgumentNull(aggregates, nameof(aggregates));
            for (var index = 0; index < aggregates.Count; ++index)
                VisitAggregate(aggregates[index]);
        }

        public virtual void VisitAggregate(DbAggregate aggregate)
        {
            VisitExpressionList(EntityUtils.CheckArgumentNull(aggregate, nameof(aggregate))
                .Arguments);
        }

        public override void Visit(DbExpression expression)
        {
            EntityUtils.CheckArgumentNull(expression, nameof(expression));
            throw new NotSupportedException(string.Format("The expression '{0}' is of an unsupported type.",
                expression.GetType().FullName));
        }

        public override void Visit(DbConstantExpression expression)
        {
            EntityUtils.CheckArgumentNull(expression, nameof(expression));
        }

        public override void Visit(DbNullExpression expression)
        {
            EntityUtils.CheckArgumentNull(expression, nameof(expression));
        }

        public override void Visit(DbVariableReferenceExpression expression)
        {
            EntityUtils.CheckArgumentNull(expression, nameof(expression));
        }

        public override void Visit(DbParameterReferenceExpression expression)
        {
            EntityUtils.CheckArgumentNull(expression, nameof(expression));
        }

        public override void Visit(DbFunctionExpression expression)
        {
            EntityUtils.CheckArgumentNull(expression, nameof(expression));
            VisitExpressionList(expression.Arguments);
        }

        public override void Visit(DbPropertyExpression expression)
        {
            EntityUtils.CheckArgumentNull(expression, nameof(expression));
            if (expression.Instance == null)
                return;
            VisitExpression(expression.Instance);
        }

        public override void Visit(DbComparisonExpression expression)
        {
            VisitBinaryExpression(expression);
        }

        public override void Visit(DbLikeExpression expression)
        {
            EntityUtils.CheckArgumentNull(expression, nameof(expression));
            VisitExpression(expression.Argument);
            VisitExpression(expression.Pattern);
            VisitExpression(expression.Escape);
        }

        public override void Visit(DbLimitExpression expression)
        {
            EntityUtils.CheckArgumentNull(expression, nameof(expression));
            VisitExpression(expression.Argument);
            VisitExpression(expression.Limit);
        }

        public override void Visit(DbIsNullExpression expression)
        {
            VisitUnaryExpression(expression);
        }

        public override void Visit(DbArithmeticExpression expression)
        {
            VisitExpressionList(EntityUtils
                .CheckArgumentNull(expression, nameof(expression)).Arguments);
        }

        public override void Visit(DbAndExpression expression)
        {
            VisitBinaryExpression(expression);
        }

        public override void Visit(DbOrExpression expression)
        {
            VisitBinaryExpression(expression);
        }

        public override void Visit(DbNotExpression expression)
        {
            VisitUnaryExpression(expression);
        }

        public override void Visit(DbDistinctExpression expression)
        {
            VisitUnaryExpression(expression);
        }

        public override void Visit(DbElementExpression expression)
        {
            VisitUnaryExpression(expression);
        }

        public override void Visit(DbIsEmptyExpression expression)
        {
            VisitUnaryExpression(expression);
        }

        public override void Visit(DbUnionAllExpression expression)
        {
            VisitBinaryExpression(expression);
        }

        public override void Visit(DbIntersectExpression expression)
        {
            VisitBinaryExpression(expression);
        }

        public override void Visit(DbExceptExpression expression)
        {
            VisitBinaryExpression(expression);
        }

        public override void Visit(DbOfTypeExpression expression)
        {
            VisitUnaryExpression(expression);
        }

        public override void Visit(DbTreatExpression expression)
        {
            VisitUnaryExpression(expression);
        }

        public override void Visit(DbCastExpression expression)
        {
            VisitUnaryExpression(expression);
        }

        public override void Visit(DbIsOfExpression expression)
        {
            VisitUnaryExpression(expression);
        }

        public override void Visit(DbCaseExpression expression)
        {
            EntityUtils.CheckArgumentNull(expression, nameof(expression));
            VisitExpressionList(expression.When);
            VisitExpressionList(expression.Then);
            VisitExpression(expression.Else);
        }

        public override void Visit(DbRefExpression expression)
        {
            VisitUnaryExpression(expression);
        }

        public override void Visit(DbRelationshipNavigationExpression expression)
        {
            VisitExpression(EntityUtils
                .CheckArgumentNull(expression, nameof(expression))
                .NavigationSource);
        }

        public override void Visit(DbDerefExpression expression)
        {
            VisitUnaryExpression(expression);
        }

        public override void Visit(DbRefKeyExpression expression)
        {
            VisitUnaryExpression(expression);
        }

        public override void Visit(DbEntityRefExpression expression)
        {
            VisitUnaryExpression(expression);
        }

        public override void Visit(DbScanExpression expression)
        {
            EntityUtils.CheckArgumentNull(expression, nameof(expression));
        }

        public override void Visit(DbFilterExpression expression)
        {
            EntityUtils.CheckArgumentNull(expression, nameof(expression));
            VisitExpressionBindingPre(expression.Input);
            VisitExpression(expression.Predicate);
            VisitExpressionBindingPost(expression.Input);
        }

        public override void Visit(DbProjectExpression expression)
        {
            EntityUtils.CheckArgumentNull(expression, nameof(expression));
            VisitExpressionBindingPre(expression.Input);
            VisitExpression(expression.Projection);
            VisitExpressionBindingPost(expression.Input);
        }

        public override void Visit(DbCrossJoinExpression expression)
        {
            EntityUtils.CheckArgumentNull(expression, nameof(expression));
            foreach (var input in expression.Inputs)
                VisitExpressionBindingPre(input);
            foreach (var input in expression.Inputs)
                VisitExpressionBindingPost(input);
        }

        public override void Visit(DbJoinExpression expression)
        {
            EntityUtils.CheckArgumentNull(expression, nameof(expression));
            VisitExpressionBindingPre(expression.Left);
            VisitExpressionBindingPre(expression.Right);
            VisitExpression(expression.JoinCondition);
            VisitExpressionBindingPost(expression.Left);
            VisitExpressionBindingPost(expression.Right);
        }

        public override void Visit(DbApplyExpression expression)
        {
            EntityUtils.CheckArgumentNull(expression, nameof(expression));
            VisitExpressionBindingPre(expression.Input);
            if (expression.Apply != null)
                VisitExpression(expression.Apply.Expression);
            VisitExpressionBindingPost(expression.Input);
        }

        public override void Visit(DbGroupByExpression expression)
        {
            EntityUtils.CheckArgumentNull(expression, nameof(expression));
            VisitGroupExpressionBindingPre(expression.Input);
            VisitExpressionList(expression.Keys);
            VisitGroupExpressionBindingMid(expression.Input);
            VisitAggregateList(expression.Aggregates);
            VisitGroupExpressionBindingPost(expression.Input);
        }

        public override void Visit(DbSkipExpression expression)
        {
            EntityUtils.CheckArgumentNull(expression, nameof(expression));
            VisitExpressionBindingPre(expression.Input);
            foreach (var dbSortClause in expression.SortOrder)
                VisitExpression(dbSortClause.Expression);
            VisitExpressionBindingPost(expression.Input);
            VisitExpression(expression.Count);
        }

        public override void Visit(DbSortExpression expression)
        {
            EntityUtils.CheckArgumentNull(expression, nameof(expression));
            VisitExpressionBindingPre(expression.Input);
            for (var index = 0; index < expression.SortOrder.Count; ++index)
                VisitExpression(expression.SortOrder[index].Expression);
            VisitExpressionBindingPost(expression.Input);
        }

        public override void Visit(DbQuantifierExpression expression)
        {
            EntityUtils.CheckArgumentNull(expression, nameof(expression));
            VisitExpressionBindingPre(expression.Input);
            VisitExpression(expression.Predicate);
            VisitExpressionBindingPost(expression.Input);
        }
    }
}