using System;
using System.Threading.Tasks;

namespace OktaTest
{
    public class Program
    {
        static async Task Main()
        {
            var accessToken = await Leprechaun.GetOktaAccessToken();
            Console.WriteLine(accessToken);

            Console.ReadKey();
        }
    }
}
