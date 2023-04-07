namespace Models
{
    public class LeadsAdModel
    {
        public string LeadId { get; set; } 
        public string AdId { get; set; }   
        public string FormId { get; set; }
        public DateTime CreatedTime { get; set; }
        public List<LeadsAdFormData> FieldData { get; set; }
    }
}