using BankAPI.Data;
using BankAPI.Data.BankModels;

namespace BankAPI.Service
{
    public class ClientService
    {
        private readonly BankContext _context;
        public ClientService(BankContext context)
        {
            _context = context;
        }
        public IEnumerable<Client> Get()
        {
            return _context.Clients.ToList();
        }


        public Client? GetById(int id)
        {
            return _context.Clients.Find(id);
        }

        public Client Create(Client newClient)
        {
            _context.Clients.Add(newClient);
            _context.SaveChanges();

            return newClient;
        }


        public void Update(Client client)
        {
            var existingClient = _context.Clients.Find(client.Id);
            if (existingClient is not null)
            {
                existingClient.Name = client.Name;
                existingClient.PhoneNumber = client.PhoneNumber;
                existingClient.Email = client.Email;
                _context.SaveChanges();

            }
        }


        public void Delete(int id)
        {
            var existingClient = _context.Clients.Find(id);
            if (existingClient is not null)
            {
                _context.Clients.Remove(existingClient);
                _context.SaveChanges();

            }

        }
    }
}
