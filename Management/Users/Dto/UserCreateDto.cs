﻿namespace Management.Users.Dto
{
    public class UserCreateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
        public List<int> RoleIds { get; set; }
    }
}
