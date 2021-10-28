using System;
using System.Collections.Generic;
using System.Text;
using GaoDing.Sdk._Base;
using GaoDing.Sdk.Auth.Api;

namespace GaoDing.Sdk
{
    /// <summary>
    /// 稿定接口客户端
    /// </summary>
    public class GaoDingClient
    {
        /// <summary>
        /// 稿定配置信息
        /// </summary>
        public GaoDingSetting Setting { get; }

        /// <summary>
        /// 授权
        /// </summary>
        public AuthApi Auth { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="setting">配置信息</param>
        public GaoDingClient(GaoDingSetting setting)
        {
            Setting = setting ?? throw new Exception("GaoDingSetting cannot be null");
            Auth = new AuthApi(this);
        }
    }
}
