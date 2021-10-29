using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GaoDing.Sdk.Auth.Model
{
    /// <summary>
    /// 获取权益使用凭证接口 出参
    /// </summary>
    public class UseCertResponse
    {
        /// <summary>
        /// 权益使用凭证，有效期1分钟
        /// </summary>
        [JsonProperty("use_cert")]
        public string UseCert { set; get; }
    }
}
