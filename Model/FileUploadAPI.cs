namespace HOSPITALMANAGEMENT.Model
{
    public class FileUploadAPI
    {
        public int ImgID { get; set; }
        public string? Customers { get; set; }
        public IFormFile? files { get; set; }
        public string ImgName { get; set; }
    }
    public class common
    {
        public FileUploadAPI _fileAPI { get; set; }
        public Customer _Customer { get; set; }
        public List<Customer> LstCustomer { get; set; }
    }
    public class Customer
    {
        public int CustID { get; set; }
        public string? CustName { get; set; }
        public string? CustomerType { get; set; }
    }
}
