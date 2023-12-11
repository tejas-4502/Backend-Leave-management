namespace Backend_Leave.Models
{
    public class LeaveApply
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Leave { get; set; }
        public string Startdate { get; set; }
        public string Enddate { get; set; }
        public string? Comments { get; set; }
    }
}
