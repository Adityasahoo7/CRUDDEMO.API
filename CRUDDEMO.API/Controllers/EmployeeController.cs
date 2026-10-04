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

            var employee = await _context.Employeesds.FindAsync(id);
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


    }
}
