using System;
using System.Threading;
using Unity;

namespace LazyResettable
{
    internal sealed class Program
    {
        private static int Repetitions = 10000;
        static void Main(string[] args)
        {
            ThreadPoolLoadTest();

            Console.WriteLine("Done");
            Console.ReadLine();
        }

        static void ThreadPoolLoadTest()
        {
            using (IUnityContainer container = new UnityContainer())
            {
                Bootstrapper.Init(container);

                var fileProvider = container.Resolve<IFileProvider>();

                for (int i = 0; i < Repetitions; i++)
                {
                    ThreadPool.QueueUserWorkItem((state) =>
                    {
                        var currentIteration = (int)state;

                        var data1 = fileProvider.GetData1();
                        var data2 = fileProvider.GetData2();

                        Console.WriteLine("Current: " + currentIteration);

                        Console.WriteLine("Data1 retrieved");
                        Console.WriteLine("Data2 retrieved");

                        if (currentIteration % 8 == 0)
                        {
                            Console.WriteLine("Data Reset");

                            fileProvider.Reset();
                        }

                    }, i);
                }

            }
        }

        static void StandardLoadTest()
        {
            using (IUnityContainer container = new UnityContainer())
            {
                Bootstrapper.Init(container);

                var fileProvider = container.Resolve<IFileProvider>();

                for (int i = 0; i < Repetitions; i++)
                {
                    var data1 = fileProvider.GetData1();
                    var data2 = fileProvider.GetData2();

                    Console.WriteLine("Current: " + i);

                    Console.WriteLine("Data1 retrieved");
                    Console.WriteLine("Data2 retrieved");

                    if (i % 8 == 0)
                    {
                        Console.WriteLine("Data Reset");

                        fileProvider.Reset();
                    }
                }


            }
        }
    }
}
