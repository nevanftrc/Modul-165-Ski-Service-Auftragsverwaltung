using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace SkiServiceAPI.Models
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonRequired]
    public string UserName { get; set; }

    [BsonRequired]
    private string Passwort { get; set; } // Hacerlo privado para no exponerlo directamente.

    public void SetPassword(string password)
    {
        Passwort = BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string password)
    {
        return BCrypt.Net.BCrypt.Verify(password, Passwort);
    }
}