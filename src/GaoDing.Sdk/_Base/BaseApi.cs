using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using UtilsSharp;
using UtilsSharp.Shared.Standard;

namespace GaoDing.Sdk._Base
{
    /// <summary>
    /// 稿定基础操作接口
    /// </summary>
    public class BaseApi
    {

        /// <summary>
        /// 基础接口请求方法
        /// </summary>
        /// <typeparam name="T">返回模型</typeparam>
        /// <param name="request">请求参数</param>
        /// <returns></returns>
        public BaseResult<T> Request<T>(BaseRequest request)
        {
            var result = new BaseResult<T>();
            try
            {
                var httpRequestMethod = request.Method.ToString().ToUpper();
                var canonicalQueryString = "";
                if (request.P?.Count > 0 && request.ContentType != "application/json")
                {
                    //请求参数按照字母先后顺序排列
                    request.P?.OrderBy(m => m.Key.GetHashCode());
                    //把所有参数名和参数值进行拼装
                    var qp = (from item in request.P where !string.IsNullOrEmpty(item.Value?.ToString()) select $"{item.Key}={item.Value}").ToList();
                    canonicalQueryString = string.Join("&", qp);
                }
                var timestamp = TimeHelper.DateTimeToTimeStamp(DateTime.Now).ToString();
                var requestPayload = request.P?.Count > 0 && request.ContentType == "application/json" ? request.P : null;

                var requestRawBuilder = new StringBuilder();
                requestRawBuilder.Append($"{httpRequestMethod}@");
                requestRawBuilder.Append($"{request.Url.TrimEnd('/')}/@");
                requestRawBuilder.Append($"{canonicalQueryString}@");
                requestRawBuilder.Append($"{timestamp}@");
                if (requestPayload != null)
                {
                    requestRawBuilder.Append($"{JsonConvert.SerializeObject(requestPayload)}");
                }
                var requestRaw = requestRawBuilder.ToString().TrimEnd('@');
                var signature =requestRaw.ToBase64HmacSha1Encrypt(request.Sk,Encoding.UTF8);
                using (var webHelper = new WebHelper())
                {
                    webHelper.Headers.Add("Content-Type", request.ContentType);
                    webHelper.Headers.Add("X-Timestamp", timestamp);
                    webHelper.Headers.Add("X-AccessKey", request.Ak);
                    webHelper.Headers.Add("X-Signature", signature);
                    if (request.Method == HttpMethod.Get)
                    {
                        var r = webHelper.DoGet($"http://open-api.gaoding.com{request.Url}?{canonicalQueryString}");
                        if (r.Code == 200)
                        {
                            result.Result = JsonConvert.DeserializeObject<T>(r.Result);
                            return result;
                        }
                        result.SetError(r.Msg, r.Code);
                        return result;

                    }
                    else
                    {
                        var r = webHelper.DoPost($"http://open-api.gaoding.com{request.Url}", request.P);
                        if (r.Code == 200)
                        {
                            result.Result = JsonConvert.DeserializeObject<T>(r.Result);
                            return result;
                        }
                        result.SetError(r.Msg, r.Code);
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                result.SetError(ex.Message, 5000);
                return result;
            }
        }
    }
}
