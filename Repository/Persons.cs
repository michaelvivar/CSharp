using System;
using System.Linq;
using ConsoleApplication1.Repositories;
using System.Collections.Generic;

namespace Application.UnitOfWork
{
    public class Persons
    {
        public UnitOfWork Db;
        public Persons()
        {
            this.Db = new UnitOfWork();
        }

        public Person Get(int id)
        {
            var person = this.Db.UserRepository.Select(id);
            if (person != null)
            {
                return new Person
                {
                    Id = person.id,
                    FirstName = person.first_name,
                    LastName = person.last_name,
                    Gender = person.gender,
                    BirthDate = person.birth_date
                };
            }
            else
            {
                return new Person();
            }
        }

        public IEnumerable<Person> Get()
        {
            return from p in this.Db.UserRepository.Select()
                   select
                       new Person
                       {
                           Id = p.id,
                           FirstName = p.first_name,
                           LastName = p.last_name,
                           Gender = p.gender,
                           BirthDate = p.birth_date
                       };
        }

        public Person Add(Person person)
        {
            try
            {
                var item = new tbl_person
                {
                    first_name = person.FirstName,
                    last_name = person.LastName,
                    gender = person.Gender,
                    birth_date = person.BirthDate
                };
                this.Db.UserRepository.Insert(item);
                this.Db.Save();
                person.Id = item.id;
            }
            catch { }
            return person;
        }

        public void Edit(Person person)
        {
            try
            {
                this.Db.UserRepository.Update(new tbl_person()
                {
                    id = person.Id,
                    first_name = person.FirstName,
                    last_name = person.LastName,
                    gender = person.Gender,
                    birth_date = person.BirthDate
                });
                this.Db.Save();
            }
            catch { }
        }

        public void Remove(int id)
        {
            try
            {
                var item = this.Db.UserRepository.Select(id);
                if (item != null)
                {
                    this.Db.UserRepository.Delete(item);
                    this.Db.Save();
                }
            }
            catch { }
        }
    }

    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public System.DateTime BirthDate { get; set; }
    }
}
