namespace testThreadTaskLoadSaveFile
{
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


}
