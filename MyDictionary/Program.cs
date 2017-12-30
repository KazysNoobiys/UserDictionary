using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MyDictionary
{
    class MyKey : IMyKey<int, string>
    {
        public int Id { get; set; }

        public string Name { get; set; }
     
    }

    class Program
    {
       
        static void Main(string[] args)
        {
            var myDictionay = new DictionaryWhithCustomKey<int, string, int>
            {
                {new MyKey() {Id = 1, Name = "Вася"}, 23},
                {new MyKey() {Id = 1, Name = "Петя"}, 23},
                {new MyKey() {Id = 2, Name = "Коля"}, 30},
                {new MyKey() {Id = 3, Name = "Саша"}, 33},
                {new MyKey() {Id = 4, Name = "Женя"}, 35}
            };
            foreach (var item in myDictionay)
            {
                Console.WriteLine($"Id:{item.Key.Id} Имя:{item.Key.Name} Возраст:{item.Value}");
            }
          
            Console.ReadKey();
        }
    }
}
