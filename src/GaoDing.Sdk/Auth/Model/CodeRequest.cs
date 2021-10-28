using System;
using System.Collections.Generic;
using System.Text;

namespace GaoDing.Sdk.Auth.Model
{
    /// <summary>
    /// 获取授权码（code）接口 请求参数
    /// </summary>
    public class CodeRequest
    {
        /// <summary>
        /// 对接方用户体系的用户唯一标识，必填,最长50位字符串
        /// </summary>
        public string Uid { set; get; }
    }
}
