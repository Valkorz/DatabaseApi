# Curl requests for HTTP

This document contains the CLI syntax for http curl commands, such as get, post, etc.

<h1 style="font-size:15px">
Last update:
22/05/2024 09:16 AM
 </h1>

---

## The syntax

Curl is a CLI and library for data transfer using different protocols. This document will be focused on curl's http/https implementations. The basic syntax is as follows:

    curl [options...] <url>

The `[options...]` parameter represents the set of actions that will be done, using the target `<url>` as reference. Let's break down the following command:

```batch
curl -X POST -H "Content-Type: application/json" -d "{\"name\":\"New Task\", \"isComplete\":false}" http://localhost:5259/Task/post
```

**`-X`:** using `-X`, we define what http operation to use, in this case it's `POST`.
**`-H`:** passes a custom header to the server, which serves to pass additional context or metadata about a piece of information sent. In this example, the header is being used to explain that the target format is a JSON.
**`-d:`:** using `-d`, we define the actual data to be sent to the server with the `POST` operation. It must be formatted according to the desired data type, in this case it's formatted as a JSON key-value pair.
**`<url>`:** finally, the `<url>` parameter is where it's specified the target route for the information. It must be aligned with the controller, so that the route is compatible with `POST` operations.

Running the example in the terminal will communicate with the server, and run the following method:

```csharp
[HttpPost("post")]
public async Task<ActionResult<TaskItem>> PostTask(TaskItem item){
    item.Id = _context.TaskItems.Count + 1;
    _context.TaskItems.Add(item);
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(GetTasks), new { id = item.Id }, item);
}
```

The `TaskItem` will be created according to the received data (by parsing the JSON information), and then it will be added into the collection.

---

## Other operations

Curl can perform other operations aside of `POST`, as showcased in the following list:

1. `POST`: Sends data to a server by URL:

```batch
curl -X POST -H "Define data type" -d "Define data" http://localhost:<port>/
```

2. `GET`: Gets data from an URL:

```batch
curl -X GET http://localhost:<port>/
```

3. `PUT`: Updates data on an URL:

```batch
curl -X PUT -H "Define data type" -d "Define data" http://localhost:<port>/
```
4. `DELETE`: Deletes data from an URL:

```batch
curl -X DELETE http://localhost:<port>/
```






