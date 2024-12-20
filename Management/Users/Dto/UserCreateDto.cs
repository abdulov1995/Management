﻿namespace Management.Users.Dto
{
    public class UserCreateDto:BaseEntity
    {
        public string FirstName { get; set; }
        public string? LastName { get; set; } 
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
        public int? RoleId { get; set; }
        public List<int> RoleIds { get; set; }

    }
}
