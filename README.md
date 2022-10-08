# AntiCaptchaApi.Net 

### AntiCaptchaApi.Net - A .NET Standard 2.1 library that implements Anticaptcha.com API

AntiCaptchaApi.Net is a framework written using the .NET Standard. Framework wraps all captcha methods provided by anticaptcha for use in your application.

## LICENSE

MIT - see LICENSE

## INFO

### Adding AntiCaptchaApi.Net to your project

Simply install the nuget package via

`Install-Package AntiCaptchaApi.Net`

### Contributing

1. Clone
1. Branch
1. Make changes
1. Push
1. Make a pull request

### Source

1. Clone the source down to your machine.
   `git clone https://github.com/RemarkableSolutionsAdmin/AntiCaptchaApi.Net.git`
   
# REQUIREMENTS

To run the build, a Visual Studio 2022 compatible environment should be setup.

## Usage

1. First, you have to initialize your AnticaptchaClient:
```csharp
var client = new AntiCaptchaClient("API_KEY");
```

2. Create your request:
```csharp
var request = new HCaptchaRequest()
            {
                WebsiteUrl = "https://democaptcha.com/demo-form-eng/hcaptcha.html/",
                WebsiteKey = "51829642-2cda-4b09-896c-594f89d700cc",
                UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/52.0.2743.116",
                ProxyConfig = //PROXY SETUP
            }; 
```

3. Create your captcha task:
```csharp
client.CreateCaptchaTask(request);
```

4. Get your captcha solution:
```csharp
client.WaitForRawTaskResult(creationTaskResult.TaskId.Value);
```

You can get your remaining balance like this
```csharp
var balance = await client.GetBalanceAsync();
```

# CREDITS

Copyright (c) 2022 Remarkable Solutions
