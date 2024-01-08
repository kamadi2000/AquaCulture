using Fish_Farm.Entities;

namespace Fish_Farm.Services.WorkerService
{
    public interface IWorkerService
    {
        Task<List<Worker>> GetAllWorkers(int clientId, HttpRequest request);
        Task<bool> AddWorker(Worker worker);
        Task<bool> DeleteWorker(int workerId);
        Task<bool> EditWorker(Worker worker);
        Task<Worker?> GetWorkerById(int workerId, HttpRequest request);
        Task<List<Worker>> GetUnEmployedWorkers(int clientId);
    }
}
