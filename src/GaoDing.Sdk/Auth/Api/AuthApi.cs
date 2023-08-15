using System;
using System.Collections.Generic;
using System.Net.Http;
using GaoDing.Sdk._Base;
using GaoDing.Sdk.Auth.Model;
using UtilsSharp.Shared.Standard;

namespace GaoDing.Sdk.Auth.Api
{
    /// <summary>
    /// 授权Api
    /// </summary>
    public class AuthApi: BaseApi
    {
        protected readonly GaoDingClient Client;

        public AuthApi(GaoDingClient client)
        {
            Client = client;
        }

        /// <summary>
        /// 授权码（authorization_code）获取接口
        /// </summary>
        /// <param name="request">参数</param>
        /// <returns></returns>
        public BaseResult<CodeResponse> Code(CodeRequest request)
        {
            var br = new BaseRequest
            {
                Ak = Client.Setting.Ak,
                Sk = Client.Setting.Sk,
                Method = HttpMethod.Get,
                ContentType = "application/x-www-form-urlencoded",
                Url = "/api/authorized/code/",
                P = new Dictionary<string, object> { { "app_id", request.AppId },{ "uid", request.Uid}}
            };
            var r = Request<CodeResponse>(br);
            return r;
        }

        /// <summary>
        /// 获取权益使用凭证接口
        /// </summary>
        /// <param name="request">参数</param>
        /// <returns></returns>
        public BaseResult<UseCertResponse> UseCert(UseCertRequest request)
        {
            var br = new BaseRequest
            {
                Ak = Client.Setting.Ak,
                Sk = Client.Setting.Sk,
                Method = HttpMethod.Post,
                ContentType = "application/json",
                Url = "/api/use-cert/",
                P = new Dictionary<string, object> { { "app_id", request.AppId }, { "ability_code", request.AbilityCode }, { "works_id", request.WorksId } }
            };
            var r = Request<UseCertResponse>(br);
            return r;
        }


    }
}
