using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingCom.model
{
    public class User
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Email")]
        public string Email { get; set; }

        [BsonElement("Phone")]
        public string Phone { get; set; }
    }

    public class Room
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("type")]
        public string Type { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("amenities")]
        public List<string> Amenities { get; set; }

        [BsonElement("price")]
        public decimal Price { get; set; }

        public Room()
        {
            // Parameterless constructor for deserialization
        }

        public Room(string name, string type, string description, List<string> amenities, decimal price)
        {
            Name = name;
            Type = type;
            Description = description;
            Amenities = amenities;
            Price = price;
        }
    }

    public class Booking
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonId]
        public ObjectId UserId { get; set; }

        [BsonId]
        public ObjectId RoomId { get; set; }

        [BsonRepresentation(BsonType.Document)]
        public DateTime CheckInDate { get; set; }

        [BsonRepresentation(BsonType.Document)]
        public DateTime CheckOutDate { get; set; }

        [BsonElement]
        public int NumberOfNights { get; set; }

        [BsonElement]
        public string PaymentMethod { get; set; }

        [BsonElement]
        public string PaymentStatus { get; set; }

        [BsonElement]
        public int NumberOfAdults { get; set; }

        [BsonElement]
        public int NumberOfChildren { get; set; }

        [BsonElement]
        public string SpecialRequests { get; set; }
    }
}
