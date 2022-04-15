using System.Collections.Generic;
using System.Threading.Tasks;
using UserRegistration.Models;

namespace UserRegistration.Components.PluginSystem
{
    public interface IDestination
    {
        public string Name { get; }
        //public string ConnectionType { get; }
        Task <List<string>> ReadUsers();
        Task<List<string>> ReadGroups();
        Task Save(UserDestinationModel userToSave);
        Task Delete(string userLogin);
    }
}