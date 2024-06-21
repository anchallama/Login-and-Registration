using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using Login_and_Registration.Models;
using Login_and_Registration.Repository;
using Login_and_Registration.Service;

public class AccountController : Controller
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;

    public AccountController(IUserRepository userRepository, IPasswordService passwordService)
    {
        _userRepository = userRepository;
        _passwordService=passwordService;
    }

    [HttpGet]
    public IActionResult SignUp()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SignUp(User model)
    {
        if (ModelState.IsValid)
        {
            var hashedPassword = _passwordService.HashPassword(model.PasswordHash);
            model.PasswordHash = hashedPassword;
            await _userRepository.AddUser(model);
            return RedirectToAction("Login");
        }

        return View(model);
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string username, string password)
    {
        if (ModelState.IsValid)
        {
            var user = await _userRepository.GetUserByUsername(username);

            if (user != null && _passwordService.VerifyPassword(user.PasswordHash, password))
            {
                // Implement authentication logic here
                // For example, setting up cookies or using ASP.NET Core Identity
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        }

        return View();
    }



  
}
