using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Backend_Leave.Models;

namespace Backend_Leave.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly EmployeeContext _employeeContext;
        public UserController(EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
        }

        [HttpPost("register")]
        public IActionResult Register(User user)
        {

            if (string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Password))
            {
                return BadRequest("Username and password are required");
            }


            var existingUser = _employeeContext.Users.FirstOrDefault(u => u.Username == user.Username);
            if (existingUser != null)
            {
                return BadRequest("Username already exists");
            }


            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);


            user.Password = hashedPassword;


            _employeeContext.Users.Add(user);
            _employeeContext.SaveChanges();

            return Ok("Registration successful");
        }

        [HttpPost("login")]
        public IActionResult Login(User user)
        {

            var existingUser = _employeeContext.Users.FirstOrDefault(u => u.Username == user.Username);

            if (existingUser == null)
            {
                return Unauthorized("Invalid credentials");
            }


            if (!BCrypt.Net.BCrypt.Verify(user.Password, existingUser.Password))
            {
                return Unauthorized("Invalid credentials");
            }

            return Ok("Login successful");
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<User>>> GetEmployee()
        {
            if (_employeeContext.Users == null)
            {
                return NotFound();
            }
            return await _employeeContext.Users.ToListAsync();
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteEmployee(int id)
        {
            if (_employeeContext.Users == null)
            {
                return NotFound();
            }

            var user = await _employeeContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            _employeeContext.Users.Remove(user);
            await _employeeContext.SaveChangesAsync();

            return Ok();
        }

    }
}
