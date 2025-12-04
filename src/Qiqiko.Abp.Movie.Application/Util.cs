using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Qiqiko.Abp.Movie
{
    public class Util
    {
        /// <summary>
        /// 编辑时获取路由主键
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string? GetRouteParameter(IHttpContextAccessor _httpContextAccessor, string key)
        {
            var routeData = _httpContextAccessor.HttpContext?.GetRouteData();
            if (routeData?.Values != null && routeData.Values.TryGetValue(key, out var value))
            {
                return value?.ToString();
            }
            return null;
        }
    }
}
