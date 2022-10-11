﻿using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests;
using AntiCaptchaApi.Net.Responses;
using AntiCaptchaApi.Net.Tests.Helpers;
using Xunit;

namespace AntiCaptchaApi.Net.Tests.IntegrationTests.AnticaptchaRequests
{
    public class ImageToTextRequestTests : AnticaptchaTestBase
    {
        private const string ExpectedCaptchaResult = "W68HP";

        private static ImageToTextRequest CreateImageToTextRequest(string filePath = "")
        {
            return new ImageToTextRequest
            {
                FilePath = filePath
            };
        }
        
        [Fact]
        public void ShouldReturnCorrectCaptchaResult_WhenCallingAuthenticRequest()
        {
            var request = CreateImageToTextRequest(filePath: "Resources\\captchaexample.png");
            TestCaptchaRequest(request, out TaskResultResponse<ImageToTextSolution> taskResult);
            AssertHelper.NotNullNotEmpty(taskResult.Solution.Url);
            AssertHelper.NotNullNotEmpty(taskResult.Solution.Text);
            Assert.Equal(ExpectedCaptchaResult, taskResult.Solution.Text);
        }

        [Fact]
        public void ShouldReturnFalseWithCorrectException_WhenCallingWithIncorrectFilePath()
        {
            var request = CreateImageToTextRequest(filePath: "dsa.png");
            var result = AnticaptchaClient.CreateCaptchaTask(request);
            Assert.NotEmpty(result.ErrorDescription);
            Assert.Contains(nameof(ImageToTextRequest.BodyBase64), result.ErrorDescription);
        }
    }
}