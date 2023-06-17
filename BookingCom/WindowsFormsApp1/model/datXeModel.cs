using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingCom.model
{
    public class Location
    {
        [BsonElement]
        public string Name { get; set; }
        [BsonElement]
        public double Latitude { get; set; }
        [BsonElement]
        public long Longitude { get; set; }
    }

    public class Reviews
    {
        [BsonElement]
        public double CarCleanliness { get; set; }
        [BsonElement]
        public double PickUpSpeed { get; set; }
        [BsonElement]
        public double DropOffSpeed { get; set; }
        [BsonElement]
        public double CarCondition { get; set; }
        [BsonElement]
        public double Helpfulness { get; set; }
        [BsonElement]
        public double EasyToFind { get; set; }
        [BsonElement]
        public double Value { get; set; }
    }

    public class CarRentalProvider
    {
        [BsonElement]
        public string Name { get; set; }
        [BsonElement]
        public string IncludedInRentalPrice { get; set; }
        [BsonElement]
        public string NotIncludedInRentalPrice { get; set; }
        [BsonElement]
        public string WhatYouNeedAtPickUP { get; set; }
        [BsonElement]
        public string DepositsExcessCover { get; set; }
        [BsonElement]
        public string FuelPolicy { get; set; }
        [BsonElement]
        public string ExtraServices { get; set; }
        [BsonElement]
        public string ExtraEquipment { get; set; }
        [BsonElement]
        public string ImportantInformation { get; set; }
        [BsonElement]
        public Reviews Reviews { get; set; }
    }

    public class BillingAddress
    {
        [BsonElement]
        public string FirstName { get; set;}
        [BsonElement]
        public string LastName { get; set;}
        [BsonElement]
        public string ContactNumber { get; set; }
        [BsonElement]
        public string Country { get; set; }
        [BsonElement]
        public string Address { get; set; }
        [BsonElement]
        public string City { get; set; }
        [BsonElement]
        public int Postcode { get; set; }
        [BsonElement]
        public bool BusinessBooking { get; set; }
    }

    public class MainDriver
    {
        [BsonElement]
        public string Email { get; set; }
        [BsonElement]
        public string Title { get; set; }
        [BsonElement]
        public string FirstName { get; set;}
        [BsonElement]
        public string LastName { get; set;}
        [BsonElement]
        public string ContactPhone { get; set; }
        [BsonElement]
        public string CountryOfResidence { get; set; }
        [BsonElement]
        public int Age { get; set; }
    }
    public class Payment
    {
        [BsonElement]
        public string CardHolderName { get; set; }
        [BsonElement]
        public string CarNumber { get; set; }
        [BsonElement]
        public DateTime ExpiryDate { get; set; }
        [BsonElement]
        public string CVC { get; set; }
    }
    public class Car
    {
        [BsonId]
        public Object Id { get; set; }
        [BsonElement]
        public CarRentalProvider RentalProvider { get; set; }
        [BsonElement]
        public string CarBrand { get; set; }
        [BsonElement]
        public string CarType { get; set; }
        [BsonElement]
        public string FuelPolicy { get; set; }
        [BsonElement]
        public int NumberOfSeats { get; set; }
        [BsonElement]
        public int NumberOfMiles { get; set; }
        [BsonElement]
        public string Transmission { get; set; }
        [BsonElement]
        public string Information { get; set; }
        [BsonElement]
        public List<string> Tags { get; set; }
        [BsonElement]
        public string ElectricCar { get; set; }
        [BsonElement]
        public int Available { get; set; }
        [BsonElement]
        public Location CarLocation { get; set; }
    }
    public class Order
    {
        [BsonId]
        public Object Id { get; set; }

        [BsonElement]
        public Location Pickup_Location { get; set; }
        [BsonElement]
        public Location Dropoff_Location { get; set; }
        [BsonElement]
        public DateTime Pickup_Time { get; set; }
        [BsonElement]
        public DateTime Dropoff_Time { get; set; }
        [BsonElement]
        public BillingAddress BillingAddress { get; set; }
        [BsonElement]
        public Payment Payment { get; set; }
        [BsonElement]
        public MainDriver MainDriver { get; set; }

    }   
}
