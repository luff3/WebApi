using lab04.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace lab04.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly ShopContext _shopContext;

        public BrandController(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brand>>> GetBrands()
        {
            if(_shopContext.Brands == null)
            {
                return NotFound();
            }
            else
            {
                return await _shopContext.Brands.ToListAsync();
                //return Ok(brands);
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Brand>> GetBrand(int id)
        {
            if (_shopContext.Brands == null)
            {
                return NotFound();
            }
            
            var brand = await _shopContext.Brands.FindAsync(id);

            if(brand == null) 
            {
                return NotFound();
            }
            else
            {
                return brand;
            }
        }

        [HttpPost]
        public async Task<ActionResult<Brand>> PostBrand(Brand brand)
        {
            _shopContext.Brands.Add(brand);
            await _shopContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBrand), new {id = brand.ID}, brand);
        }
        [HttpPut]
        public async Task<IActionResult> PutBrand(int id, Brand brand)
        {
            if(id != brand.ID)
            {
                return BadRequest();
            }
            _shopContext.Entry(brand).State = EntityState.Modified;

            try
            {
                await _shopContext.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException) 
            { 
                if(!BrandAvailable(id))
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

        private bool BrandAvailable(int id)
        {
            return (_shopContext.Brands?.Any(x => x.ID == id)).GetValueOrDefault();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBrand(int id)
        {
            if(_shopContext.Brands == null)
            {
                return NotFound();
            }
            var brand = await _shopContext.Brands.FindAsync(id);

            if(brand == null)
            {
                return NotFound();
            }
            _shopContext.Brands.Remove(brand);
            await _shopContext.SaveChangesAsync();
            return Ok();

        }


    }
}
