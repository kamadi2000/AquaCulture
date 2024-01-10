using Fish_Farm.DTOs;
using Fish_Farm.Entities;
using Fish_Farm.Repositories.WorkerRepository;

namespace Fish_Farm.Services.WorkerService
{
    public class WorkerService : IWorkerService
    {
        private readonly IWorkerRepository _workerRepository;
        public WorkerService(IWorkerRepository workerRepository)
        {
            _workerRepository = workerRepository;

        }
        public async Task<bool> AddWorker(WorkerDTO worker)
        {
            return await _workerRepository.AddWorker(worker);
        }

        public async Task<bool> DeleteWorker(int workerId)
        {
            return await (_workerRepository.DeleteWorker(workerId));
        }

        public async Task<bool> EditWorker(Worker worker)
        {
            return await _workerRepository.EditWorker(worker);
        }

        public async Task<List<Worker>> GetAllWorkers(int clientId, HttpRequest request)
        {
            return await _workerRepository.GetAllWorkers(clientId,request);
        }

        public async Task<List<Worker>> GetUnEmployedWorkers(int clientId)
        {
            return await _workerRepository.GetUnEmployedWorkers(clientId);
        }

        public async Task<Worker?> GetWorkerById(int workerId, HttpRequest request)
        {
            return await _workerRepository.GetWorkerById(workerId,request);
        }
    }
}
