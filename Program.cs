﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FakeUsersLite;
using System.Threading;
using System.IO;

namespace testThreadTaskLoadSaveFile
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");


            Repo repo = new Repo();
            DateTime t = DateTime.Now;
            repo.GenUsersS(100000);
            Console.WriteLine($"GenUsersS ms={(DateTime.Now - t).TotalMilliseconds}");
            Console.WriteLine(repo.GetInfoOfUsers());
            repo.SaveFile();

            Console.WriteLine("End");
            Console.ReadKey();
        }
    }


    public class Repo
    {
        private DBClass DB { get; set; }
        private long Memory;
        private FakeUser FakeUser = new FakeUser();

        public void SaveFile()
        {
            //StreamWriter writer = new StreamWriter("bazatest.txt");
            //writer.Close();
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            Task ShowWaitingTask = Task.Factory.StartNew(ShowWaiting, tokenSource.Token);
            Task SerializeTask = Task.Factory.StartNew(Serialize);
            SerializeTask.Wait();
            try
            {
                tokenSource.Cancel(true);
                ShowWaitingTask.Wait();
            }
            catch (AggregateException e)
            {
                if (ShowWaitingTask.IsCanceled)
                {
                    SerializeTask.Dispose();
                    ShowWaitingTask.Dispose();
                    tokenSource.Dispose();
                }
            }
            ProgressBarConsole.NewLineAfterShowWait();
        }

        private void Serialize()
        {
            string S;
            DateTime t = DateTime.Now;
            S = JsonConvert.SerializeObject(DB);
            Console.WriteLine($"SerializeObject ms={(DateTime.Now - t).TotalMilliseconds}");
            Console.WriteLine($"S Lenght = {((float)S.Length) / 1000000}M");
        }

        private void ShowWaiting(object o)
        {
            CancellationToken token = (CancellationToken)o;
            while(true)
            {
                token.ThrowIfCancellationRequested();
                ProgressBarConsole.ShowWait();
                Thread.Sleep(100);
            }
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


    public class DBClass
    {
        private int lastID;
        public int LastID { get { return lastID; } }

        private List<User> users;

        public List<User> Users
        {
            get { return users; }
            private set { users = value; }
        }
        public DBClass()
        {
            users = new List<User>();
        }
        public void AddUser(string name, int age)
        {
            lastID++;
            User user = new User(name, age, lastID);
            Users.Add(user);
        }
    }


    public class User
    {
        private int id;
        public int ID
        {
            get { return id; }
            private set { id = value; }
        }


        private int age;
        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public User() {}
        public User(string name, int age, int id)
        {
            Name = name;
            Age = age;
            ID = id;
        }

    }

    static class Extension
    {
        private static Random random = new Random();
        public static int GetAge(this FakeUser o, int min,int max)
        {
            return random.Next(min, max + 1);
        }

    }


}