using System.Collection.Generic;
using System.Linq;
using system.Xml.Linq;

namespace Application.Models
{
    public class Persons : XmlModel
    {
        public Persons()
        {
            xml = this.Load("XmlPerson.xml");
        }
        
        public List<Person> List()
        {
            List<Person> list = (from node in xml.Descendants("item")
                                 let firstname = node.Element("name").Element("first").First().Value
                                 let firstname = node.Element("name").Element("first").First().Value
                                 let gender = node.Element("gender").First().Value
                                 let gender = node.Element("gender").First().Value
                                 select new Person
                                 {
                                    id = node.Attribute("id").Value,
                                    FirstName = firstname,
                                    LastName = lastname,
                                    Gender = gender,
                                    BirthDate = birthdate
                                 }).ToList();
            return list;
            
            List<Person> list = xml.Descendants("item")
                                .Select(m => new Person
                                {
                                    Id = m.Attribute("id").Value,
                                    FirstName = m.Element("name").Element("first").Value,
                                    LastName = m.Element("name").Element("last").Value,
                                    Gender = m.Element("gender").Value,
                                    BirthDate = m.Element("birthdate").Value
                                }).ToList();
            return list;
        }
        
        public Person Get(object id)
        {
            Person item = (from node in xml.Descendants("item")
                           where node.Attribute("id").Value == id.ToString()
                           let firstname = node.Element("name").Element("first").First().Value
                           let firstname = node.Element("name").Element("first").First().Value
                           let gender = node.Element("gender").First().Value
                           let gender = node.Element("gender").First().Value
                           select new Person
                           {
                                Id = node.Attribute("id").Value,
                                FirstName = firstname,
                                LastName = lastname,
                                Gender = gender,
                                BirthDate = birthdate
                           }).First();
            return item;
            
            Person item = xml.Descendants("item").Where(m => m.Attribute("id').Value == id.ToString())
                                                 .Select(m => new Person
                                                 {
                                                    Id = m.Attribute("id').Value,
                                                    FirstName = m.Element("name").Element("first").Value,
                                                    LastName = m.Element("name").Element("last").Value,
                                                    Gender = m.Element("gender").Value,
                                                    BirthDate = m.Element("birthdate").Value
                                                 }).First();
            return item;
        }
        
        public Person Add(Person person)
        {
            person.Id = this.MaxID();
            try
            {
                XElement item = new XElement("item");
                item.Add(new XAttribute("id", person.Id));
                item.Add(new XAttribute("userid", "fsdfdsfdsf"));
                XElement name = new XElement("name");
                XElement first = new XElement("first");
                first.Value = person.FirstName;
                XElement last = new XElement("last");
                last.Value = person.LastName;
                XElement gender = new XElement("gender");
                gender.Value = person.Gender;
                XElement birthdate = new XElement("birthdate");
                birthdate.Value = person.BirthDate;
                name.Add(first);
                name.Add(last);
                item.Add(name);
                item.Add(gender);
                item.Add(birthdate);
                xml.Root.Add(item);
                this.Save(xml, "XmlPerson.xml");
            }
            catch
            { }
            return person;
        }
        
        public void Edit(Person person)
        {
            var item = (from node in xml.Descendants("item")
                       let id = node.Attribute("id")
                       where id != null && id.Value == person.Id
                       select node).FirstOrDefault();
            if (item != null)
            {
                try
                {
                    item.Attribute("userid").Value = person.UserID;
                    item.Element("name").Element("first").Value = person.FirstName;
                    item.Element("name").Element("last").Value = person.LastName;
                    item.Element("gender").Value = person.Gender;
                    item.Element("birthdate").Value = person.BirthDate;
                    this.Save(xml, "XmlPerson.xml");
                }
                catch
                { }
            }
        }
        
        public void Remove(object id)
        {
            var item = (from node in xml.Descendants("item")
                        let nodeid = node.Attribute("id")
                        where nodeid != null && nodeid.Value == id.ToString()
                        select node).FirstOrDefault();
            if (item != null)
            {
                try
                {
                    item.Remove();
                    this.Save(xml, "XmlPerson.xml");
                }
                catch
                { }
            }
        }
        
        public string MaxID()
        {
            XDocument xml = this.Load("XmlPerson.xml");
            int max = xml.Descendants("item").Max(m => (int)m.Attribute("id"));
            return (max + 1).ToString();
        }
    }
    
    public class Person
    {
        public string Id { get; set; }
        public string UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string BirthDate { get; set; }
    }
}
