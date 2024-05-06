using DemoAPI.ActionFilters;
//using DemoAPI.Controllers.Action;
using DemoAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;

namespace DemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProductController : ControllerBase
    {
        readonly DemoAPIContext dc;
        private IMemoryCache cache;
       
        /// <summary>
        /// Constructor for injecting Democontext by the framework
        /// </summary>
        /// <param name="dc"></param>
        public ProductController(DemoAPIContext dc, IMemoryCache cache)
        {
            this.dc = dc;
            this.cache = cache;
        }
        /// <summary>
        /// Get all Products
        /// </summary>
        /// <returns>List of Products</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCateCTO>>> GetAllProducts()
        {
            ProductCateCTO[] pdt = await (from p in dc.Products
                                          join c in dc.Categories
                                          on p.CategoryCID equals c.CID into t
                                          from c in t.DefaultIfEmpty()
                                          select new ProductCateCTO
                                          {
                                              CategoryName = c == null ? "NA" : c.CName,
                                              CateId = c == null?0:c.CID,
                                              CName = p.PName,
                                              Price = p.price,
                                              CID = p.PID
                                          }).ToArrayAsync();
            return Ok(pdt);
           
            //Employee[] e = await dc.Employees.ToArrayAsync();
            //return Ok(e);
            //EmployeeDeptDTO[] edt = await (from e in dc.Employees
            //                               join d in dc.Departments
            //                               on e.DeptId equals d.DID
            //                               select new EmployeeDeptDTO
            //                               {
            //                                   DepartmentName = d.DName,
            //                                   DeptId = d.DID,
            //                                   EName = e.EName,
            //                                   ESalary = e.ESalary,
            //                                   EID = e.EID
            //                               }).ToArrayAsync();
        }
        /// <summary>
        /// Get Product by id
        /// </summary>
        /// <param name="id">The id of the Product</param>
        /// <returns>An Product object</returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ServiceFilter(typeof(MyLogResultFilter))]

     //   public async Task<ActionResult<Product>> GetProduct(int? id)
       // {
         //   Product p = new Product();
           // string mykey = "somedatakey";
            //if(cache.TryGetValue(mykey, out p) == false)
            //{
             //   var options = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(5));
                
              //  p = await dc.Products.FirstAsync(id);
               // if(p == null)
               // {
                 //   return NotFound("No Product Found");
              //  }
            //    cache.Set(mykey, p, options);
           // }
           // return Ok(p);
     //   }
        
        public async Task<ActionResult<Product>> GetProduct(int? id)
        {

            if (!id.HasValue)
            {
                return NotFound("No Product Id Specified");
            }
            Product p = new Product();
            // Product p = await dc.Products.FindAsync(id);
            string mykey = "SomedataKey";
            if(cache.TryGetValue(mykey, out p ) == false)
            {
                var options = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(5));
                p = await dc.Products.FindAsync(id);
                if(p == null)
                {
                    return NotFound("No Product Found");
                }
                cache.Set(mykey, p , options);
            }

           // if (p == null)
            
            return Ok(p);
        }
        /// <summary>
        /// Add an product
        /// </summary>
        /// <param name="p">The product data without id</param>
        /// <returns>The created product with ID</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
      
        public async Task<ActionResult<Product>> AddProduct([Bind(include: "EName, Price")] Product p)
        {
            p.CategoryCID = null;
            if (ModelState.IsValid)
                {
                dc.Products.Add(p);
                await dc.SaveChangesAsync(); //201  Resource Created
                return CreatedAtAction(nameof(GetProduct), new { id = p.PID }, p);

            }
            else
            {
                return BadRequest(ModelState);
            }
           
        }
        /// <summary>
        ///  Delete an product
        /// </summary>
        /// <param name="id"> The id of the product </param>
        /// <returns> The deleted </returns>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        
        public async Task<ActionResult<Product>> DeleteProduct(int? id)
        {
            if (id.HasValue)
            {
                return NotFound("No Product Id Specified");

            }
            Product p = await dc.Products.FindAsync(id);
            if (p == null)
            {
                return NotFound("No Product Found");
            }
            dc.Products.Remove(p);
            await dc.SaveChangesAsync();
            return Ok(p);
        }
        /// <summary>
        /// Update an product
        /// </summary>
        /// <param name="id">The id to be edited</param>
        /// <param name="p"> The product data</param>
        /// <returns> No Content</returns>
        /// 
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<Product>> UpdateProduct(int? id, Product p)
        {
            if(!id.HasValue)
            {
                return NotFound("No Product Id Specified");
            }
            if(id.Value != p.PID)
            {
                return BadRequest("ID mismatch");
            }
            dc.Entry(p).State = EntityState.Modified;
            //EF will automatcially generate the required update Command
            try
            {
                await dc.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!dc.Products.Any(pro => p.PID == pro.PID)) 
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
            //200 range -- everything is ok
            //400 - error
            //500 - Server error
        }
    }
}

