using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MyDictionary
{
    class MyCollection<T, TId, TName> :IEnumerable
                                      where T : IMyKey<TId, TName>
                                      where TId : IEquatable<TId>
                                      where TName : IEquatable<TName>
    {
        private Dictionary<Key, T> dictionary;
        private object LockOn = new object();
        public MyCollection()
        {
            dictionary = new Dictionary<Key, T>();
        }

        public void Add(object obj)
        {
            lock (LockOn)
            {
                T Tobj = (T)obj;
                if (!Contains(Tobj.Id) && !Contains(Tobj.Name))
                {
                    dictionary.Add(new Key(Tobj.Id, Tobj.Name), Tobj);                    
                }                
            }
        }

        public void Remove(object obj)
        {
            lock (LockOn)
            {
                T Tobj = (T)obj;
                dictionary.Remove(new Key(Tobj.Id, Tobj.Name));
            }
        }
        public T Find(TId id)
        {
            lock (LockOn)
            {
                Dictionary<Key, T>.KeyCollection keyCol = dictionary.Keys;
                foreach (var key in keyCol)
                {
                    if (key.Id.Equals(id))
                        return dictionary[key];

                }
                return default(T);
            }
        }
        public T Find(TName name)
        {
            lock (LockOn)
            {
                Dictionary<Key, T>.KeyCollection keyCol = dictionary.Keys;
                foreach (var key in keyCol)
                {
                    if (key.Name.Equals(name))
                        return dictionary[key];
                }
                return default(T);
            }
        }       

        public bool Contains(TId id)
        {
            lock (LockOn)
            {
                Dictionary<Key, T>.KeyCollection keyCol = dictionary.Keys;
                foreach (var key in keyCol)
                {
                    if (key.Id.Equals(id))
                        return true;

                }
                return false;
            }
        }
        public bool Contains(TName name)
        {
            lock (LockOn)
            {
                Dictionary<Key, T>.KeyCollection keyCol = dictionary.Keys;
                foreach (var key in keyCol)
                {
                    if (key.Name.Equals(name))
                        return true;
                }
                return false;
            }
        }
       
        IEnumerator IEnumerable.GetEnumerator()
        {
            return dictionary.GetEnumerator();
        }

        public class Key
        {
            private TId id;
            private TName name;
            public TId Id { get { return id; } }
            public TName Name { get { return name; } }
            public Key(TId id, TName name)
            {
                this.id = id;
                this.name = name;
            }
        }
    }
    public interface IMyKey<TId, TName>
    {
        TId Id { get; }
        TName Name { get; }
    }

}
