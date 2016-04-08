using DAL;
using Microsoft.AspNet.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
 
namespace HelloMvc.Controllers
{
    [Route("api/[controller]")]
    public class ItemController : Controller
    {
        [HttpGet]
        public IEnumerable<Item> GetAll()
        {
            var allItems = new ItemContext().GetAll();
            return allItems;
        }
    }
}