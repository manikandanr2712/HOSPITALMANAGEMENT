﻿namespace HOSPITALMANAGEMENT.Model.DbModels
{
    public class User
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string phoneNumber { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public IList<UserEvent> UserEvents { get; set; }
    }
}
