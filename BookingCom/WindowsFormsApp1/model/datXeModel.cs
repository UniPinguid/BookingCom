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
        public string Name { get; set; }
        public double Latitude { get; set; }
        public long Longitude { get; set; }
    }

    public class Reviews
    {
        public double CarCleanliness { get; set; }
        public double PickUpSpeed { get; set; }
        public double DropOffSpeed { get; set; }
        public double CarCondition { get; set; }
        public double Helpfulness { get; set; }
        public double EasyToFind { get; set; }
        public double Value { get; set; }
    }

    public class CarRentalProvider
    {
        public string Name { get; set; }
        public string IncludedInRentalPrice { get; set; }
        public string NotIncludedInRentalPrice { get; set; }
        public string WhatYouNeedAtPickUP { get; set; }
        public string DepositsExcessCover { get; set; }
        public string FuelPolicy { get; set; }
        public string ExtraServices { get; set; }
        public string ExtraEquipment { get; set; }
        public string ImportantInformation { get; set; }
        public Reviews Reviews { get; set; }
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
        public string Tag { get; set; }
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

    }   
}
