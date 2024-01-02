using Fish_Farm.Data;
using Fish_Farm.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fish_Farm.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IWebHostEnvironment _hostEnvironment;

        public WorkerController(DataContext dataContext, IWebHostEnvironment hostEnvironment)
        {
            _dataContext = dataContext;
            _hostEnvironment = hostEnvironment;
        }
        [HttpGet]
        public async Task<ActionResult<List<Worker>>> GetWorker()
        {
            return Ok(await _dataContext.WorkerTable
                .Select(x => new Worker()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Age = x.Age,
                    Email = x.Email,
                    ImageName = x.ImageName,
                    Position = x.Position,
                    ImageSrc = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, x.ImageName)
                })
                .ToListAsync());
        }
        [HttpPost]
        public async Task<ActionResult<Worker>> AddWorker([FromForm]Worker worker)
        {
            if (worker.ImageName != null)
            {
                worker.ImageName = await SaveImage(worker.ImageFile);
            }
            _dataContext.WorkerTable.Add(worker);
            await _dataContext.SaveChangesAsync();
            return Ok(worker);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteWorker(int id)
        {
            var user = await _dataContext.WorkerTable.FindAsync(id);
            if (user == null)
            {
                return BadRequest("User not found");
            }
            _dataContext.WorkerTable.Remove(user);
            await _dataContext.SaveChangesAsync();
            return Ok("Successful");
        }
        [HttpPut]
        public async Task<ActionResult<Worker>> EditWorker(Worker worker)
        {
            var user = await _dataContext.WorkerTable.FindAsync(worker.Id);
            if (user == null)
            {
                return BadRequest("User not found");
            }
            if (worker.ImageName != null)
            {
               user.ImageName = await SaveImage(worker.ImageFile);
            }
            user.Email = worker.Email;
            user.Position = worker.Position;
            user.Age = worker.Age;
            user.Name = worker.Name;
            await _dataContext.SaveChangesAsync();
            return Ok("Successful");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Worker>> GetWorkerById(int id)
        {
            var user = await _dataContext.WorkerTable
                .Select(x => new Worker()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Age = x.Age,
                    Email = x.Email,
                    ImageName = x.ImageName,
                    Position = x.Position,
                    ImageSrc = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, x.ImageName)
                })
                .FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return BadRequest("User not found");
            }
            return Ok(user);
        }
        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imageName = new  String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ','-');
            imageName = imageName+DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            Console.WriteLine(imageName);
            return imageName;
        }

    }
}
