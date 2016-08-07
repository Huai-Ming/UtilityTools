using System;

namespace HttpUtility.Basic
{
    public class Client
    {
        private static void Main(string[] args)
        {
            string reponse = HttpRequestHelper.GetValueFromWebApiByGet("http://www.baidu.com");
            Console.WriteLine((reponse));

            Console.WriteLine("Press any key to close the application");
            Console.ReadKey();
        }
    }
}
