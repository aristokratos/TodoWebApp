using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AnotherTodo.Models.ViewModels;
using AnotherTodo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using AnotherTodo.Helper;


namespace Todo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public IActionResult Index()
        {
            var todoListViewModel = GetAllTodos();
            return View(todoListViewModel);
        }

        [HttpGet]
        public JsonResult PopulateForm(int id)
        {
            var todo = GetById(id);
            return Json(todo);
        }


        internal TodoViewModels GetAllTodos()
        {
            List<TodoItem> todoList = new();

            using (var db = DbHelper.connection())
                   
            {

                using (var tableCmd = db.CreateCommand())
                {
                    db.Open();
                    tableCmd.CommandText = "SELECT * FROM Another_Tb";

                    //SqlCommand cmd = new SqlCommand("SELECT * FROM Another_Tb", db);

                    using (SqlDataReader reader = tableCmd.ExecuteReader())
                       
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                todoList.Add(
                                    new TodoItem
                                    {
                                        Id = Convert.ToInt32(reader.GetString(0)),

                                        Name = reader.GetString(1)
                                        
                                    });
                            }
                        }
                        else
                        {
                            return new TodoViewModels
                            {
                                TodoList = todoList
                            };
                        }
                    };
                }
            }

            return new TodoViewModels
            {
                TodoList = todoList
            };
        }


        internal TodoItem GetById(int id)
        {
            TodoItem todo = new();

            using (var db = DbHelper.connection())
            {
                using (var tableCmd = db.CreateCommand())
                {
                    db.Open();
                    tableCmd.CommandText = $"SELECT * FROM Table Where Id = '{id}'";

                    using (var reader = tableCmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            todo.Id = Convert.ToInt32(reader.GetString(0));
                            todo.Name = reader.GetString(1);
                        }
                        else
                        {
                            return todo;
                        }
                    };
                }
            }

            return todo;
        }

        public RedirectResult Insert(TodoItem todo)
        {
            using (var db = DbHelper.connection())
            {
                using (var tableCmd = db.CreateCommand())
                {
                    db.Open();
                    tableCmd.CommandText = $"INSERT INTO Table (name) VALUES ('{todo.Name}')";
                    try
                    {
                        tableCmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return Redirect("https://localhost:7256/");
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            using (var db = DbHelper.connection())
            {
                using (var tableCmd = db.CreateCommand())
                {
                    db.Open();
                    tableCmd.CommandText = $"DELETE from Table WHERE Id = '{id}'";
                    tableCmd.ExecuteNonQuery();
                }
            }

            return Json(new { });
        }

        public RedirectResult Update(TodoItem todo)
        {
            using (var db = DbHelper.connection())
            {
                using (var tableCmd = db.CreateCommand())
                {
                    db.Open();
                    tableCmd.CommandText = $"UPDATE Table SET name = '{todo.Name}' WHERE Id = '{todo.Id}'";
                    try
                    {
                        tableCmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            return Redirect("https://localhost:7256/");
        }

        public ViewResult About()
        {
            throw new NotImplementedException();
        }
    }
}
