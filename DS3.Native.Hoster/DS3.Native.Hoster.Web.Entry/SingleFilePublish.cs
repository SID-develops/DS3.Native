﻿using Furion;
using System.Reflection;

namespace DS3.Native.Hoster.Web.Entry
{
    /// <summary>
    /// 解决单文件发布问题
    /// </summary>
    public class SingleFilePublish : ISingleFilePublish
    {
        /// <summary>
        /// 解决单文件不能扫描的程序集
        /// </summary>
        /// <remarks>和 <see cref="IncludeAssemblyNames"/> 可同时配置</remarks>
        /// <returns></returns>
        public Assembly[] IncludeAssemblies()
        {
            // 需要 Furion 框架扫描哪些程序集就写上去即可
            return Array.Empty<Assembly>();
        }

        /// <summary>
        /// 解决单文件不能扫描的程序集名称
        /// </summary>
        /// <remarks>和 <see cref="IncludeAssemblies"/> 可同时配置</remarks>
        /// <returns></returns>
        public string[] IncludeAssemblyNames()
        {
            // 需要 Furion 框架扫描哪些程序集就写上去即可
            return new[]
            {
                "DS3.Native.Hoster.Application",
                "DS3.Native.Hoster.Core",
                "DS3.Native.Hoster.EntityFramework.Core",
                "DS3.Native.Hoster.Web.Core",
                "DS3.Native.Hoster.Resources"
            };
        }
    }
}