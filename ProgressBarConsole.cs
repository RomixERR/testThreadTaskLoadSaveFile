using System;
using System.Threading.Tasks;
using System.Threading;

namespace testThreadTaskLoadSaveFile
{
    public class ProgressBarConsole
    {
        private static int OldNum;
        private static int waitNum;
        private static char wnch = (char)91;
        private static int wnch2 = 93;
        private static Task taskShowWait;
        private static CancellationTokenSource cancellationTokenSource;

        static ProgressBarConsole()
        {
            init();
        }


        public static void Show(int procent)
        {
            if (OldNum == procent) return;
            //int y = Console.CursorTop;
            int w = Console.BufferWidth - 6;
            int x = (int)((float)procent / 102 * w);
            string S = new String('#', x);
            OldNum = procent;
            //Console.CursorTop = y;
            Console.CursorLeft = 0;
            Console.Write($"{procent,3}[{S}|");
            Console.CursorLeft = w;
            Console.Write("]");
        }

        public static void Show(int i, int amount)
        {
            float p = (float)i / amount * 101;
            Show((int)p);
        }

        public static void NewLineAfterShow()
        {
            init();
            Console.WriteLine();
        }



        public static void ShowWaitStart(string msg = "wait")
        {
            cancellationTokenSource = new CancellationTokenSource();
            taskShowWait = Task.Factory.StartNew(ShowWaiting, cancellationTokenSource.Token);
        }

        public static void ShowWaitStop(string msg = "ok")
        {
            cancellationTokenSource.Cancel();
            taskShowWait.Wait();
            taskShowWait.Dispose();
            cancellationTokenSource.Dispose();
        }


        private static void NewLineAfterShowWait(string msg = "ok")
        {
            init();
            Console.CursorLeft = 0;
            Console.Write($"{msg}                                     ");
            Console.WriteLine();
        }


        private static void ShowWaiting(object o)
        {
            CancellationToken token = (CancellationToken)o;
            while (true)
            {
                if (token.IsCancellationRequested)
                {
                    NewLineAfterShowWait();
                    return;
                }
                    ShowWait();
                Thread.Sleep(100);
            }
        }

        private static void ShowWait(string msg = "wait")
        {
            Console.CursorLeft = 0;
            Console.Write($"{msg} {(char)waitNum}");
            waitNum++;
            if (waitNum > wnch2) waitNum = wnch;
        }

    

        

        private static void init()
        {
            OldNum = -1;
            waitNum = wnch;
        }
    }


}
