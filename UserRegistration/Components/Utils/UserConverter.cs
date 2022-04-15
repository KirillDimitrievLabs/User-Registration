using System;
using System.Collections.Generic;
using UserRegistration.Models;
using System.Linq;

namespace UserRegistration.Components
{
    public static class UserConverter
    {
        public static UserDestinationModel ToUserDestinationModel(UserSourceModel userSourceModel)
        {
                UserDestinationModel userDestination = new UserDestinationModel 
                {
                    FullName = userSourceModel.FullName,
                    Login = userSourceModel.FullName.Replace(' ', '_'),
                    Avatar = userSourceModel.Avatar,
                    Disabled = userSourceModel.Disabled,
                    Email = userSourceModel.Email,
                    Groups = GroupFormater(userSourceModel.OrgStructure)
                };
            return userDestination;
        }
        public static List<UserDestinationModel> ToUserDestinationModel(UserSourceModel[] userSourceModels)
        {
            List<UserDestinationModel> userDestinationModel = new List<UserDestinationModel>();
            foreach (var userSourceModel in userSourceModels)
            {
                userDestinationModel.Add(ToUserDestinationModel(userSourceModel));
            }
            return userDestinationModel;
        }

        private static string[] GroupFormater(OrgStructureModel groupSourceString)
        {
            var Company = $"@{nameof(groupSourceString.Company)} {groupSourceString.Company}";
            var Office = $"@{nameof(groupSourceString.Office)} {groupSourceString.Office}";
            var Team = $"@{nameof(groupSourceString.Team)} {groupSourceString.Team}";

            string[] result = { Company, Office, Team };
            return result;
        }
    }
}