namespace AppLookUp.Models.Interfaces
{
    public interface IAuditable
    {
        public DateTime CreatedTime { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
