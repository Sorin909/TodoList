using System.Data.SqlClient;
using TodosList.Models;

namespace TodosList
{
    public class DBConnection
    {
        public List<Todo> GetAllTodos()
        {
            List<Todo> todos = new List<Todo>();

            var cmd = GetSqlCommand();

            cmd.CommandText = "SELECT * FROM Todos";

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var todo = new Todo()
                {
                    Id = int.Parse(reader["Id"].ToString()),
                    Title = reader["Title"].ToString(),
                   Date = DateTime.Parse(reader["Date"].ToString()).Date,
                };
                todos.Add(todo);

            }

            return todos;
        }

        public Todo GetTodoById(int Id)
        {
            var cmd = GetSqlCommand();

            cmd.CommandText = "SELECT * FROM Todos WHERE Id= @id";
            cmd.Parameters.AddWithValue("id", Id);

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var todo = new Todo()
                {
                    Id = int.Parse(reader["Id"].ToString()),
                    Title = reader["Title"].ToString(),
                    Date = DateTime.Parse(reader["Date"].ToString()).Date,

                };
                return todo;
            }
            return null;
        }

        public void SaveTodo(Todo todo)
        {
            var cmd = GetSqlCommand();

            cmd.CommandText = "Insert into Todos (Title,Date) VALUES (@title,@date)";

            cmd.Parameters.AddWithValue("title", todo.Title);
            cmd.Parameters.AddWithValue("date", todo.Date);


            cmd.ExecuteNonQuery();
        }

        public void UpdateTodo(int id, Todo todo)
        {
            var cmd = GetSqlCommand();
            cmd.CommandText = "UPDATE Todos SET Title=@title,Date=@date WHERE id=@id";
            cmd.Parameters.AddWithValue("id", id);
            cmd.Parameters.AddWithValue("title", todo.Title);
            cmd.Parameters.AddWithValue("date", todo.Date);



            cmd.ExecuteNonQuery();
        }

        public void DeleteTodoById(int id)
        {
            var cmd = GetSqlCommand();
            cmd.CommandText = "DELETE FROM Todos WHERE Id=@id";
            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();
        }

        private SqlCommand GetSqlCommand()
        {
            string connectionSring = "Data Source=localhost;Initial Catalog=TodosDB;Integrated Security=True;Encrypt=False";

            SqlConnection conn = new SqlConnection(connectionSring);

            conn.Open();

            SqlCommand cmd = conn.CreateCommand();


            cmd.CommandType = System.Data.CommandType.Text;

            return cmd;
        }
    }
}
