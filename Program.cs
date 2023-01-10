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

            //List<Thread> threads = new List<Thread>();




            Console.WriteLine("End One Part (Press key)");
            Console.ReadKey();
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
