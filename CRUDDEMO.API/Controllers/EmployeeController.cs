using CRUDDEMO.API.Models;
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
            var employee = await _context.Employeesds.FromSqlRaw("exec getempbyid @id ={0}", id).ToListAsync();
            return Ok(employee);
        }
    }
}
