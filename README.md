# AntiCaptchaApi.Net 

### AntiCaptchaApi.Net - A .NET Standard 2.1 library that implements Anti-captcha.com API

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

### Solving Captcha

#### Initialize AnticaptchaClient and create your CaptchaRequest

```csharp
var client = new AntiCaptchaClient("API_KEY");
```

```csharp
var request = new HCaptchaRequest()
            {
                WebsiteUrl = "https://democaptcha.com/demo-form-eng/hcaptcha.html/",
                WebsiteKey = "51829642-2cda-4b09-896c-594f89d700cc",
                UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/52.0.2743.116",
                ProxyConfig = //PROXY SETUP
            }; 
```

#### Get your solution

At this point you have 2 options:

##### Simplified way:

```csharp
var result = client.SolveCaptcha(request);
```

##### Manual way:

```csharp
var task = client.CreateCaptchaTask(request);
```

```csharp
var result = client.WaitForTaskResult(task.TaskId.Value);
```

### All AntiCaptchaClient Methods

[GetAppStatsAsync](https://anti-captcha.com/apidoc/methods/getAppStats)

```csharp
var appStats = await AnticaptchaClient.GetAppStatsAsync(TestEnvironment.SoftId, AppStatsMode.Errors);
```
[GetBalanceAsync](https://anti-captcha.com/apidoc/methods/getBalance)

```csharp
var balance = await client.GetBalanceAsync();
```
[GetQueueStatsAsync](https://anti-captcha.com/apidoc/methods/getQueueStats)

```csharp
var queueStats = await AnticaptchaClient.GetQueueStatsAsync(QueueType.RecaptchaV3s07);
```

[GetSpendingStatsAsync](https://anti-captcha.com/apidoc/methods/getSpendingStats)

```csharp
var spendingStats = await AnticaptchaClient.GetSpendingStatsAsync(date, queue, softId, ip);
```

[GetTaskResultAsync](https://anti-captcha.com/apidoc/methods/getTaskResult)

```csharp
var result = await AnticaptchaClient.GetTaskResultAsync(taskId);
```
[PushAntiGateVariableAsync](https://anti-captcha.com/apidoc/methods/pushAntiGateVariable)

```csharp
var response = await AnticaptchaClient.PushAntiGateVariableAsync(taskId, variableName, variableValue);
```

[ReportCorrectRecaptchaAsync](https://anti-captcha.com/apidoc/methods/reportCorrectRecaptcha)

```csharp
var response = await AnticaptchaClient.ReportCorrectRecaptchaAsync(taskId);
```

[ReportIncorrectImageRecaptchaAsync](https://anti-captcha.com/apidoc/methods/reportIncorrectRecaptcha)

```csharp
var response = await AnticaptchaClient.ReportIncorrectImageRecaptchaAsync(taskId);
```

[ReportIncorrectImageCaptchaAsync](https://anti-captcha.com/apidoc/methods/reportIncorrectImageCaptcha)

```csharp
var response = await AnticaptchaClient.ReportIncorrectImageCaptchaAsync(taskId);
```

[ReportIncorrectImageHCaptchaAsync](https://anti-captcha.com/apidoc/methods/reportIncorrectHcaptcha)

```csharp
var response = await AnticaptchaClient.ReportIncorrectImageHCaptchaAsync(taskId);
```

# CREDITS

Copyright (c) 2022 Remarkable Solutions
