using Microsoft.AspNetCore.Mvc;
using TodosList.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodosList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        // GET: api/<TodosController>
        [HttpGet]
        public List<Todo> Get()
        {
            var db = new DBConnection();
            var todos = db.GetAllTodos();

            return todos;
        }

        // GET api/<BooksController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var db = new DBConnection();
            var todo = db.GetTodoById(id);
            if (todo == null)
            {
                return NotFound();
            }
            return Ok(todo);
        }

        // POST api/<BooksController>
        [HttpPost]
        public void Post([FromBody] Todo todo)
        {
            var db = new DBConnection();
            db.SaveTodo(todo);
        }

        // PUT api/<BooksController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Todo todo)
        {
            var db = new DBConnection();
            db.UpdateTodo(id, todo);
        }

        // DELETE api/<BooksController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var db = new DBConnection();
            db.DeleteTodoById(id);
        }
    }
}
