
## .NET Core C# API + Database project
---

This project is a task/todo list, using dotnet core MVC and Entity Framework to implement a database of tasks (in SQLite) that can be modified through http protocol commands, featuring GET, POST, PUT and DELETE.

---

### Files:

`./docs/guide.md`: a comprehensive step-by-step guide to building your own .Net CORE WebApi.

`.docs/httpAttributes.md`: a list of http attributes to be used when defining methods for a controller.

`./Tasks/`: the WebApi folder.

`./Tasks/Program.cs`: root file of the api, used for initializing controllers and other resources.

`.Tasks/Controllers/`: where the custom controller implementations will be stored.

`./Requesters/`: code examples in different programming languages to communicate with the API.


---

### Features:

- A Controller that creates a database, with GET and POST methods;
- Two program examples for calling http requests (javascript and batch);
- Detailed documentation;

---
### TODO:

- A second database structure created without Entity Framework (to allow for more manual customization);
- Finish the `httpAttributes.md` documentation.
- Add more http request examples for other programming languages;


