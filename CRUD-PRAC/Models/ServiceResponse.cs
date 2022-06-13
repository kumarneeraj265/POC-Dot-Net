namespace CRUD_PRAC.Models
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }

        public string Message { get; set; } = "Data Found";

        public int Success { get; set; } = 200;
    }
}
