using System;
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
        [HttpGet("get")]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetTasks(){
            return await _context.TaskItems.ToListAsync();
        }

        [HttpPost("post")]
        public async Task<ActionResult<TaskItem>> PostTask(TaskItem item){
            _context.TaskItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTasks), new { id = item.Id }, item);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<TaskItem>> DeleteTask(TaskItem item){
            _context.TaskItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTasks), new { id = item.Id }, item);
        }
    }
}