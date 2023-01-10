using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FakeUsersLite;
using System.IO;

namespace testThreadTaskLoadSaveFile
{
    public class Repo
    {
        private DBClass DB { get; set; }
        private long Memory;
        private FakeUser FakeUser = new FakeUser();
        private string S;

        public void SaveFile(string file = "bazatest.txt")
        {
            Console.WriteLine($"Start SerializeObject");
            DateTime t1 = DateTime.Now;
            Task SerializeTask = Task.Factory.StartNew(Serialize);
            ProgressBarConsole.ShowWaitStart();
            SerializeTask.Wait();
            ProgressBarConsole.ShowWaitStop();
            DateTime t2 = DateTime.Now;
            

            Console.WriteLine($"SerializeObject ms={(t2 - t1).TotalMilliseconds}");
            Console.WriteLine($"S Lenght = {((float)S.Length) / 1000000}M");

            SerializeTask.Dispose();
            


            Console.WriteLine($"Start StreamWriter");
            t1 = DateTime.Now;
            StreamWriter writer = new StreamWriter(file);
            Task task = writer.WriteAsync(S);
            ProgressBarConsole.ShowWaitStart();
            task.Wait();
            ProgressBarConsole.ShowWaitStop();
            t2 = DateTime.Now;
            writer.Close();

            Console.WriteLine($"StreamWriter ms={(t2 - t1).TotalMilliseconds}");
            Console.WriteLine($"File saved as {file}!");
        }

        private void Serialize()
        {
            S = JsonConvert.SerializeObject(DB);
        }

        



        public Repo()
        {
            Memory = GC.GetTotalMemory(true);
            DB = new DBClass();
        }

        public void GenUsersS(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                ProgressBarConsole.Show(i, amount);
                DB.AddUser(FakeUser.GetFullName(), FakeUser.GetAge(16,63));
            }
            ProgressBarConsole.NewLineAfterShow();
        }

        //public void GenUsers(int amount)
        //{
        //    Parallel.For(0, amount,AddUser);
        //}

        //private void AddUser(int a)
        //{
        //    string name = FakeUser.GetFullName();
        //    int age = FakeUser.GetAge(16, 63);
        //    //Console.WriteLine($" TID + = {Thread.CurrentThread.ManagedThreadId}");
        //    lock (DB)
        //    {
        //        DB.AddUser(name, age);
        //    }
        //    //Console.WriteLine($" TID - = {Thread.CurrentThread.ManagedThreadId}");
        //}


        public void ShowAllUsers()
        {
            Console.WriteLine($"# Count Of Users={GetCountOfUsers()}");
            foreach (var item in DB.Users)
            {
                Console.WriteLine($"# ID={item.ID}, {item.Name}, Age={item.Age}");
            }
        }

        public int GetCountOfUsers()
        {
            return DB.Users.Count;
        }

        public string GetInfoOfUsers()
        {
            long MT = GC.GetTotalMemory(true);
            long M = MT - Memory;
            float MTf = (float)MT / 1000000;
            float Mf = (float)M / 1000000;

            return $"Total Users = {GetCountOfUsers()}, Memory = {Mf}/({MTf} MB), LastID = {DB.LastID}";
        }


    }


}
