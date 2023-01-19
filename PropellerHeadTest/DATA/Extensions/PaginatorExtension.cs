using DATA.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DATA.Extensions
{

    public static class PaginatorExtension
    {
        private static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string columnName, bool isDescending = true)
        {
            if (String.IsNullOrEmpty(columnName))
            {
                return source;
            }
            var props = columnName.Split('.');
            Type type = typeof(T);
            ParameterExpression arg = Expression.Parameter(type, "");
            Expression expr = arg;
            foreach (string prop in props)
            {
                PropertyInfo pi = type.GetProperty(prop) ?? type.GetProperty("Id");
                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }

            LambdaExpression lambda = Expression.Lambda(expr, arg);

            string methodName = isDescending ? "OrderByDescending" : "OrderBy";

            Expression methodCallExpression = Expression.Call(typeof(Queryable), methodName,
                                  new Type[] { source.ElementType, expr.Type },
                                  source.Expression, Expression.Quote(lambda));

            return source.Provider.CreateQuery<T>(methodCallExpression);
        }
        private static IQueryable<T> WhereEquals<T>(this IQueryable<T> source, string member, object value)
        {
            var item = Expression.Parameter(typeof(T), "item");
            var memberValue = member.Split('.').Aggregate((Expression)item, Expression.PropertyOrField);
            var memberType = memberValue.Type;
            if (value != null && value.GetType() != memberType)
            {
                value = Convert.ChangeType(value, memberType);
            }
            var condition = Expression.Equal(memberValue, Expression.Constant(value, memberType));
            var predicate = Expression.Lambda<Func<T, bool>>(condition, item);
            return source.Where(predicate);
        }
        private static IQueryable<T> WhereContains<T>(this IQueryable<T> source, string member, object value)
        {

            var toToLower = typeof(string).GetMethod("ToLower", Type.EmptyTypes);

            var item = Expression.Parameter(typeof(T), "item");
            var memberValue = member.Split('.').Aggregate((Expression)item, Expression.PropertyOrField);
            var memberType = memberValue.Type;
            MethodCallExpression memberValueExp = null;

            if (memberType != typeof(string))
            {

                var toString = memberType.GetMethod("ToString", Type.EmptyTypes);
                memberType = typeof(string);
                memberValueExp = Expression.Call(memberValue, toString);
            }

            memberValueExp = Expression.Call((memberValueExp == null ? memberValue : memberValueExp), toToLower);


            if (value != null && value.GetType() != memberType)
            {
                value = Convert.ChangeType(value, memberType);
            }
            var someValue = Expression.Constant(value.ToString().ToLower(), memberType);
            MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });

            var containsMethodExp = Expression.Call(memberValueExp, method, someValue);
            var predicate = Expression.Lambda<Func<T, bool>>(containsMethodExp, item);
            return source.Where(predicate);
        }
        private static IQueryable<T> WhereOrFields<T>(this IQueryable<T> source, string member, string member2, object value)
        {
            var item = Expression.Parameter(typeof(T), "item");
            var memberValue = member.Split('.').Aggregate((Expression)item, Expression.PropertyOrField);
            var memberValue2 = member2.Split('.').Aggregate((Expression)item, Expression.PropertyOrField);
            var memberType = memberValue.Type;
            if (value != null && value.GetType() != memberType)
            {
                value = Convert.ChangeType(value, memberType);
            }
            MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            var typeLower = typeof(string).GetMethod("ToLower", Type.EmptyTypes);
            var someValue = Expression.Constant(value.ToString().ToLower(), memberType);

            var memberValueLowerExp = Expression.Call(memberValue, typeLower);
            var memberValueLowerExp2 = Expression.Call(memberValue2, typeLower);

            Expression orElseExpr = Expression.OrElse(
                Expression.Call(memberValueLowerExp, method, someValue),
                Expression.Call(memberValueLowerExp2, method, someValue)
             );


            var lambdaExp = Expression.Lambda<Func<T, bool>>(orElseExpr, item);
            return source.Where(lambdaExp);
        }

        /// <summary>
        /// Method to filter and order entity collections
        /// </summary>
        /// <typeparam name="T">Entity Type To Filter</typeparam>
        /// <param name="query">Queryable of T</param>
        /// <param name="conditions">List of conditions</param>
        /// <param name="orderBy">Field used to order the entity</param>
        /// <param name="orderByDesc">Order specification</param>
        /// <param name="page">page number</param>
        /// <param name="pageSize">Quantity per page</param>
        /// <returns></returns>
        public static async Task<PaginationDto<T>> GetPagination<T>(IQueryable<T> query, List<PaginationConditionDto> conditions, string orderBy, bool orderByDesc, int page, int pageSize) where T : class
        {
            if (conditions != null && conditions.Count > 0)
            {
                foreach (var cond in conditions)
                {
                    if (cond.Method.ToLower().Equals("equals"))
                    {
                        query = query.WhereEquals(cond.Field, cond.Value);
                    }
                    else if (cond.Method.ToLower().Equals("or"))
                    {
                        query = query.WhereOrFields(cond.Field, cond.Field2, cond.Value);
                    }                  
                    else
                    {
                        query = query.WhereContains(cond.Field, cond.Value);
                    }

                }
            }

            PaginationDto<T> pagination = new PaginationDto<T>
            {
                TotalItems = query.Count(),
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)query.Count() / pageSize),
                CurrentPage = page,
                OrderBy = orderBy,
                OrderByDesc = orderByDesc
            };

            int skip = (page - 1) * pageSize;

            try
            {
                if (!String.IsNullOrEmpty(orderBy))
                {
                    pagination.Result = await query
                     .OrderBy(orderBy, orderByDesc)
                     .Skip(skip)
                     .Take(pageSize)
                     .ToListAsync();
                }
                else
                {
                    pagination.Result = query
                     .Skip(skip)
                     .Take(pageSize)
                     .ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return pagination;
        }
    }
}
