using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using WebMinRouteGroup.Data;

namespace WebMinRouteGroup;

public static class TodoEndpointsV1
{
    public static RouteGroupBuilder MapTodosApiV1(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetAllTodos);
        group.MapGet("/{id}", GetTodo);
        group.MapPost("/", CreateTodo)
            .AddEndpointFilter(async (efiContext, next) =>
            {
                var param = efiContext.GetArgument<TodoDto>(0);

                var validationErrors = Utilities.IsValid(param);

                if (validationErrors.Any())
                {
                    return Results.ValidationProblem(validationErrors);
                }

                return await next(efiContext);
            });

        group.MapPut("/{id}", UpdateTodo);
        group.MapDelete("/{id}", DeleteTodo);

        return group;
    }

    // get all todos
    // <snippet_1>
    //public static async Task<Ok<Todo[]>> GetAllTodos(TodoGroupDbContext database)
    //{
    //    //var todos = await database.Todos.ToArrayAsync();
    //    //Todo[] todos = new Todo[5];
    //    //Todo[] todos = new Todo[] { };
    //    List<Todo> todoList = new List<Todo>();
    //    //// Define the connection string
    //    //string connectionString = "Data Source=localhost;Database=Todo;User id=sa;Password=Pa$$w0rd;TrustServerCertificate=true";
    //    //// Define the query with parameters
    //    //string query = "SELECT  * FROM Todo FOR Json Path";
    //    //string str = "";
    //    //using (SqlConnection connection = new SqlConnection(connectionString))
    //    //using (SqlCommand command = new SqlCommand(query, connection))
    //    //{
    //    //    connection.Open();
    //    //    SqlDataReader dr = command.ExecuteReader();
    //    //    while (dr.Read())
    //    //    {
    //    //        str = dr.GetString(0).ToString();
    //    //        //Console.WriteLine(str);
    //    //    }
    //    //    connection.Close();
    //    //}
    //    //var todos = str;
        
    //    //todos[0] = new Todo();
    //    //todos[0].Id = 1;
    //    //todos[0].Title = "Task2";
    //    //todos[0].Description = "Description";
    //    //todos[0].IsDone = true;
    //    Todo todo = new Todo();
    //    todo.Id = 1;
    //    todo.Title = "Task2";
    //    todo.Description = "Description";
    //    todo.IsDone = true;
    //    todoList.Add(todo);
    //    todo.Id = 2;
    //    todo.Title = "Walk dog";
    //    todo.Description = "Walk dog";
    //    todo.IsDone = true;
    //    todoList.Add(todo);
    //    var todos = todoList.ToArray();
    //    return TypedResults.Ok(todos);
    //}

    public static async Task<Ok<Todo[]>> GetAllTodos(TodoGroupDbContext database)
    {
        List<Todo> todoList = new List<Todo>();
        Todo todo = new Todo();
        todo.Id = 1;
        todo.Title = "Task2";
        todo.Description = "Description";
        todo.IsDone = true;
        todoList.Add(todo);
        todo.Id = 2;
        todo.Title = "Walk dog";
        todo.Description = "Walk dog";
        todo.IsDone = true;
        todoList.Add(todo);
        var todos = todoList.ToArray();
        return TypedResults.Ok(todos);
    }
    // </snippet_1>

    // get todo by id
    public static async Task<Results<Ok<Todo>, NotFound>> GetTodo(int id, TodoGroupDbContext database)
    {
        var todo = await database.Todos.FindAsync(id);

        if (todo != null)
        {
            return TypedResults.Ok(todo);
        }

        return TypedResults.NotFound();
    }

    // create todo
    public static async Task<Created<Todo>> CreateTodo(TodoDto todo, TodoGroupDbContext database)
    {
        var newTodo = new Todo
        {
            Title = todo.Title,
            Description = todo.Description,
            IsDone = todo.IsDone
        };

        await database.Todos.AddAsync(newTodo);
        await database.SaveChangesAsync();

        return TypedResults.Created($"/todos/v1/{newTodo.Id}", newTodo);
    }

    // update todo
    public static async Task<Results<Created<Todo>, NotFound>> UpdateTodo(Todo todo, TodoGroupDbContext database)
    {
        var existingTodo = await database.Todos.FindAsync(todo.Id);

        if (existingTodo != null)
        {
            existingTodo.Title = todo.Title;
            existingTodo.Description = todo.Description;
            existingTodo.IsDone = todo.IsDone;

            await database.SaveChangesAsync();

            return TypedResults.Created($"/todos/v1/{existingTodo.Id}", existingTodo);
        }

        return TypedResults.NotFound();
    }

    // delete todo
    public static async Task<Results<NoContent, NotFound>> DeleteTodo(int id, TodoGroupDbContext database)
    {
        var todo = await database.Todos.FindAsync(id);

        if (todo != null)
        {
            database.Todos.Remove(todo);
            await database.SaveChangesAsync();

            return TypedResults.NoContent();
        }

        return TypedResults.NotFound();
    }
}
