using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        IProductService _productService;
         //loosely coupled soyuta bağımlılık yani gevşek bağımlı
         //ama veri çekemez somut bişey yok o zamaaan IOC inversion of Control
         //değişimin kontrolü
         //bunun için eskiden startup şimdi ise program cs içine ekleme yapıldır addsingleton
         //AOP aspect oriented Program
        
        public ProductsController(IProductService _productManager) { this._productService = _productManager; } //bu servis managera ve productdal ise efye döndü

        [HttpGet("getall")]
        public IActionResult Get() //IActionResult
        {
            if(_productService.GetAll().Succes == false)
            {
                return BadRequest(ModelState);
            }
            return Ok(_productService.GetAll());
        }

        [HttpPost("add")]

        public IActionResult Post(Product product)
        {
            var result = _productService.Add(product);

            if(result.Succes)
            {
                return Ok (result);
                    
            }
            return BadRequest(result);

        }
        [HttpGet("getbyid")] //bununla /api/controller/getbyid?id=2
        public IActionResult Get(int id)
        {
            var result = _productService.GetById(id);
            if( result.Succes ) { return Ok(result.Data); } 
            return BadRequest(result);
            //api/controller?id=1

        }
    }
}
