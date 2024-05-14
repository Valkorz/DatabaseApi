using System;

namespace TaskAPI.Content{

    public class Task{
        public string Name {get; internal set;}
        public int Id {get; internal set;}

        public Task(string name, int id){
            Name = name;
            Id = id;
        }
    }
}