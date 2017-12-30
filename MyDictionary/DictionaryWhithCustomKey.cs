using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace MyDictionary
{
    public class DictionaryWhithCustomKey<TId, TName, TValue> : IDictionary<IMyKey<TId, TName>, TValue>, ICollection
                                                        where TId : IEquatable<TId>
                                                        where TName : IEquatable<TName>
                                                       
    {
        private List<MyObject<TId, TName, TValue>> values;

        public DictionaryWhithCustomKey()
        {
            this.values = new List<MyObject<TId, TName, TValue>>();
        }
        
        public TValue this[IMyKey<TId, TName> key]
        {
            get
            {
                return GetValue(key);
            }

            set
            {
                SetValue(key, value);
            }
        }
        public ICollection<KeyValuePair<IMyKey<TId, TName>, TValue>> this[TId id] => GetValue(id);

        public ICollection<KeyValuePair<IMyKey<TId, TName>, TValue>> this[TName name] => GetValue(name);
       

        private TValue GetValue(IMyKey<TId, TName> key)
        {
            foreach (var myObject in values)
            {
                if (myObject.MyKey.Id.Equals(key.Id) && myObject.MyKey.Name.Equals(key.Name))
                {
                    return myObject.Value;
                }
            }
            return default(TValue);
        }
        private ICollection<KeyValuePair<IMyKey<TId,TName>,TValue>> GetValue(TId id)
        {
            var collection = new List<KeyValuePair<IMyKey<TId, TName>, TValue>>();
            foreach (var item in values)
            {
                if (item.MyKey.Id.Equals(id))
                {
                    collection.Add(new KeyValuePair<IMyKey<TId, TName>, TValue>(item.MyKey,item.Value));
                }
            }
            return collection;
        }
       
        private ICollection<KeyValuePair<IMyKey<TId, TName>, TValue>> GetValue(TName name)
        {
            var collection = new List<KeyValuePair<IMyKey<TId, TName>, TValue>>();
            foreach (var item in values)
            {
                if (item.MyKey.Name.Equals(name))
                {
                    collection.Add(new KeyValuePair<IMyKey<TId, TName>, TValue>(item.MyKey, item.Value));
                }
            }
            return collection;
        }

        private Boolean SetValue(IMyKey<TId, TName> key, TValue value)
        {
            foreach (var item in values)
            {
                if (item.MyKey.Id.Equals(key.Id) && item.MyKey.Name.Equals(key.Name))
                {
                    item.Value = value;
                    return true;
                }
            }
            return false;
        }


        public int Count => values.Count;

        public bool IsReadOnly => false;

        public ICollection<IMyKey<TId, TName>> Keys
        {
            get
            {
                var myKeys = new List<IMyKey<TId, TName>>();
                foreach (var myObject in values)
                {
                    myKeys.Add(myObject.MyKey);
                }
                return myKeys;
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                var listValues = new List<TValue>();
                foreach (var myObject in values)
                {
                    listValues.Add(myObject.Value);
                }
                return listValues;
            }
        }

        public object SyncRoot => ((ICollection)values).SyncRoot;

        public bool IsSynchronized => ((ICollection)values).IsSynchronized;

        public void Add(KeyValuePair<IMyKey<TId, TName>, TValue> item)
        {
            values.Add(new MyObject<TId, TName, TValue>(item.Key.Id, item.Key.Name, item.Value));
        }

        public void Add(IMyKey<TId, TName> key, TValue value)
        {
            values.Add(new MyObject<TId, TName, TValue>(key.Id, key.Name, value));
        }
        public void Add(TId id, TName name, TValue value)
        {
            values.Add(new MyObject<TId, TName, TValue>(id, name, value));
        }

        public void Clear()
        {
            values.Clear();
        }

        public bool Contains(KeyValuePair<IMyKey<TId, TName>, TValue> item)
        {
            foreach (var myObject in values)
            {
                if (myObject.MyKey.Id.Equals(item.Key.Id)
                    && myObject.MyKey.Name.Equals(item.Key.Name)
                    && myObject.Value.Equals(item.Value))
                {
                    return true;
                }
            }
            return false;
        }

        public bool ContainsKey(IMyKey<TId, TName> key)
        {
            foreach (var myObject in values)
            {
                if (myObject.MyKey.Equals(key))
                {
                    return true;
                }
            }
            return false;
        }

        public void CopyTo(KeyValuePair<IMyKey<TId, TName>, TValue>[] array, int arrayIndex)
        {
            for (int i = arrayIndex; i < array.Length; i++)
            {
                array[i] = new KeyValuePair<IMyKey<TId, TName>, TValue>(values[i].MyKey, values[i].Value);
            }
        }

        public IEnumerator<KeyValuePair<IMyKey<TId, TName>, TValue>> GetEnumerator()
        {
            return new DictionaryWhithCustomKeyEnumerator<TId, TName, TValue>(this);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new DictionaryWhithCustomKeyEnumerator<TId, TName, TValue>(this);
        }

        public bool Remove(KeyValuePair<IMyKey<TId, TName>, TValue> item)
        {
            foreach (var myObject in values)
            {
                if (myObject.MyKey.Id.Equals(item.Key.Id)
                    && myObject.MyKey.Name.Equals(item.Key.Name)
                    && myObject.Value.Equals(item.Value))
                {
                    values.Remove(myObject);
                    return true;
                }
            }
            return false;
        }

        public bool Remove(IMyKey<TId, TName> key)
        {
            foreach (var myObject in values)
            {
                if (myObject.MyKey.Id.Equals(key.Id)
                    && myObject.MyKey.Name.Equals(key.Name))
                {
                    values.Remove(myObject);
                    return true;
                }
            }
            return false;
        }

        public bool TryGetValue(IMyKey<TId, TName> key, out TValue value)
        {
            foreach (var myObject in values)
            {
                if (myObject.MyKey.Id.Equals(key.Id)
                    && myObject.MyKey.Name.Equals(key.Name))
                {
                    value = myObject.Value;
                    return true;
                }
            }
            value = default(TValue);
            return false;
        }

        public void CopyTo(Array array, int index)
        {
            ((ICollection)values).CopyTo(array, index);
        }



        #region MyObject

        private class MyObject<TId1, TName1, TValue1>
        {
            public class Key : IMyKey<TId1, TName1>
            {
                public TId1 Id { get; set; }
                public TName1 Name { get; set; }
            }

            public readonly Key MyKey;
            public TValue1 Value { get; set; }
            public MyObject(TId1 id, TName1 name, TValue1 value)
            {
                MyKey = new Key
                {
                    Id = id,
                    Name = name
                };
                Value = value;
            }
        }

        #endregion
        private class DictionaryWhithCustomKeyEnumerator<TId1, TName1, TValue1> :IEnumerator<KeyValuePair<IMyKey<TId1, TName1>, TValue1>> 
                                                                                    where TId1 : IEquatable<TId1>
                                                                                    where TName1 : IEquatable<TName1>
                                                                                    
        {
            private List<DictionaryWhithCustomKey<TId1, TName1, TValue1>.MyObject<TId1, TName1, TValue1>> values;
            private Int32 _curretIndex = -1;

            public DictionaryWhithCustomKeyEnumerator(DictionaryWhithCustomKey<TId1, TName1, TValue1> dictionary)

            {
                this.values = dictionary.values;
            }
            public object Current
            {
                get
                {
                    ValidateIndex();
                    return values[_curretIndex];
                }
            }

            KeyValuePair<IMyKey<TId1, TName1>, TValue1> IEnumerator<KeyValuePair<IMyKey<TId1, TName1>, TValue1>>.Current
            {
                get
                {
                    IMyKey<TId1, TName1> key = (IMyKey<TId1, TName1>)values[_curretIndex].MyKey;
                    TValue1 value = values[_curretIndex].Value;
                    return new KeyValuePair<IMyKey<TId1, TName1>, TValue1>(key,value);
                }
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                if (_curretIndex < values.Count - 1) { _curretIndex++; return true; }
                return false;
                
            }

            public void Reset()
            {
                _curretIndex = -1;
            }

            private void ValidateIndex()
            {
                if (_curretIndex < 0 || _curretIndex >= values.Count)
                    throw new InvalidOperationException("Enumerator is before or after the collection");
            }

        }
    }


}