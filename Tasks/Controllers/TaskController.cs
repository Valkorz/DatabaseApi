using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.EntityFrameworkCore.Design;

namespace Tasks.Controllers
{
    //Create a task item class to be used as a base for the SQL table
    public class TaskItem
    {
        public itn Id {get; set;}
        public string Name {get; set;}
        public bool IsComplete {get; set;}
    }

    /* 
        DbContext class for representing a database session for querying and saving instances of each entity.
    */
    
    
    [ApiController]
    [Route("[controller]")]
    public class TaskController
    {
        
    }
}