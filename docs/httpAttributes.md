
<body>
    <div style=
    "width: px; 
    height: 15px;
    background-color: teal;">
    </div>
</body>

<h1 style = 
"color:powderblue;
font-family:verdana;
font-size:200%;
text-align:center;"> 
.NET WebAPI HTTP attributes guide 
</h1>

<body>
    <div style=
    "width: px; 
    height: 15px;
    background-color: teal;">
    </div>
</body>

---

In .NET WebAPI, HTTP attributes are used to define the behavior of API endpoints. These attributes provide a declarative way to specify the HTTP methods, routes, and other details for handling incoming requests.

---

<h1 style = 
"color:teal;
font-family:verdana;
font-size:150%;
text-align:center;"> 
[ApiController]
</h1>

This attribute is used for defining a class as an APIController, this is done so that it can be used to create endpoints between the web service and the backend structure.

Example:

```csharp
    [ApiController]
    [Route("[controller]")]
    public class PageController : ControllerBase{

        [HttpGet]
        public string displayMessage(){
            return "Hello World";
        }
       
    }
```

A Controller functions like a "branch" to your website, this means that the class above will print "Hello World" in the website `http://localhost:<port>/page`, "page" being whatever the class name is (minus the word "Controller", which is omitted automatically).

---

<h1 style = 
"color:teal;
font-family:verdana;
font-size:150%;
text-align:center;"> 
[Route("[controller]")]
</h1>


This attribute is used to define a template for the controller, whereas `[controller]` is a placeholder for the controller's actual name.

---

<h1 style = 
"color:teal;
font-family:verdana;
font-size:150%;
text-align:center;"> 
[HttpGet] or [HttpGet("name")]
</h1>

This attribute is used to define the method to respond to HttpGet requests. This means that it's the method executed when the user requests from it's specified URL. If chosen to have no parameters, such as `[HttpGet]` , the target URL will be the same as the controller's. However, if you want to add multiple methods capable of responding to this request, you can add a parameter that names a specific URL for it, like `[HttpGet("Page")]`.

Example:

```csharp
    //This is called when accessing http://localhost:<port>/page (Controller name)
    [HttpGet]
    public string foo(){
        return "hello";
    }
       
    //This is called when accessing http://localhost:<port>/page/foo2
    [HttpGet("foo2")]
    public string foo2(){
        return "goodbye";
    }
       

```

