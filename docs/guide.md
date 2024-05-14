

# ASP.NET: A Step-by-step guide on creating your first API.
### By Vittorio Pivarci

---

## Introduction
This documentation explains how to construct a RestAPI using C# and ASP.NET, and an overview of the implementations of this solution. 

## Step 1: Create the project

    1. Open the console;
    2. In the desired directory, type "dotnet new sln -o {Name}" (replacing {Name}) with your name of choice;
    3. Enter the folder and type "dotnet new classlib -o {Name}.{Name}" to create a class library folder;
    4. In the same folder, type "dotnet new webapi -o {Name}" to create a webapi template project.

The above procedures will generate a solution for your project, a class library for your custom implementations and behavior, and a webapi project which will handle the communication between your classes and an http address.

## Step 2: Adding controllers

Controllers serve as a way to communicate between the website and the machine using http actions, such as ``` GET ``` and ```PUT```. Defining a controller defines a new page for your website. In our case, this website is only a handle for getting/setting information and is not intended to have a designed user interface.
To add controllers, follow these steps:

    1. In the generated webapi project folder, create a "Controllers" folder;
    2. Create a new script;
    3. Inside the script, define the controller, including the asp.net and http usings and directives.

The resulting script should be as follows:

```csharp

//Usings for communicating with the API
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;

//Namespace is optional
namespace Tasks.Controllers{

    //The class name should include "Controller" after it's actual name.
    [ApiController]
    [Route("[controller]")]
    public class PageController : ControllerBase{

        /*This method is called upon entering the following address:
        http://localhost<port>/page
        */
        [HttpGet]
        public string Ping(){
            return "pong";
        }
       
    }
    
}
```
The code above defines a class named "PageController" as an API controller (to communicate between machine and server). The class then defines a method called "Ping", which returns "pong". This method is marked as a ```GET``` operation using the ```[HttpGet]``` attribute, this means that the method will be called once the client enters the specified address.

## Step 3: The address

Running the project will result in an address, this address is the local address generated once the computer starts hosting the server, it usually looks like this:

```
    http://localhost:<port>/
```

The ```<port>``` is a placeholder. The port will be generated by the computer upon running the dotnet solution, usually represented by a 4 digit number, such as ```5000``` or ```5001```.

## Step 4: Configuring the controllers

After adding the controllers inside their respective folder, you must specify their usage in the root code of your webapi solution, usually labeled as ```Program.cs```.
Open the file, and replace the generated template with the following code:

```csharp
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();  //Map controllers in the folder

app.Run();
```

This code, upon compilation, will initialize the WebAPI and specify all the controllers in the dedicated folder. This step is **crucial** to ensure the availability of the controllers during runtime.

## Step 5: Running the solution

In the console/command prompt, enter the folder where the webapi solution ```(.sln)``` is located, then type ```dotnet run``` to initialize the server for the API. The output should be the following (address port may be different):

```batch
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5000
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
      Content root path: C:\{YourPath}\{FolderName}
```
``` Content root path: C:\{YourPath}\{FolderName} ``` specifies the target of your WebAPI solution.

## Step 6: Navigating the website

While it is not an actual "website", given the fact that the subject of matter is an API, it is still possible to navigate the http addresses, because that will be used by other programs whom attempt to communicate with the API. It doesn't have an interface, but the return contents of methods will be displayed on the screen of your browser.
Paste the generated ```localhost``` link into your browser of choice. Accessing different controllers requires you to access the page labeled by their name minus "Controller", as follows:

```
    PageController = http://localhost:5000/Page
```
Entering the address above will redirect you to the page desbribed by the PageController class, which should display "pong" as specified by the Ping method's return value:

![Example](ImageExample.PNG)

## Step 7: Communicating with the API from another program

Now comes the most intriguing functionality, the inter-process communication. While your website is being hosted, other programs can generate http requests to it. The fun part is that any language can generate these requests.
Below are a few examples of http requests for different programming languages:

### C#:
```csharp
using System.Net.Http;
using System.Threading.Tasks;

public async Task GetTasksAsync()
{
    HttpClient client = new HttpClient();
    HttpResponseMessage response = await client.GetAsync("http://localhost:5000/page");
    response.EnsureSuccessStatusCode();
    string responseBody = await response.Content.ReadAsStringAsync();
}
```

### JavaScript:
```js

fetch(`http://localhost:5000/page`)
    .then(response => response.text())
    .then(data => console.log(data))
    .catch((error) => {
      console.error('Error:', error);
    });

```

## Step 8: Adding a SQL Database

Let's go one step beyond, what if we added a way for users to add, modify and remove data from a database? Like sending messages in a forum for example. In order to add SQLite to your WebApi project, please follow these steps:

  1. **In the terminal inside the WebApi folder, run:**
    `dotnet add package Microsoft.EntityFrameworkCore.Sqlite`
    `dotnet add package Microsoft.EntityFrameworkCore.Design`
  2. **Create a model class**:
    