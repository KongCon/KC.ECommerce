﻿using System;
using System.Linq.Expressions;

namespace KC.ECommerce.Common
{
    public static partial class ExpressionExtensions
    {
        /// <summary>
        /// 使用 and 拼接两个 lambda 表达式
        /// </summary>
        /// <returns></returns>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> exp1, Expression<Func<T, bool>> exp2) => And(exp1, true, exp2);
        /// <summary>
        /// 使用 and 拼接两个 lambda 表达式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exp1"></param>
        /// <param name="condition">true 时生效</param>
        /// <param name="exp2"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> exp1, bool condition, Expression<Func<T, bool>> exp2)
        {
            if (condition == false) return exp1;
            if (exp1 == null) return exp2;
            if (exp2 == null) return exp1;

            ParameterExpression newParameter = Expression.Parameter(typeof(T), "c");
            NewExpressionVisitor visitor = new NewExpressionVisitor(newParameter);

            var left = visitor.Replace(exp1.Body);
            var right = visitor.Replace(exp2.Body);
            var body = Expression.AndAlso(left, right);
            return Expression.Lambda<Func<T, bool>>(body, newParameter);
        }

        /// <summary>
        /// 使用 or 拼接两个 lambda 表达式
        /// </summary>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> exp1, Expression<Func<T, bool>> exp2) => Or(exp1, true, exp2);
        /// <summary>
        /// 使用 or 拼接两个 lambda 表达式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exp1"></param>
        /// <param name="condition">true 时生效</param>
        /// <param name="exp2"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> exp1, bool condition, Expression<Func<T, bool>> exp2)
        {
            if (condition == false) return exp1;
            if (exp1 == null) return exp2;
            if (exp2 == null) return exp1;

            ParameterExpression newParameter = Expression.Parameter(typeof(T), "c");
            NewExpressionVisitor visitor = new NewExpressionVisitor(newParameter);

            var left = visitor.Replace(exp1.Body);
            var right = visitor.Replace(exp2.Body);
            var body = Expression.OrElse(left, right);
            return Expression.Lambda<Func<T, bool>>(body, newParameter);
        }

        /// <summary>
        /// 将 lambda 表达式取反
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exp"></param>
        /// <param name="condition">true 时生效</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> exp, bool condition = true)
        {
            if (condition == false) return exp;
            if (exp == null) return null;

            var candidateExpr = exp.Parameters[0];
            var body = Expression.Not(exp.Body);
            return Expression.Lambda<Func<T, bool>>(body, candidateExpr);
        }
    }

    internal class NewExpressionVisitor : ExpressionVisitor
    {
        public ParameterExpression _newParameter { get; private set; }
        public NewExpressionVisitor(ParameterExpression param)
        {
            this._newParameter = param;
        }
        public Expression Replace(Expression exp)
        {
            return this.Visit(exp);
        }
        protected override Expression VisitParameter(ParameterExpression node)
        {
            return this._newParameter;
        }
    }

}
