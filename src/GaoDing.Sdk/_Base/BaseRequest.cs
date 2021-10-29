using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace GaoDing.Sdk._Base
{
    /// <summary>
    /// 基础请求参数
    /// </summary>
    public class BaseRequest: GaoDingSetting
    {
        /// <summary>
        /// 表示请求的路径，以 ”/“ 开头，并以 ”/“ 结尾，当结尾不是”/“ 需要补上”/“，如：/api/user 需要变为 /api/user/
        /// </summary>
        public string Url { set; get; }
        /// <summary>
        /// 请求参数
        /// </summary>
        public Dictionary<string,object> P { set; get; }
        /// <summary>
        /// 表示请求的http方法，大写， 如POST、GET、PUT
        /// </summary>
        public HttpMethod Method { set; get; }
        /// <summary>
        /// ContentType
        /// </summary>
        public string ContentType { set; get; } = "application/json";

    }
}
