using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace LR7.Controllers
{
    public class FileController : Controller
    {
        [HttpGet]
        public IActionResult DownloadFile(string firstName = "", string lastName = "", string fileName = "")
        {
            // Перевірка на заповненість полів, тільки якщо дані отримані через GET
            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName) && !string.IsNullOrEmpty(fileName))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"First name: {firstName}\n");
                sb.Append($"Last name: {lastName}");
                byte[] bytes = Encoding.UTF8.GetBytes(sb.ToString());

                return File(bytes, contentType: "text/plain", $"{fileName}.txt");
            }
            else
            {
                // Передача введених значень назад у ViewData
                ViewData["FirstName"] = firstName;
                ViewData["LastName"] = lastName;
                ViewData["FileName"] = fileName;

                // Форма була надіслана, але є пусті поля -> попередження
                if (Request.Method == "GET" && (!string.IsNullOrEmpty(firstName) || !string.IsNullOrEmpty(lastName) || !string.IsNullOrEmpty(fileName)))
                {
                    ViewData["WarningMessage"] = "There are empty fields!";
                }
                return View();
            }
        }
    }
}
