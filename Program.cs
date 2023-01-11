using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace testThreadTaskLoadSaveFile
{
    class Program
    {
        static void Main(string[] args)
        {
            PartAsync();
            Console.ReadKey();
            //MatrixPart();
            //Console.ReadKey();
            OnePart();
            Console.ReadKey();
            TwoPart();
            Console.ReadKey();
        }

        public static void OnePart()
        {
            Console.WriteLine("Start One Part");
            List<Thread> threads = new List<Thread>();
            FakeUsersLite.FakeUser fu = new FakeUsersLite.FakeUser(FakeUsersLite.FakeUser.Egender.Female);
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
                            Console.WriteLine(
                                $"My id is " +
                                $"{Thread.CurrentThread.ManagedThreadId,-4}, {Thread.CurrentThread.Name,-31}, " +
                                $"ii={ii,2}, sum = {sum} {A}");
                        }
                        Thread.Sleep(100+ii*10);
                    }
                });
                threads.Add(thread);
            }
            foreach (var item in threads)
            {
                item.Name = fu.GetFullName();
                item.Start();
            }
            foreach (var item in threads)
            {
                item.Join();
            }



            Console.WriteLine("End One Part (Press key)");
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
        }

        public class borshch { public string Color { get; set; } }

        public static async void PartAsync()
        {
            Task<borshch> borshchTask1 = Task.Run(() => PourBorshch("красный"));
            Task<borshch> borshchTask2 = Task.Run(() => PourBorshch("белый"));
            Console.WriteLine("Запущены задачи приготовления");
            var a = await borshchTask1;
            var b = await borshchTask2;
            Console.WriteLine($"Приготовлены {a.Color} и {b.Color}");
        }

        public static async Task<borshch> PourBorshch(string color)
        {
            Thread.CurrentThread.Name = color;
            Console.WriteLine($"Начало готовки {color} борщ, Thread Name = {Thread.CurrentThread.Name}, Thread ID = {Thread.CurrentThread.ManagedThreadId}");
            for (int i = 0; i <= 10; i++)
            {
                Thread.Sleep(100);
                Console.WriteLine($"Готовим {color} борщ - {i*10}%");
            }
            Console.WriteLine($"Окончание готовки {color} борщ");
            borshch b = new borshch() { Color = color };
            return b;
        }

        public static void MatrixPart()
        {
            //Console.WriteLine("Start Matrix part");
            //Matrix A = new Matrix(4, 2, nameof(A));
            //Matrix B = new Matrix(2, 3, nameof(B));
            //A.GenerateRandomMatrixInt();
            //B.GenerateRandomMatrixInt();
            //A.ShowMatrix();
            //Console.WriteLine(A.SummAllElements());
            //Console.WriteLine();
            //B.ShowMatrix();
            //Console.WriteLine(B.SummAllElements());
            //Console.WriteLine();

            //Console.WriteLine("End Matrix part");
        }
    }

}
