using MongoDB.Bson;

namespace TODO.Models.Collections
{
    public class EntityCollections
    {
    }
    public class User
    {
        public ObjectId Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string PasswordSalt { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public bool IsEmailVerified { get; set; }
        public DateTime EmailVerifiedAt { get; set; }
    }
    public class OtpNotification
    {
        public ObjectId Id { get; set; }
        public ObjectId UserId { get; set; } // Foreign key to User collection
        public string Username { get; set; } = string.Empty;
        public string Otp { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; }
        public DateTime ExpiresOn { get; set; }
    }

}
