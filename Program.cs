using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace testThreadTaskLoadSaveFile
{
    class Program
    {
        static void Main(string[] args)
        {
            OnePart();
            TwoPart();
        }

        public static void OnePart()
        {
            Console.WriteLine("Start One Part");
            List<Thread> threads = new List<Thread>();
            int sum = 0;
            object forLock= new object();

            for (int i = 0; i < 12; i++)
            {
                Thread thread = new Thread(() => {
                    string A;
                    for (int ii = 0; ii < 10; ii++)
                    {
                        lock(forLock)
                        {
                            sum += ii;
                            if (sum == 42) A = "       <--- amazing!!!"; else A = "";
                            Console.WriteLine($"My id is {Thread.CurrentThread.ManagedThreadId}, ii={ii}, sum now {sum} {A}");
                        }
                        Thread.Sleep(100+ii*10);
                    }
                });
                threads.Add(thread);
            }
            foreach (var item in threads)
            {
                item.Start();
            }
            foreach (var item in threads)
            {
                item.Join();
            }



            Console.WriteLine("End One Part (Press key)");
            Console.ReadKey();
            Console.Clear();
        }

        public static void TwoPart()
        {
            Console.WriteLine("Start Two Part");

            Repo repo = new Repo();
            DateTime t = DateTime.Now;
            repo.GenUsersS(1_000_000);
            Console.WriteLine($"GenUsersS ms={(DateTime.Now - t).TotalMilliseconds}");
            Console.WriteLine(repo.GetInfoOfUsers());
            repo.SaveFile();

            Console.WriteLine("End Two Part (Press key)");
            Console.ReadKey();
        }
    }
}
