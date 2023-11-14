using lab04.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lab04.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ShopContext _shopContext;

        public CustomerController(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            if (_shopContext.Customers == null)
            {
                return NotFound();
            }
            else
            {
                return await _shopContext.Customers.ToListAsync();
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            if (_shopContext.Customers == null)
            {
                return NotFound();
            }

            var customer = await _shopContext.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }
            else
            {
                return customer;
            }
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> PostEmployee(Customer customer)
        {
            _shopContext.Customers.Add(customer);
            await _shopContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCustomer), new { id = customer.ID }, customer);
        }


        [HttpPut]
        public async Task<IActionResult> PutEmployee(int id, Customer customer)
        {
            if (id != customer.ID)
            {
                return BadRequest();
            }
            _shopContext.Entry(customer).State = EntityState.Modified;

            try
            {
                await _shopContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerAvailable(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }

            }
            return Ok();
        }

        private bool CustomerAvailable(int id)
        {
            return (_shopContext.Customers?.Any(x => x.ID == id)).GetValueOrDefault();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            if (_shopContext.Customers == null)
            {
                return NotFound();
            }
            var customer = await _shopContext.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }
            _shopContext.Customers.Remove(customer);
            await _shopContext.SaveChangesAsync();
            return Ok();

        }

    }
}
