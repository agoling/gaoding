using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using GaoDing.Sdk._Base;
using GaoDing.Sdk.Auth.Model;
using Newtonsoft.Json;
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
            //������Ϣ
            //var setting = new GaoDingSetting()
            //{
            //    AppId = "AppId0000000000000000",
            //    Ak = "AK0000000000000000000000",
            //    Sk = "Sk0000000000000000000000"
            //};
            var settingStr = File.ReadAllText("D:\\Setting\\GaoDing.json");
            var setting = JsonConvert.DeserializeObject<GaoDingSetting>(settingStr);
            //��ʼ��Sdk
            GaoDingContext.Initialization(new GaoDingClient(setting));
            //���û�ȡ��Ȩ�루code���ӿ�
            var request = new CodeRequest
            {
                AppId = setting.AppId,
                Uid = "b2b-2613179621fe405"
            };
            var r=GaoDingContext.Auth.Code(request);
            //��ȡȨ��ʹ��ƾ֤�ӿ�
            var request1 = new UseCertRequest
            {
                AppId = setting.AppId,
                AbilityCode = "IE002",
                WorksId = 17752029443402756
            };
            var r1 = GaoDingContext.Auth.UseCert(request1);
        }
    }
}