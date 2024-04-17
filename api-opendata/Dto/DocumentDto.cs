namespace api_opendata.Dto
{
    public class DocumentDto
    {
        public int? Id { get; set; }
        public string DocumentName { get; set; }
        public string IssuedDate { get; set; }
        public string DocumentCode { get; set; }
        public string DocumentFormat { get; set; }
        public string DocumentSize { get; set; }
        public string CreatedTime { get; set; }
        public string UpdatedTime { get; set; }
    }
}
