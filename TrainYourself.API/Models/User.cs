using System;
using MongoDB.Bson;
using TrainYourself.API.Common;

namespace TrainYourself.API.Models
{
    [BsonCollection("Users")]
    public class User : IDocument
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt => Id.CreationTime;
        public string Username { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
