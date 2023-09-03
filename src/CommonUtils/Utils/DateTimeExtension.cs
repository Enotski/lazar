using System.Linq.Expressions;

namespace CommonUtils.Utils {
    /// <summary>
    /// Helper class for operations with <see cref="DateTime" />
    /// </summary>
    public static class DateTimeExtension {
        /// <summary>
        /// Expression to select records in a range of dates
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">Predicate expression</param>
        /// <param name="getDate"><see cref="DateTime" /> property</param>
        /// <param name="fromDate">Lower bound</param>
        /// <param name="toDate">Upper bound</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> WhereDateBetween<T>(this Expression<Func<T, bool>> expression, Expression<Func<T, DateTime>> getDate, string fromDate, string toDate) {
            if (string.IsNullOrEmpty(fromDate) && string.IsNullOrEmpty(toDate))
                return expression;  // The simplest query is no query

            DateTime tmp;
            DateTime? tmpTo = null;
            if (!string.IsNullOrEmpty(toDate) && DateTime.TryParse(toDate, out tmp)) {
                tmpTo = tmp.AddDays(1);
            }

            DateTime? tmpFrom = null;
            if (!string.IsNullOrEmpty(fromDate) && DateTime.TryParse(fromDate, out tmp)) {
                tmpFrom = tmp;
            }
            return expression.And(getDate.Chain(DateBetween(tmpFrom, tmpTo)));
        }
        /// <summary>
        /// Expression to select records in a range of dates
        /// </summary>
        /// <param name="fromDate">Lower bound</param>
        /// <param name="toDate">Upper bound</param>
        /// <returns></returns>
        private static Expression<Func<DateTime, bool>> DateBetween(DateTime? fromDate, DateTime? toDate) {
            if (!toDate.HasValue && !fromDate.HasValue) {
                return date => true;
            }

            if (toDate == null) {
                return date => fromDate <= date;
            }

            if (fromDate == null) {
                return date => toDate >= date;
            }

            return date => fromDate <= date && toDate >= date;
        }
        public static Expression<Func<TIn, TOut>> Chain<TIn, TInterstitial, TOut>(this Expression<Func<TIn, TInterstitial>> inner, Expression<Func<TInterstitial, TOut>> outer) {
            var visitor = new SwapVisitor(outer.Parameters[0], inner.Body);
            return Expression.Lambda<Func<TIn, TOut>>(visitor.Visit(outer.Body), inner.Parameters);
        }
        internal class SwapVisitor : ExpressionVisitor {
            private readonly Expression _source, _replacement;

            public SwapVisitor(Expression source, Expression replacement) {
                _source = source;
                _replacement = replacement;
            }

            public override Expression Visit(Expression node) {
                return node == _source ? _replacement : base.Visit(node);
            }
        }
    }
}