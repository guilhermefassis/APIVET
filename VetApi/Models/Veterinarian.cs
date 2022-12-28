using System;
using VetApi.DTOS.VeterinarianDTOS;
using VetApi.Models.Enums;

namespace VetApi.Models
{
    public class Veterinarian : Person
    {
        public string CRMV { get; set; }
        public Specialty Specialty { get; set; }
        public static implicit operator Veterinarian(CreateVeterinarianWithAddressDTO veterinarian)
        {
            var address = new Address();
            address.Neighborhood = veterinarian.Address.Neighborhood;
            address.Number = veterinarian.Address.Number;
            address.State = veterinarian.Address.State;
            address.PublicPlace = (PublicPlace)Enum.Parse(typeof(PublicPlace), veterinarian.Address.PublicPlace);
            address.ZipCode = veterinarian.Address.ZipCode;
            address.City = veterinarian.Address.City;
            address.StreetName = veterinarian.Address.StreetName;

            return new Veterinarian() 
            {
                SSN = veterinarian.SSN,
                Name = veterinarian.Name,
                BirthDay = veterinarian.BirthDay,
                CRMV = veterinarian.CRMV,
                Specialty = veterinarian.Specialty,
                Address = address
            };
        }
    }
}