using System;
using System.Collections.Generic;
using System.Text;

namespace GaoDing.Sdk.Auth.Model
{
    /// <summary>
    /// 获取授权码（code）接口 返回参
    /// </summary>
    public class CodeResponse
    {
        /// <summary>
        /// 授权码code
        /// </summary>
        public string Code { set; get; }

    }
}
