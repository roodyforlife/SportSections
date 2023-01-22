using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SportSections.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportSections.Controllers
{
    public class RequestsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Request(string request)
        {
            string connectionString = $"Server=DESKTOP-KIV92L3;Database=SportSectionsIHE;Trusted_Connection=True;Encrypt=False;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(request, connection);
                var result = new RequestViewModel();
                var reader = command.ExecuteReader();
                result.Displays = new string[reader.FieldCount];
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    result.Displays[i] = reader.GetName(i);
                }

                while (reader.Read())
                {
                    string[] value = new string[reader.FieldCount];
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        value[i] = reader.GetValue(i).ToString();
                    }

                    result.Result.Add(value);
                }

                return View(result);
            }
        }
    }
}
