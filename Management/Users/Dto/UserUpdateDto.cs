﻿using Management.Roles.Model;

namespace Management.Users.Dto
{
    public class UserUpdateDto
    {
        public string FirstName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
       // public int RoleId { get; set; }
        public IEnumerable<int> RoleIds { get; internal set; }
    }
}
