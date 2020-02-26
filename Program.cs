using System;
using System.Threading;
using Prometheus;

namespace metrics_test
{
    class Program
    {
        //private static readonly Counter TickTock = Metrics.CreateCounter("sampleapp_ticks_total", "Just keeps on ticking");
        private static readonly Gauge JobsInQueue = Metrics.CreateGauge("myapp_jobs_queued", "Number of jobs waiting for processing in the queue.");
        static void Main(string[] args)
        {
            var server = new MetricServer(hostname: "localhost", port: 1234);
            server.Start();
            while (true)
            {
                Console.WriteLine("Press enter to increment counter");
                var value = int.Parse(Console.ReadLine());
                Console.WriteLine(value);
                if(value > 0){
                    Console.WriteLine("positive");
                    JobsInQueue.Inc(value);
                } 
                else if(value < 0) {
                    Console.WriteLine("negative");
                    JobsInQueue.Dec(Math.Abs(value));
                }
            }
        }
    }
}
