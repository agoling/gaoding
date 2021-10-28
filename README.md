# gaoding
稿定公司，稿定设计开放平台接口SDK:https://open.gaoding.com/docs/zhang-hu

调用方式：

```c#
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
                Uid = "b2b-26131xxxxxxxx"
            };
            var r=GaoDingContext.Auth.Code(request);
```

