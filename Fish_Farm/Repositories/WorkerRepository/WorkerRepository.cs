using Azure.Core;
using Fish_Farm.Data;
using Fish_Farm.DTOs;
using Fish_Farm.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Fish_Farm.Repositories.WorkerRepository
{
    public class WorkerRepository : IWorkerRepository
    {
        private readonly DataContext _dataContext;
        private readonly IWebHostEnvironment _hostEnvironment;

        public WorkerRepository(DataContext dataContext, IWebHostEnvironment hostEnvironment)
        {
            _dataContext = dataContext;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<bool> AddWorker(WorkerDTO worker)
        {
            if (worker.ImageFile != null)
            {
                worker.ImageName = await SaveImage(worker.ImageFile);
            }
            _dataContext.WorkerTable.Add(new Worker
            {
                Name = worker.Name,
                Age = worker.Age,
                Position = worker.Position,
                ImageFile = worker.ImageFile,
                ImageName = worker.ImageName,
                Email = worker.Email,
                ClientId = worker.ClientId,

            });
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteWorker(int workerId)
        {
            var user = await _dataContext.WorkerTable.FindAsync(workerId);
            if (user == null)
            {
                return false;
            }
            _dataContext.WorkerTable.Remove(user);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EditWorker(Worker worker)
        {
            var user = await _dataContext.WorkerTable.FindAsync(worker.Id);
            if (user == null)
            {
                return false;
            }
            if (worker.ImageFile != null)
            {
                user.ImageName = await SaveImage(worker.ImageFile);
            }
            user.Email = worker.Email;
            user.Position = worker.Position;
            user.Age = worker.Age;
            user.Name = worker.Name;
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Worker>> GetAllWorkers(int clientId,HttpRequest request)
        {
            return await _dataContext.WorkerTable
                .Where(wt => wt.ClientId == clientId)
                .Select(x => new Worker()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Age = x.Age,
                    Email = x.Email,
                    ImageName = x.ImageName,
                    Position = x.Position,
                    ImageSrc = String.Format("{0}://{1}{2}/Images/{3}", request.Scheme, request.Host, request.PathBase, x.ImageName)
                })
                .ToListAsync();
        }

        public async Task<List<Worker>> GetUnEmployedWorkers(int clientId)
        {
            var unEmployedWorkers = await _dataContext.WorkerTable
                .Where(x => x.ClientId == clientId)
                .Where(w => w.FishFarmId == null)
                .ToListAsync();
            if (unEmployedWorkers == null)
            {
                return new List<Worker>();
            }
            return unEmployedWorkers;
        }

        public async Task<Worker?> GetWorkerById(int workerId, HttpRequest request)
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
                   ImageSrc = String.Format("{0}://{1}{2}/Images/{3}", request.Scheme, request.Host, request.PathBase, x.ImageName)
               })
               .FirstOrDefaultAsync(x => x.Id == workerId);
            if (user == null)
            {
                return null;
            }
            return user;
        }

        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
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
