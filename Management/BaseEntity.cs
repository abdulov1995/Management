namespace Management
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        //public string? DeletedBy { get; set; }
       // public DateTime? DeletedOn { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; } 
    }
}
