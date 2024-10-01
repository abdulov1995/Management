namespace Management.Users.Dto
{
    public class CreateUserDto
    {
        public string FirstName { get; set; }
        public int Age { get; set; }
        public List<int> RoleIds { get; set; }
    }
}
