namespace Sitecore.Feature.Blog.Extensions
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using Expressions;

    public static class ExpressionExtensions
    {
        /// <summary>
        /// Rewrites a given expression using the <see cref="ParameterReplaceVisitor"/> Expression Visitor
        /// by replacing the second argument of the initial expression with a constant value
        /// </summary>
        /// <param name="expression">Original Expression</param>
        /// <param name="value">Value to replace the 2nd parameter with</param>
        /// <returns>Rewritten Expression</returns>
        public static Expression<Func<T, bool>> Rewrite<T, TR>(this Expression<Func<T, TR, bool>> expression, TR value)
        {
            return
                Expression.Lambda<Func<T, bool>>(
                    new ParameterReplaceVisitor(expression.Parameters[1], value).Visit(expression.Body), expression.Parameters[0]);
        }

        /// <summary>
        /// Converts the expression into a PropertyInfo
        /// </summary>
        /// <param name="expression">The Expression</param>
        /// <exception cref="ArgumentException">Expression cannot refer to a method</exception>
        /// <exception cref="ArgumentException">Expression cannot refer to a field</exception>
        /// <returns><see cref="PropertyInfo"/> of an expression</returns>
        public static PropertyInfo ToPropertyInfo<T, TProperty>(this Expression<Func<T, TProperty>> expression)
        {
            if (expression == null)
            {
                return null;
            }

            var member = expression.Body as MemberExpression;
            if (member == null)
            {
                throw new ArgumentException($"Expression '{expression}' refers to a method, not a property.");
            }

            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
            {
                throw new ArgumentException($"Expression '{expression}' refers to a field, not a property.");
            }

            return propInfo;
        }
    }
}