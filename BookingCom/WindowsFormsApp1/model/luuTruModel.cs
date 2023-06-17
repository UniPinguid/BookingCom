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

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("phone")]
        public string Phone { get; set; }
    }

    public class Stay
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("location")]
        public string Location { get; set; }

        [BsonElement("address")]
        public string Address { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("facilities")]
        public List<String> Facilities { get; set; }

        [BsonElement("score")]
        public decimal Score { get; set; }

        [BsonElement("cheap_price")]
        public decimal CheapPrice { get; set; }
    }

    public class Room
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public ObjectId Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("stayId")]
        public ObjectId StayId { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("type")]
        public string Type { get; set; }

        [BsonElement("sleeps")]
        public int Sleeps { get; set; }

        [BsonElement("amenities")]
        public List<string> Amenities { get; set; }

        [BsonElement("price")]
        public decimal Price { get; set; }

        [BsonElement("availability")]
        public int Availability { get; set; }

        public Room()
        {
            // Parameterless constructor for deserialization
        }
    }

    public class Booking
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public ObjectId Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("userId")]
        public ObjectId UserId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("stayId")]
        public ObjectId StayId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("roomId")]
        public List<ObjectId> RoomId { get; set; }

        [BsonElement("roomNum")]
        public List<decimal> RoomNum { get; set; }

        [BsonElement("checkInDate")]
        [BsonRepresentation(BsonType.Document)]
        public DateTime CheckInDate { get; set; }

        [BsonElement("checkOutDate")]
        [BsonRepresentation(BsonType.Document)]
        public DateTime CheckOutDate { get; set; }

        [BsonElement("noNights")]
        public int NumberOfNights { get; set; }

        [BsonElement("paymentMethod")]
        public string PaymentMethod { get; set; }

        [BsonElement("paymentStatus")]
        public string PaymentStatus { get; set; }

        [BsonElement("noAdults")]
        public int NumberOfAdults { get; set; }

        [BsonElement("noChildren")]
        public int NumberOfChildren { get; set; }

        [BsonElement("request")]
        public string SpecialRequests { get; set; }
    }
}
