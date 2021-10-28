using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using GaoDing.Sdk._Base;
using GaoDing.Sdk.Auth.Model;
using NUnit.Framework;
using UtilsSharp;

namespace GaoDing.Sdk.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            //配置信息
            var setting = new GaoDingSetting()
            {
                AppId = "AppId0000000000000000",
                Ak = "AK0000000000000000000000",
                Sk = "Sk0000000000000000000000"
            };
            //初始化Sdk
            GaoDingContext.Initialization(new GaoDingClient(setting));
            //调用接口
            var request = new CodeRequest
            {
                Uid = "b2b-2613179621fe405"
            };
            var r=GaoDingContext.Auth.Code(request);
        }
    }
}