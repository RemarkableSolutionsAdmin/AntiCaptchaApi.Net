using System.Threading;
using System.Threading.Tasks;

namespace AntiCaptchaApi.Net.Internal
{
    internal static class Waiter
    {
        internal static async Task Wait(int currentSecond)
        {
            await Task.Delay(currentSecond.Equals(0) ? 3000 : 1000);
        }
    }
}