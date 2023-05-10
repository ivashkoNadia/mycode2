using api_viddils2.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace api_viddils2.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly Viddil_context _context;

        public UserController(Viddil_context context)
        {
            _context = context;
        }

        [HttpGet("/logout")]
        public async Task<ActionResult<IEnumerable<User_items>>> Log_outUsers()
        {
            if (Function.return_user().firstname == "") return BadRequest(new
            { message = "you can not log out because you did not log in" });
            Function.Del_user();
            return Ok(new { message = "log out was successfully" });
        }
        [HttpPost("/login")]
        public async Task<ActionResult<IEnumerable<User_registr>>> Log_inUsers(User_registr todoitem)
        {
            var users = await _context.UserItems.ToListAsync();
            foreach (var user in users)
            {
                if (user.email == todoitem.email && user.password == todoitem.password)
                {
                    Function.Set_user(user);
                    return StatusCode(200, "log in was successfully");
                }
            }

            return NotFound(new { message = "such user is not exist" });

        }

        [HttpPost("/registration")]
        public async Task<ActionResult<IEnumerable<User_items>>> Registr_inUsers(User_items todoItem)
        {
            var validat_error = validation.Valid_user(todoItem);
            if (validat_error.Count > 0)
            {
                return BadRequest(validat_error);
            }
            var userItems = await _context.UserItems.ToListAsync();
            if (userItems.Count == 0) todoItem.id = 1;
            else { todoItem.id = userItems.Max(x => x.id) + 1; }
            todoItem.role = "User";
            _context.UserItems.Add(todoItem);

            foreach (var item in userItems) { if (item.email == todoItem.email) return BadRequest(new { message = "such user already exists" }); }

            //if (!TryValidateModel(todoItem))
            //{
            //    return BadRequest(ModelState);
            //}
            await _context.SaveChangesAsync();

            return Ok(new { message = "the user was registered" });
        }

        [HttpPost("/send_to_moderation/{id}")]
        public async Task<ActionResult<Viddil_item>> For_Moderator(int id)
        {
            if (Function.return_user().role == "User") return StatusCode(403, "you do not have permission");
            var viddil_item = await _context.ViddilItems.FindAsync(id);

            if (viddil_item == null)
            {
                return NotFound(new { message = "ID has not been found" });
            }

            if (viddil_item.state == "Draft")
            {
                viddil_item.state = "Moderation";
                _context.Entry(viddil_item).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return StatusCode(200, "this item was sent to admin for publishing");
            }
            return StatusCode(404, "Such items not found with Draft state");
        }

        [HttpPost("/send_to_publishing/{id}")]
        public async Task<ActionResult<Viddil_item>> For_Admin(int id)
        {
            if (Function.return_user().role != "Admin") return StatusCode(403, "you do not have permission");
            var viddil_item = await _context.ViddilItems.FindAsync(id);

            if (viddil_item == null)
            {
                return NotFound(new { message = "ID has not been found" });
            }
            if (viddil_item.state == "Moderation")
            {
                viddil_item.state = "Published";
                _context.Entry(viddil_item).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return StatusCode(200, "this item was published");
            }

            return StatusCode(404, "Such items not found with Moderation state");
        }
    }

}

