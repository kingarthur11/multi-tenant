using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.DTOs
{
    public class CreateTodoRequest
    {
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
    }
}