using Microsoft.AspNetCore.Mvc;
using MvcMovie.Data;
using MvcMovie.Models;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;

namespace MvcMovie.Controllers;

public class LoginController : Controller
{
    
        private readonly MvcMovieContext _context;

        public LoginController(MvcMovieContext context)
        {
            _context = context;
        }
    // 
    // GET: /Login/
    public IActionResult Index()
    {
        return View();
    }
    // 
    // GET: /HelloWorld/Welcome/{name=Scott}&{numTimes=1}
    public IActionResult Welcome(string name="World", int numTimes = 1)
    {
        ViewData["Message"] = "Hello " + name;
        ViewData["NumTimes"] = numTimes;
        return View();
    }
    [HttpPost]
        public async Task<IActionResult> RegisterInput(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Name = model.Email,
                    Password = HashPassword(model.Password)
                };
                _context.Add(user);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
          public string HashPassword(string password)
{
    // Converter a senha em um array de bytes
    byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

    // Calcular o hash
    SHA256 sha256 = SHA256.Create();
    byte[] hashBytes = sha256.ComputeHash(passwordBytes);

    // Converter o hash de volta em uma string
    string hashString = BitConverter.ToString(hashBytes).Replace("-", "");

    return hashString;
}
}
