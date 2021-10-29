using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using UtilsSharp;
using UtilsSharp.Standard;

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
                var signature = HmacSha1(requestRaw, request.Sk);

                using (var webHelper = new WebHelper())
                {
                    webHelper.Headers.Add("Content-Type", request.ContentType);
                    webHelper.Headers.Add("X-Timestamp", timestamp);
                    webHelper.Headers.Add("X-AccessKey", request.Ak);
                    webHelper.Headers.Add("X-Signature", signature);
                    try
                    {
                        if (request.Method == HttpMethod.Get)
                        {

                            var r = webHelper.DownloadString($"http://open-api.gaoding.com{request.Url}?{canonicalQueryString}");
                            result.Result = JsonConvert.DeserializeObject<T>(r);
                            return result;
                        }
                        else
                        {
                            var data = JsonConvert.SerializeObject(request.P);
                            var r = webHelper.UploadString($"http://open-api.gaoding.com{request.Url}", request.Method.ToString(), data);
                            result.Result = JsonConvert.DeserializeObject<T>(r);
                            return result;
                        }
                    }
                    catch (Exception ex)
                    {
                        if (ex.GetType().Name == "WebException")
                        {
                            var we = (WebException)ex;
                            using (var hr = (HttpWebResponse)we.Response)
                            {
                                var statusCode = (int)hr.StatusCode;
                                var sb = new StringBuilder();
                                var sr = new StreamReader(hr.GetResponseStream(), Encoding.UTF8);
                                sb.Append(sr.ReadToEnd());
                                result.SetError($"{sb}", statusCode);
                                return result;
                            }
                        }
                        result.SetError(ex.Message,5000);
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


        /// <summary>
        /// HmacSha1加密
        /// </summary>
        /// <param name="str">要加密的原串</param>
        ///<param name="key">私钥</param>
        /// <returns></returns>
        public static string HmacSha1(string str, string key)
        {
            var hmacSha1 = new HMACSHA1();
            hmacSha1.Key = Encoding.UTF8.GetBytes(key);

            var dataBuffer = Encoding.UTF8.GetBytes(str);
            var hashBytes = hmacSha1.ComputeHash(dataBuffer);

            return Convert.ToBase64String(hashBytes);

        }


    }
}
