using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Tasks.Controllers{

    [ApiController]
    [Route("[controller]")]
    public class PageController : ControllerBase{

        [HttpGet]
        public string ping(){
            return "pong";
        }
       
    }
}