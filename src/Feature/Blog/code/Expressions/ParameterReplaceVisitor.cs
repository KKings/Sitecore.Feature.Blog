namespace Sitecore.Feature.Blog.Expressions
{
    using System.Linq.Expressions;

    public class ParameterReplaceVisitor : ExpressionVisitor
    {
        /// <summary>
        /// Parameter Expression to be replaced with the Constant
        /// </summary>
        private readonly ParameterExpression parameterExpression;

        /// <summary>
        /// Constant Expression that will replace the Parameter Expression
        /// </summary>
        private readonly ConstantExpression constantExpression;

        public ParameterReplaceVisitor(ParameterExpression expression, object value)
        {
            this.parameterExpression = expression;
            this.constantExpression = Expression.Constant(value);
        }

        /// <summary>
        /// Replaces the Parameter Expression with a constant expression
        /// </summary>
        /// <param name="expression">Parameters within the Expression</param>
        /// <returns><c>The Constant Expression</c> if the parameter matches the designated expression</returns>
        protected override Expression VisitParameter(ParameterExpression expression)
        {
            if (expression == this.parameterExpression)
            {
                return this.constantExpression;
            }

            return expression;
        }
    }
}