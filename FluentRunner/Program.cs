using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            Traditional();
            Console.WriteLine("-----------------");
            FluentObject();
            string result = Console.ReadLine();
        }

        static void Traditional()
        {
            List<Person> People = new List<Person> 
            {
                new Person(){ name="Bentley", birthDate=new DateTime(1963, 3, 7)},
                new Person(){ name="Christy", birthDate=new DateTime(1973, 9, 19)},
                new Person(){ name="Benjamin", birthDate=new DateTime(2001, 9, 19)}
            };

            IEnumerable<Person> q = People.Where(p => p.name.StartsWith("B")).Where(p => p.birthDate > new DateTime(2000, 1, 1));
            foreach (Person p in q)
            {
                Console.WriteLine(p.name);
            }
        }

        static void FluentObject()
        {
            PersonList People = new PersonList()
            {
                new Person(){ name="Bentley", birthDate=new DateTime(1963, 3, 7)},
                new Person(){ name="Christy", birthDate=new DateTime(1973, 9, 19)},
                new Person(){ name="Benjamin", birthDate=new DateTime(2001, 9, 19)}
            };

            PersonList q = People.Name().StartsWith("B").BirthDate().Greater(new DateTime(2000,1,1));
            foreach (Person p in q)
            {
                Console.WriteLine(p.name);
            }
        }

    }
}
