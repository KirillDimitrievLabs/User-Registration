using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserRegistration.Models;
using UserRegistration.Components.PluginSystem;
using Microsoft.Extensions.Logging;
namespace UserRegistration.Components.Core
{
    public class Syncer
    {
        private static List<UserDestinationModel> ConvertedUserSources { get; set; }
        private IDestination[] Destinations { get; set; }
        private readonly ILogger _logger;

        public Syncer(ISource sourceConnection, IDestination[] destination, ILoggerFactory loggerFactory)
        {
            ConvertedUserSources = UserConverter.ToUserDestinationModel(sourceConnection.Read());
            
            Destinations = destination;
            _logger = loggerFactory.CreateLogger<Syncer>();
        }

        public async Task Sync()
        {
            
            foreach (IDestination destination in Destinations)
            {
                foreach (var convertedUserSource in ConvertedUserSources)
                {
                    List<string> serviceUsers = await destination.ReadUsers();
                    List<string> usersToDelete = GetUsersToDelete(ConvertedUserSources, serviceUsers);
                    if (usersToDelete.Count != 0 && usersToDelete != null)
                    {
                        foreach (var userToDelete in usersToDelete)
                        {
                            if (userToDelete != "Kirillasd" && userToDelete != "root" && userToDelete != "гость")
                            {
                                await DeleteUser(destination, userToDelete);
                            }
                        }
                    }
                    if (!serviceUsers.Contains(convertedUserSource.FullName))
                    {
                        await CreateUser(destination, convertedUserSource);
                    }
                }
            }
        }


        private async Task CreateUser(IDestination destination, UserDestinationModel userDestination)
        {
            List<string> existingUserGroups = await destination.ReadGroups();
            userDestination.Groups = GetComparedUserGroups(existingUserGroups, userDestination.Groups).ToArray();

            await destination.Save(userDestination);

            _logger.LogInformation($"{destination.GetType().Name}: User with name of '{userDestination.FullName}' was created");
        }

        private async Task DeleteUser(IDestination destination, string userName)
        {
            await destination.Delete(userName);
            _logger.LogInformation($"{destination.GetType().Name}: User with name of '{userName}' was deleted");
        }

        private static List<string> GetComparedUserGroups(List<string> exsistingGroupsList, string[] currentGroupsList)
        {
            List<string> tempUserGroups = new List<string>();
            foreach (string userGroup in currentGroupsList)
            {
                if (exsistingGroupsList.Contains(userGroup))
                {
                    tempUserGroups.Add(userGroup);
                }
            }
            return tempUserGroups;
        }
        private static List<string> GetUsersToDelete(List<UserDestinationModel> userDestination, List<string> serviceUsers) 
        {
            List<string> tempUserDestinationModels = new List<string>();
            userDestination.Select(name => name.FullName).Except(serviceUsers);
            tempUserDestinationModels = serviceUsers.Except(userDestination.Select(name => name.FullName)).ToList();
            return tempUserDestinationModels;
        }

    }
}