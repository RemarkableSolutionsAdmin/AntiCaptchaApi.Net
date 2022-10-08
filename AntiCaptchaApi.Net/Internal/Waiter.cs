using System.Threading;
using System.Threading.Tasks;

namespace AntiCaptchaApi.Internal
{
    internal static class Waiter
    {
        internal static async Task Wait(bool isAsync, int currentSecond)
        {
            if (isAsync)
            {
                await Task.Delay(currentSecond.Equals(0) ? 3000 : 1000);
            }
            else
            {
                Thread.Sleep(currentSecond.Equals(0) ? 3000 : 1000);
            }
        }
    }
}