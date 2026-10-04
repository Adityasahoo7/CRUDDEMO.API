namespace CRUDDEMO.API.Models.DTOs
{
    public class GetAllEmpDto
    {
   
        public string Name { get; set; }
        public int Salary { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string Department { get; set; }
        public DateOnly JoiningDate { get; set; }
    }
}
