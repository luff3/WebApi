using lab04.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace lab04.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ShopContext _shopContext;
        public EmployeeController(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            if (_shopContext.Brands == null)
            {
                return NotFound();
            }
            else
            {
                return await _shopContext.Employees.ToListAsync();
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            if (_shopContext.Employees == null)
            {
                return NotFound();
            }

            var employee = await _shopContext.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }
            else
            {
                return employee;
            }
        }

    }
}
