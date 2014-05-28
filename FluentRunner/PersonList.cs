using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentHelper;

namespace FluentRunner
{
    class PersonList : List<Person>
    {
        public PersonList() : base()
        { 
        }

        public PersonList(List<Person> list) : base(list)
        { 
        }

        public FluentCollectionString<PersonList, Person> Name()
        {
            return new FluentCollectionString<PersonList, Person>(this, s => s.name, NameSetter);
        }

        private void NameSetter(Person si, string value)
        {
            si.name = value;
        }
        public FluentCollection<PersonList, Person, DateTime> BirthDate()
        {
            return new FluentCollection<PersonList, Person, DateTime>(this, s => s.birthDate, BirthDateSetter);
        }

        public PersonList BirthDate(DateTime value)
        {
            return new FluentCollection<PersonList, Person, DateTime>(this, s => s.birthDate, BirthDateSetter).Set(value);
        }

        private void BirthDateSetter(Person si, DateTime value)
        {
            si.birthDate = value;
        }
    }
}
