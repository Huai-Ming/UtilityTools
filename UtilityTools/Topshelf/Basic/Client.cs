using System;
using System.Timers;

namespace Topshelf.Basic
{
    class Client
    {
        private static void Main(string[] args)
        {
            //Here we are setting up the host using the HostFactory.Run the runner.
            //We open up a new lambda where the ‘x’ in this case exposes all of the host level configuration. 
            //Using this approach the command arguments are extracted from environment variables.
            HostFactory.Run(x =>                                 //1
            {
                //Here we are telling Topshelf that there is a service of type ‘TownCrier”. 
                //The lambda that gets opened here is exposing the service configuration options through the ‘s’ parameter.
                x.Service<TownCrier>(s =>                        //2
                {
                    //This tells Topshelf how to build an instance of the service. 
                    //Currently we are just going to ‘new it up’ 
                    //but we could just as easily pull it from an IoC container with some code that would look something like ‘container.GetInstance<TownCrier>()’
                    s.ConstructUsing(name => new TownCrier());     //3
                    //How does Topshelf start the service
                    s.WhenStarted(tc => tc.Start());              //4
                    //How does Topshelf stop the service
                    s.WhenStopped(tc => tc.Stop());               //5
                });
                //Here we are setting up the ‘run as’ and have selected the ‘local system’.
                //We can also set up from the command line Interactively with a win from type prompt 
                //and we can also just pass in some username/password as string arguments
                x.RunAsLocalSystem();                            //6

                //Here we are setting up the description for the winservice to be use in the windows service monitor
                x.SetDescription("Sample Topshelf Host");        //7
                //Here we are setting up the display name for the winservice to be use in the windows service monitor
                x.SetDisplayName("Stuff");                       //8
                //Here we are setting up the service name for the winservice to be use in the windows service monitor
                x.SetServiceName("Stuff");                       //9
                //Now that the lambda has closed, the configuration will be executed and the host will start running.
            });   
        }
    }

    public class TownCrier
    {
        readonly Timer _timer;
        public TownCrier()
        {
            _timer = new Timer(1000) { AutoReset = true };
            _timer.Elapsed += (sender, eventArgs) => Console.WriteLine("It is {0} and all is well", DateTime.Now);
        }
        public void Start() { _timer.Start(); }
        public void Stop() { _timer.Stop(); }
    }
}
