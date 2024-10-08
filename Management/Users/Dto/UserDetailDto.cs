﻿using Management.Roles.Dto;

namespace Management.Users.Dto
{
    public class UserDetailDto
    {
        public string FirstName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<RoleDto> Roles { get; set; }
    }
}
