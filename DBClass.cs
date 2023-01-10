using System.Collections.Generic;

namespace testThreadTaskLoadSaveFile
{
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


}
