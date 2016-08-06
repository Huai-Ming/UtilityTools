using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;

namespace Log4Net.Basic
{
    class Client
    {
        private static readonly ILog logger =
              LogManager.GetLogger(typeof(Client));

        static void Main(string[] args)
        {
            #region Without config file
            //BasicConfigurator.Configure();

            //logger.Debug("Here is a debug log.");
            ////logger.Info("... and an Info log.");
            //logger.Warn("... and a warning.");
            //logger.Error("... and an error.");
            //logger.Fatal("... and a fatal error.");
            #endregion

            #region With config file
            InitLog4Net();

            var logger = LogManager.GetLogger(typeof(Program));

            logger.Info("消息");
            logger.Warn("警告");
            logger.Error("异常");
            logger.Fatal("错误1");
            logger.Fatal("错误2");
            #endregion

            Console.WriteLine("");
            Console.WriteLine("Press any key to close the application");
            Console.ReadKey();
        }

        private static void InitLog4Net()
        {
            var logCfg = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config");
            XmlConfigurator.ConfigureAndWatch(logCfg);
        }
    }
}
