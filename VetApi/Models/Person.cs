using System;
using System.Runtime.Serialization;

namespace VetApi.Models
{
    [DataContract]
    public abstract class Person
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string SSN { get; set; } // => CPF
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public DateTime BirthDay { get; set; }
        [DataMember]
        public Address Address { get; set; }
    }
}