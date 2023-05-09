using ImageUpload.Data;
using ImageUpload.VM;
using Microsoft.AspNetCore.Mvc;
using ImageUpload.Models;
using Microsoft.EntityFrameworkCore;

namespace ImageUpload.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly IWebHostEnvironment _environment;


        public UserController(ApplicationContext applicationContext) {
            _context = applicationContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Index()
        {
            return await _context.Users.ToListAsync();
        }


    [HttpPost]
    public async Task<IActionResult> Create(UserDataModel userDataModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var path = "images";

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        string fileName = userDataModel.profilePicture.FileName;
        using (var fs = new FileStream(Path.Combine(path, fileName), FileMode.Create))
        {
            await userDataModel.profilePicture.CopyToAsync(fs);
        }

        User user = new User()
        {
            Name = userDataModel.Name,
            About = userDataModel.About,
            profilePicture = "images/" + fileName
        };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return Ok(user);
    }


}
}

