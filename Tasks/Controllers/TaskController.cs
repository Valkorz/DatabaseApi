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

        //Get single task in the database. replace {id} by the desired element id in the url.
        [HttpGet("get/{id}")]
        public async Task<ActionResult<TaskItem>> GetTask(int id){
            return await _context.TaskItems.FirstOrDefaultAsync(x => x.Id == id);
        }

        /*
            Add new Task element to database
            "Content-Type: application/json" -d '{`"name`":`"New Task`", `"isComplete`":false}' http://localhost:5259/Task/post
        */

        [HttpPost("post")]
        public async Task<ActionResult<TaskItem>> PostTask(TaskItem item){
            item.Id = _context.TaskItems.Count() + 1;
            _context.TaskItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTasks), new { id = item.Id }, item);
        }

        /*
            Update existing database element.
            PUT -H "Content-Type: application/json" -d "{ \"name\":\"taskName\", \"isComplete\":false}" http://localhost<port>/Task/update/{id}
        */

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateTask(int id, TaskItem item)
        {
            if(!_context.TaskItems.Any(x => x.Id == id)){
                return NotFound();
            }
            else{
                try{
                    TaskItem target = _context.TaskItems.Single(x => x.Id == id);
                    _context.TaskItems.Remove(target);
                    target = item;
                    target.Id = id;
                    _context.TaskItems.Add(target);
                    await _context.SaveChangesAsync();
                    return Content($"Updated id {id} to {item}.");
                }
                catch(Exception ex){
                    return Content(ex.ToString());
                }
            }
        }

        /*
            Delete element from database, specified by ID.
            http://localhost:5000/Task/delete/{id}
        */

        [HttpDelete("delete/{id}")]
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
        public async Task<ActionResult> Clear(){

            //Remove all items one by one because dbSet doesn't have a clear() method, bruh.
            var items  = _context.TaskItems;
            foreach(var item in items){
                _context.TaskItems.Remove(item);
            }
            await _context.SaveChangesAsync();
            return Content($"Cleared all data.");
        }


        //HTTP GET QUERY METHODS

        //Gets the completion rate for the last n number of tasks
        //Url FromQuery: ..Task/CompletionRate?count=10

        [HttpGet("CompletionRate")]
        public async Task<float> GetRecentCompletions([FromQuery] int count){
            var items = _context.TaskItems.ToList();
            int total = count, completions = 0;

            for(int i = items.Count() - count; i < items.Count(); i++){
                if(items[i].IsComplete) completions++;
            }

            float completionRate = total > 0? (float)completions / total : 0;
            return completionRate; 
        }

        //Returns the number of tasks on the collection
        [HttpGet("Count")]
        public async Task<int> Count(){
            return _context.TaskItems.Count();
        }
    }
}