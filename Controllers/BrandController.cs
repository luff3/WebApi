using AutoMapper;
using lab04.Models;
using lab04.ViewModels;
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
        private readonly IMapper _mapper;

        public BrandController(ShopContext shopContext, IMapper mappper)
        {
            _shopContext = shopContext;
            _mapper = mappper;
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
                //return await _shopContext.Brands.ToListAsync();
                return Ok(_mapper.Map<IEnumerable<BrandViewModel>>(_shopContext.Brands));
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
                return Ok(_mapper.Map<BrandViewModel>(brand));
            }
        }

        [HttpPost]
        public async Task<ActionResult<Brand>> PostBrand([FromBody] BrandViewModel br) 
        {

              var brand = _mapper.Map<Brand>(br);
              _shopContext.Brands.Add(brand);
              await _shopContext.SaveChangesAsync();
              return Ok(brand);
            //return CreatedAtAction(nameof(GetBrand), new {id = brand.ID}, brand);
        }
        [HttpPut]
        public async Task<IActionResult> PutBrand(int id,[FromBody] BrandViewModel br)
        {

            var  brand = await _shopContext.Brands.FindAsync(id);

            if ( brand == null) return NotFound();

            _mapper.Map(br, brand);

           
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
             return Ok(_mapper.Map<BrandViewModel>(brand));
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
