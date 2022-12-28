using System;
using VetApi.DTOS.TutorsDTOS;
using VetApi.Models.Enums;

namespace VetApi.Models
{
    public class Tutor : Person
    {
        public static implicit operator Tutor(CreateTutorWithAddressDTO tutor)
        {
            var address = new Address();
            address.Neighborhood = tutor.Address.Neighborhood;
            address.Number = tutor.Address.Number;
            address.State = tutor.Address.State;
            address.PublicPlace = (PublicPlace)Enum.Parse(typeof(PublicPlace), tutor.Address.PublicPlace);
            address.ZipCode = tutor.Address.ZipCode;
            address.City = tutor.Address.City;
            address.StreetName = tutor.Address.StreetName;

            return new Tutor() 
            {
                SSN = tutor.SSN,
                Name = tutor.Name,
                BirthDay = tutor.BirthDay,
                Address = address
            };
        }
    }
}