using System;
using FakeUsersLite;

namespace testThreadTaskLoadSaveFile
{
    static class Extension
    {
        private static Random random = new Random();
        public static int GetAge(this FakeUser o, int min,int max)
        {
            return random.Next(min, max + 1);
        }

    }


}
