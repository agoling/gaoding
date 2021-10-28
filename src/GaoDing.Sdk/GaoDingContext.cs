using System;
using System.Collections.Generic;
using System.Text;
using GaoDing.Sdk._Base;
using GaoDing.Sdk.Auth.Api;

namespace GaoDing.Sdk
{

    /// <summary>
    /// 稿定Sdk请求上下文
    /// </summary>
    public abstract class GaoDingContext : GaoDingContext<GaoDingContext>
    {

    }

    /// <summary>
    /// 稿定请求上下文
    /// </summary>
    public abstract class GaoDingContext<TMark>
    {
        // ReSharper disable once StaticMemberInGenericType
        private static GaoDingClient _instance;

        /// <summary>
        /// GaoDingClient 静态实例，使用前请初始化
        /// GaoDingContext.Initialization(new GaoDingClient())
        /// </summary>
        private static GaoDingClient Instance
        {
            get
            {
                if (_instance == null) throw new Exception("使用前请初始化 GaoDingContext.Initialization(new GaoDingClient());");
                return _instance;
            }
        }

        /// <summary>
        /// 初始化GaoDingClient静态访问类
        /// GaoDingContext.Initialization(new GaoDingClient())
        /// </summary>
        /// <param name="gaoDingClient"></param>
        public static void Initialization(GaoDingClient gaoDingClient)
        {
            _instance = gaoDingClient;
        }

        /// <summary>
        /// 稿定配置信息
        /// </summary>
        public static GaoDingSetting Setting => Instance.Setting;

        /// <summary>
        /// 授权
        /// </summary>
        public static AuthApi Auth => Instance.Auth;
    }
}
