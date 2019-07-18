using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace SAC.Models
{
    internal class WhereBuilder<T>
    {
        private Expression<Func<T, bool>> internalExpression;
        private bool isSet;

        public WhereBuilder()
        {
            //internalExpression = new Expression();
            this.isSet = false;
        }

        public void SetWhere(Expression<Func<T, bool>> expresion) {
            this.internalExpression = expresion;
            this.isSet = true;
        }

        public void Or(Expression<Func<T, bool>> expresion) {
            this.internalExpression = this.internalExpression.Or(expresion);
        }

        public void And(Expression<Func<T, bool>> expresion)
        {
            this.internalExpression = this.internalExpression.And(expresion);
        }

        public void SetOr(Expression<Func<T, bool>> expresion)
        {
            if (this.isSet)
            {
                this.internalExpression = this.internalExpression.Or(expresion);
            }
            else
            {
                this.internalExpression = expresion;
                this.isSet = true;
            }
            
        }

        public void SetAnd(Expression<Func<T, bool>> expresion)
        {
            if (this.isSet)
            {
                this.internalExpression = this.internalExpression.And(expresion);
            }
            else
            {
                this.internalExpression = expresion;
                this.isSet = true;
            }
        }

        public Expression<Func<T, bool>> GetWhere() {
            if (this.isSet)
            {
                return this.internalExpression;
            }
            else {
                return TexpressionCustomValueAlwaysTrue=>true; 
            }
        }
    }





    public class ParameterRebinder : ExpressionVisitor
    {

        private readonly Dictionary<ParameterExpression, ParameterExpression> map;



        public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
        {

            this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();

        }



        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
        {

            return new ParameterRebinder(map).Visit(exp);

        }



        protected override Expression VisitParameter(ParameterExpression p)
        {

            ParameterExpression replacement;

            if (map.TryGetValue(p, out replacement))
            {

                p = replacement;

            }

            return base.VisitParameter(p);

        }

    }

    public static class Utility
    {

        public static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {

            // build parameter map (from parameters of second to parameters of first)

            var map = first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);



            // replace parameters in the second lambda expression with parameters from the first

            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);



            // apply composition of lambda expression bodies to parameters from the first expression 

            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);

        }



        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {

            return first.Compose(second, Expression.And);

        }



        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {

            return first.Compose(second, Expression.Or);

        }

    }


}