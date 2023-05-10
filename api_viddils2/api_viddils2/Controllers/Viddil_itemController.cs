using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_viddils2.Models;

using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json.Linq;



namespace api_viddils2.Controllers
{
    [Route("api/viddils")]
    [ApiController]
    public class Viddil_itemController : ControllerBase
    {
        private readonly Viddil_context _context;

        public Viddil_itemController(Viddil_context context)
        {
            _context = context;
        }


        // GET: api/Viddil_item
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Viddil_item>>> GetViddilItems(string find = null, string sort = null, string type = "up")
        {
            var viddilItems = await _context.ViddilItems.ToListAsync();

            var polya = Function.Get_polya(new Viddil_item());
            if(sort != null)
            {
                if (!polya.Contains(sort)) return NotFound(new { message = "Attribute has not been found" });
            }

            if (sort != null && find == null)
            {
                var viddils = Function.Sorting(viddilItems, sort, type);
                return Ok(Function.Check_role( viddils));
            }

            if (find != null && sort == null)
            {
                var viddils = Function.Find(viddilItems, find);
                return Ok(Function.Check_role(viddils));
            }
            if (sort != null && find != null)
            {
                var viddils = Function.Find(viddilItems, find);
                viddils = Function.Sorting(viddils, sort, type);
                return Ok(Function.Check_role(viddils));
            }


            return Ok(Function.Check_role(viddilItems));
        }


        // GET: api/Viddil_item/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Viddil_item>> GetViddil_item(int id)
        {
          if (_context.ViddilItems == null)
          {
              return NotFound(new { message = "ID has not been found" });
          }
            var viddil_item = await _context.ViddilItems.FindAsync(id);

            if (viddil_item == null)
            {
                return NotFound(new { message = "ID has not been found" });
            }
            if (Function.return_user().role == "User")
            {
                if (viddil_item.state!="Published") return NotFound(new { message = "ID has not been found" });
            }
            return Ok(viddil_item);
        }

        

        // PUT: api/Viddil_item/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutViddil_item(int id, [FromBody] Viddil_item data )
        {
            if (Function.return_user().role == "User") return StatusCode(403, "you do not have permission");
            if (!Viddil_itemExists(id))
            {
                return StatusCode(404, "ID has not been found");
            }

            var viddil_item = await _context.ViddilItems.FindAsync(id);

            foreach (var pole in data.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                if (pole.GetValue(data).ToString() != "" && pole.GetValue(data).ToString() != "0")
                {
                    foreach (var pole2 in viddil_item.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
                    {
                        try
                        {
                            if (pole2.Name == pole.Name)
                            {
                                var value = Convert.ChangeType(pole.GetValue(data), pole.PropertyType);
                                pole.SetValue(viddil_item, value);
                            }
                        }
                        catch (System.FormatException) { return StatusCode(400, "do not valid data"); }
                        catch(System.ArgumentException) { return StatusCode(400, "do not valid data"); }
                    }
                }
                

            }
            var validat_error = validation.Valid_viddil(viddil_item);
            if (validat_error.Count > 0)
            {
                return BadRequest(validat_error);
            }

            viddil_item.id = id;
            viddil_item.state = "Draft";
            _context.Entry(viddil_item).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(400, "do not valid data");
            }
            return Ok("viddil was updated");

    }

        // POST: api/Viddil_item
        [HttpPost]
        public async Task<ActionResult<Viddil_item>> PostTodoItem(Viddil_item todoItem)
        {
            if (Function.return_user().role == "User") return new StatusCodeResult(403);
            var validat_error = validation.Valid_viddil(todoItem);
            if (validat_error.Count > 0)
            {
                return BadRequest(validat_error);
            }
            var viddilItems = await _context.ViddilItems.ToListAsync();
            

            todoItem.id = viddilItems.Max(x=>x.id)+1 ;
            todoItem.state = "Draft";
            _context.ViddilItems.Add(todoItem);

            if (!TryValidateModel(todoItem))
            {
                return BadRequest(ModelState);
            }

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetViddilItems), new { id = todoItem.id }, todoItem);

        }


        // DELETE: api/Viddil_item/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteViddil_item(int id)
        {
            if (Function.return_user().role != "Admin")return new StatusCodeResult(403);

            if (_context.ViddilItems == null)
            {
                return NotFound(new { message = "ID has not been found" });
            }
            var viddil_item = await _context.ViddilItems.FindAsync(id);
            if (viddil_item == null)
            {
                return NotFound(new { message = "ID has not been found" });
            }

            _context.ViddilItems.Remove(viddil_item);
            await _context.SaveChangesAsync();

            return StatusCode(200, "viddil was deleted");
        }

        private bool Viddil_itemExists(int id)
        {
            return (_context.ViddilItems?.Any(e => e.id == id)).GetValueOrDefault();
        }

    }
}
