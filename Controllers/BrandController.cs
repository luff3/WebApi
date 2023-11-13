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
            }
        }


        [HttpGet]
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
    }
}
