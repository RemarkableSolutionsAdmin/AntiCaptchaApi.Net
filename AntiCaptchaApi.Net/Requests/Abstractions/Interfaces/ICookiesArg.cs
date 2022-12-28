namespace AntiCaptchaApi.Net.Requests.Abstractions.Interfaces;

public interface ICookiesArg : IRequestArg
{
    public string Cookies { get; set; }   
}