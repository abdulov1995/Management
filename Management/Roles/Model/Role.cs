﻿using Management.Users.Model;

namespace Management.Roles.Model
{
    public class Role:BaseEntity
    {
        public string Name { get; set; } 
        public bool IsDeleted { get; internal set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
