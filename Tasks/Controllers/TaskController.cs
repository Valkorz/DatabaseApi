using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.EntityFrameworkCore.Design;

namespace Tasks.Controllers
{
    //Create a task item class to be used as a base for the SQL table
    public class TaskItem
    {
        public int Id {get; set;}
        public string Name {get; set;}
        public bool IsComplete {get; set;}
    }

    /* 
        DbContext class for representing a database session for querying and saving instances of each entity.
    */
    public class TaskContext : DbContext
    {
        //Pass context options to base constructor
        public TaskContext(DbContextOptions<TaskContext> opts) : base(opts) {}
        
        public DbSet<TaskItem> TaskItems {get; set;}
    }

    
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly TaskContext _context; //Reference the database context the controller is reffering to

        public TaskController(TaskContext context){
            _context = context;
        }

        //Get all tasks in the database and return on get request
        [HttpGet("dump")]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetTasks(){
            return await _context.TaskItems.ToListAsync();
        }

        //Get single task in the database
        [HttpGet("get/{id}")]
        public async Tasks<ActionResult<TaskItem>> GetTask(int id){
            return await _context.TaskItems.FirstOrDefaultAsync(x => x.Id == id);
        }

        /*
            Add new Task element to database
            "Content-Type: application/json" -d '{`"name`":`"New Task`", `"isComplete`":false}' http://localhost:5259/Task/post
        */

        [HttpPost("post")]
        public async Task<ActionResult<TaskItem>> PostTask(TaskItem item){
            item.Id = _context.TaskItems.Count + 1;
            _context.TaskItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTasks), new { id = item.Id }, item);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateTask(int id, TaskItem item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.TaskItems.Any(x => x.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /*
            Delete element from database, specified by ID.
            http://localhost:5000/Task/delete/{id}
        */

        [HttpDelete("delete")]
        public async Task<ActionResult<TaskItem>> DeleteTask(int id){
            var item = await _context.TaskItems.FirstOrDefaultAsync(x => x.Id == id);

            if(item == null) return Content($"{id} not found in the database.");

            _context.TaskItems.Remove(item);
            await _context.SaveChangesAsync();

            return Content($"Removed item: {item}");
        }

        /*
            Clear database.
            http://localhost:5000/Task/clear
        */

        [HttpDelete("clear")]
        public async Tasks<ActionResult> Clear(){
            _context.TaskItems.Clear();
            await _context.SaveChangesAsync();
            return Content($"Cleared all data.");
        }
    }
}