﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

using static Dapper.SqlMapper;
using Acesoft.Data.SqlMapper;

namespace Acesoft.Data
{
    public static class ISessionExtensions
    {
        public static SqlMap GetSqlMap(this ISession s, string sqlScope, string sqlId)
        {
            var mapper = MapperContainer.Instance.GetSqlMapper(s);
            return mapper.GetSqlMap(sqlScope, sqlId);
        }

        public static SqlMap GetSqlMap(this ISession s, string sqlFullId)
        {
            var items = sqlFullId.Split('.');
            return s.GetSqlMap(items[0], items[1]);
        }

        public static void FlushCache(this ISession s, string sqlFullId)
        {
            var mapper = MapperContainer.Instance.GetSqlMapper(s);
            mapper.CacheManager.Flush(sqlFullId);
        }

        #region execute
        public static int Execute(this ISession s, RequestContext ctx)
        {
            var mapper = MapperContainer.Instance.GetSqlMapper(s);
            return mapper.Execute(s, ctx);
        }

        public static Task<int> ExecuteAsync(this ISession s, RequestContext ctx)
        {
            var mapper = MapperContainer.Instance.GetSqlMapper(s);
            return mapper.ExecuteAsync(s, ctx);
        }

        public static object ExecuteScalar(this ISession s, RequestContext ctx)
        {
            var mapper = MapperContainer.Instance.GetSqlMapper(s);
            return mapper.ExecuteScalar(s, ctx);
        }

        public static Task<object> ExecuteScalarAsync(this ISession s, RequestContext ctx)
        {
            var mapper = MapperContainer.Instance.GetSqlMapper(s);
            return mapper.ExecuteScalarAsync(s, ctx);
        }

        public static T ExecuteScalar<T>(this ISession s, RequestContext ctx)
        {
            var mapper = MapperContainer.Instance.GetSqlMapper(s);
            return mapper.ExecuteScalar<T>(s, ctx);
        }

        public static Task<T> ExecuteScalarAsync<T>(this ISession s, RequestContext ctx)
        {
            var mapper = MapperContainer.Instance.GetSqlMapper(s);
            return mapper.ExecuteScalarAsync<T>(s, ctx);
        }
        #endregion

        #region query
        public static IEnumerable<dynamic> Query(this ISession s, RequestContext ctx)
        {
            var mapper = MapperContainer.Instance.GetSqlMapper(s);
            return mapper.Query(s, ctx);

        }
        public static IEnumerable<T> Query<T>(this ISession s, RequestContext ctx)
        {
            var mapper = MapperContainer.Instance.GetSqlMapper(s);
            return mapper.Query<T>(s, ctx);
        }

        public static IEnumerable<TReturn> Query<TFisrt, TSecond, TReturn>(this ISession s, RequestContext ctx, Func<TFisrt, TSecond, TReturn> map)
        {
            var mapper = MapperContainer.Instance.GetSqlMapper(s);
            return mapper.Query(s, ctx, map);
        }

        public static IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TReturn>(this ISession s, RequestContext ctx, Func<TFirst, TSecond, TThird, TReturn> map)
        {
            var mapper = MapperContainer.Instance.GetSqlMapper(s);
            return mapper.Query(s, ctx, map);
        }

        public static dynamic QueryFirst(this ISession s, RequestContext ctx)
        {
            var mapper = MapperContainer.Instance.GetSqlMapper(s);
            return mapper.QueryFirst(s, ctx);
        }

        public static T QueryFirst<T>(this ISession s, RequestContext ctx)
        {
            var mapper = MapperContainer.Instance.GetSqlMapper(s);
            return mapper.QueryFirst<T>(s, ctx);
        }

        public static dynamic QuerySingle(this ISession s, RequestContext ctx)
        {
            var mapper = MapperContainer.Instance.GetSqlMapper(s);
            return mapper.QuerySingle(s, ctx);
        }

        public static T QuerySingle<T>(this ISession s, RequestContext ctx)
        {
            var mapper = MapperContainer.Instance.GetSqlMapper(s);
            return mapper.QuerySingle<T>(s, ctx);
        }

        public static IDictionary<string, IEnumerable<dynamic>> QueryMultiple(this ISession s, RequestContext ctx)
        {
            var mapper = MapperContainer.Instance.GetSqlMapper(s);
            return mapper.QueryMultiple(s, ctx);
        }

        public static T QueryMultiple<T>(this ISession s, RequestContext ctx, Func<GridReader, T> func)
        {
            var mapper = MapperContainer.Instance.GetSqlMapper(s);
            return mapper.QueryMultiple(s, ctx, func);
        }

        public static DataTable QueryDataTable(this ISession s, RequestContext ctx)
        {
            var mapper = MapperContainer.Instance.GetSqlMapper(s);
            return mapper.QueryDataTable(s, ctx);
        }

        public static DataSet QueryDataSet(this ISession s, RequestContext ctx)
        {
            var mapper = MapperContainer.Instance.GetSqlMapper(s);
            return mapper.QueryDataSet(s, ctx);
        }

        public static GridResponse<dynamic> QueryPage(this ISession s, RequestContext ctx, GridRequest request = null)
        {
            var mapper = MapperContainer.Instance.GetSqlMapper(s);
            return mapper.QueryPage(s, ctx, request);
        }

        public static GridResponse<T> QueryPage<T>(this ISession s, RequestContext ctx, GridRequest request = null)
        {
            var mapper = MapperContainer.Instance.GetSqlMapper(s);
            return mapper.QueryPage<T>(s, ctx, request);
        }

        public static GridResponse QueryPageTable(this ISession s, RequestContext ctx, GridRequest request = null)
        {
            var mapper = MapperContainer.Instance.GetSqlMapper(s);
            return mapper.QueryPageTable(s, ctx, request);
        }
        #endregion
    }
}
