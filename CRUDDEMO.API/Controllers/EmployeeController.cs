using CRUDDEMO.API.Models;
using CRUDDEMO.API.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUDDEMO.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly AppDbContext _context;
        
        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("Getempbynum")]
        public async Task<IActionResult> getempbycount(int count)
        {
            var employee = await _context.Employeesds.FromSqlRaw("  exec getempbycount @count ={0}", count).ToListAsync();
            return Ok(employee);

        }

        [HttpGet]
        public async Task<IActionResult> getAll()
        {
            var employee = await _context.Employeesds.ToListAsync();
            return Ok(employee);
        }


        [HttpGet]
        [Route("GetEmpByID")]
        public async Task<IActionResult>GetEmpByID(int id)
        {
            // var employee = await _context.Employeesds.FromSqlRaw("exec getempbyid @id ={0}", id).ToListAsync();

            // var employee = await _context.Employeesds.FindAsync(id);
            var employee = await _context.Employeesds.FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]
        [Route("AddEmployee")]
        public async Task<IActionResult>Addemployee(GetAllEmpDto dto)
        {
            var emp = new Employeesd
            {
                Name = dto.Name,
                Salary=dto.Salary,
                Phone=dto.Phone,
                Email=dto.Email,
                Age=dto.Age,
                Department=dto.Department,
                JoiningDate=dto.JoiningDate


            };

            await _context.Employeesds.AddAsync(emp);
            await _context.SaveChangesAsync();

            return Ok(emp);

        }

        [HttpGet]
        [Route("GetEMPByEmail")]
        public async Task<IActionResult> getempbyemail(string email) {

            var employee = await _context.Employeesds.SingleOrDefaultAsync(e => e.Email == email);
            return Ok(employee);
        
        }

        [HttpGet]
        [Route("GetTotalempCount")]
        public async Task<IActionResult> gettotalempcount()
        {
            var count = await _context.Employeesds.CountAsync();

            return Ok(count);
        }
        [HttpGet]
        [Route("Gettop10")]
        public async Task<IActionResult> gettoptenemp() {

            var employee = await _context.Employeesds.OrderBy(e => e.Id).Take(10).ToListAsync();

            return Ok (employee);
        
        }

        [HttpGet]
        [Route("Getempbyemail")]
        public async Task<IActionResult> getempbyemailid(string email)
        {
            var employee = await _context.Employeesds.AnyAsync(e => e.Email == email);

            return Ok(employee);
        }

        [HttpGet]
        [Route("Getemployeenameonly")]
        public async Task<IActionResult> getempnameonly()
        {
            var empname = await _context.Employeesds.Select(e => e.Name).ToListAsync();
            return Ok(empname);
        }
        [HttpGet]
        [Route("Getallempnochnage")]
        public async Task<IActionResult> getempnochange()
        {
            var employee = await _context.Employeesds.AsNoTracking().ToListAsync();

            return Ok(employee);
        }


        [HttpGet]
        [Route("GetHighestID")]
        public async Task<IActionResult> gethighestid()
        {
            var employee = await _context.Employeesds.OrderByDescending(e => e.Id).FirstOrDefaultAsync();

            return Ok(employee);
        }


        [HttpGet]
        [Route("Getsalary")]
        public async Task<IActionResult> GetSalary()
        {
            var employee = await _context.Employeesds.Where(e => e.Salary > 50000).ToListAsync();

            return Ok(employee);
        }

        [HttpGet]
        [Route("Getsalarybetween")]
        public async Task<IActionResult> GetSalarybetween()
        {
            var employee = await _context.Employeesds.Where(e => e.Salary >= 30000 && e.Salary <= 60000).ToListAsync();

            return Ok(employee);
        }
        [HttpGet]
        [Route("GetIITdeptemp")]
        public async Task<IActionResult> Getiddept()
        {
            var employee = await _context.Employeesds.Where(e => e.Department == "IT").ToListAsync();

            return Ok(employee);
        }
        [HttpGet]
        [Route("GetIITdeptempandsalarymorethan50k")]
        public async Task<IActionResult> get50k()
        {
            var employee = await _context.Employeesds.Where(e => e.Department == "IT" && e.Salary >= 50000).ToListAsync();

            return Ok(employee);
        }

        [HttpGet]
        [Route("GetempIT&HR")]
        public async Task<IActionResult> getempithr()
        {
            var employee = await _context.Employeesds.Where(e => e.Department == "IT" || e.Department == "HR").ToListAsync();
            return Ok(employee);
        }

        [HttpGet]
        [Route("ListofdeptfromUser")]
        public async Task<IActionResult> ListofdeptfromUser()
        {
            var dept = new List<string> { "IT", "HR", "RPA" };

            var EMPLOYEE = await _context.Employeesds.Where(e => dept.Contains(e.Department)).ToListAsync();
            return Ok(EMPLOYEE);
                


        }
        [HttpGet]
        [Route("EmpNotinSalesSupport")]
        public async Task<IActionResult> empnotinsalessupport()
        {
            var empdept = new List<string> { "Sales", "Support" };
            var emp = await _context.Employeesds.Where(e => !empdept.Contains(e.Department)).ToListAsync();
            return Ok(emp);
        }

        [HttpGet]
        [Route("Younger30")]
        public async Task<IActionResult> Getyoung()
        {
            var emp = await _context.Employeesds.Where(e => e.Age < 30).ToListAsync();
            return Ok(emp);
        }

        [HttpGet]
        [Route("YoungerBetween25&30")]
        public async Task<IActionResult> Getyoungbtn()
        {
            var emp = await _context.Employeesds.Where(e => e.Age > 25 && e.Age < 30).OrderBy(e => e.Age).ToListAsync();
            return Ok(emp);
        }

        [HttpGet]
        [Route("EmpnamestartwithA")]
        public async Task<IActionResult> getempnamestartwitha()
        {
            var emp = await _context.Employeesds.Where(e => e.Name.StartsWith("A")).ToListAsync();
            return Ok(emp);
        }
    }
}
