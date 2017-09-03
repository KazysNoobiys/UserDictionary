using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MyDictionary
{
    using Collection = MyCollection<UserType<ID, string, string>, ID, string>;
    using User = UserType<ID, string, string>;
    class UserType<TId, TName, TValue> : IMyKey<TId, TName>
    {
        TId id;
        TName name;
        TValue value;
        public UserType(TId id, TName name, TValue value)
        {
            this.id = id;
            this.name = name;
            this.value = value;
        }
        public TId Id
        {
            get{ return id;}
        }
        public TName Name
        {
            get { return name; }
        }
        public TValue Value
        {
            get { return value; }
        }

    }
    class ID:IEquatable<ID>
    {
        public int ID1 { get; set; }
        public int ID2 { get; set; }
        public ID(int id1, int id2)
        {
            ID1 = id1;
            ID2 = id2;
        }

        public override bool Equals(object obj)
        {
            if (obj is ID)
                return Equals((ID)obj);
            return false;

        }
        public bool Equals(ID other)
        {
            if (ID1 == other.ID1 && ID2 == other.ID2)
                return true;
            else
                return false;
        }
        public override int GetHashCode()
        {
            return ID1 ^ ID2;
        }
    }    
      
    class Program
    {
       
        static void Main(string[] args)
        {
            User user1 = new User(new ID(10,13), "Name1","Value1");
            User user2 = new User(new ID(2,211), "Name2","Value2");
            Collection myCollection = new Collection();
            
            Thread thread1 = new Thread(myCollection.Add);
            Thread thread2 = new Thread(myCollection.Add);
            thread1.Start(user1);
            thread2.Start(user2);
           
            thread1.Join();
            thread2.Join();

            ID findID = new ID(10, 13);
            UserType<ID, string, string> userTemp = myCollection.Find(findID);
            Console.WriteLine(userTemp.Id.ID1+ " " + userTemp.Id.ID2 + " " + userTemp.Name);
            userTemp = myCollection.Find("Name2");
            Console.WriteLine(userTemp.Id.ID1 + " " + userTemp.Id.ID2 + " " + userTemp.Name);
            foreach (KeyValuePair<Collection.Key,User> dic in myCollection)
            {
                Console.WriteLine("Id = ({0},{1}); Name = {2}; Value = {3}",dic.Key.Id.ID1, dic.Key.Id.ID2, dic.Key.Name,dic.Value.Value);
            }
            Console.ReadKey();
        }
    }
}
