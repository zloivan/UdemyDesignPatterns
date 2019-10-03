using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Protorype
{
    public static class ExtensionMethod
    {
        /// <summary>
        /// Classes require [Serialize] attribute
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static T DeepCopy<T>(this T self)
        {
            //initialize instruments
            var stream = new MemoryStream();
            var formatter = new BinaryFormatter();

            //serialize
            formatter.Serialize(stream, self);

            //deserialize copy
            stream.Seek(0, SeekOrigin.Begin);
            var copy = formatter.Deserialize(stream);

            //close copy
            stream.Close();

            //cast to T and return copy
            return (T)copy;
        }
        /// <summary>
        /// Classes require  to have parametrless constructor and they must be public
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static T DeepCopyXml<T>(this T self)
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new XmlSerializer(typeof(T));
                formatter.Serialize(stream, self);

                stream.Seek(0, SeekOrigin.Begin);
                var copy = formatter.Deserialize(stream);
                return (T) copy;
            }
        }
    }


    public class Person
    {
        public Person()
        {
            
        }
        public string[] Names { get; set; }
        public Address Address { get; set; }

        public Person(string[] names, Address address)
        {
            Names = names;
            Address = address;
        }

        public Person(Person other)
        {
            Names = other.Names;
            Address = new Address(other.Address);
        }

        public override string ToString()
        {
            return $"{nameof(Names)}: {string.Join(" ", Names) }, {nameof(Address)}: {Address}";
        }
    }
    public class Address
    {
        public Address()
        {
            
        }
        public string StreetName { get; set; }
        public int HouseNumber { get; set; }

        public Address(string streetName, int houseNumber)
        {
            StreetName = streetName;
            HouseNumber = houseNumber;
        }

        public Address(Address other)
        {
            StreetName = other.StreetName;
            HouseNumber = other.HouseNumber;
        }

        public override string ToString()
        {
            return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var john = new Person(new string[] { "John", "Smith" },
                new Address("St. Andrew", 12));

            var jane = john.DeepCopyXml();

            jane.Names = new[] { "Jane", "Smith" };
            jane.Address.HouseNumber = 150;

            Console.WriteLine(john);
            Console.WriteLine(jane);
        }
    }
}
