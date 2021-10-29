using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace GaoDing.Sdk.Auth.Model
{
    /// <summary>
    /// 获取权益使用凭证接口入参
    /// </summary>
    public class UseCertRequest
    {
        /// <summary>
        /// 应用ID，使用回调方法asyncgetUseRightToken(info)中的info.appId
        /// </summary>
        public string AppId { set; get; }
        /// <summary>
        /// 开放API能力编码，使用回调方法asyncgetUseRightToken(info)中的info.abilityCode
        /// </summary>
        public string AbilityCode { set; get; }
        /// <summary>
        /// 作品ID，使用回调方法asyncgetUseRightToken(info)中的info.workId
        /// </summary>
        public long WorksId { set; get; }

    }
}
