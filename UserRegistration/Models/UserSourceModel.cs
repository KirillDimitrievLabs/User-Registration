using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace UserRegistration.Models
{
    public class UserSourceModel : IUserModel
    {
        [YamlMember(Alias = "Fullname")]
        public string FullName { get; set; }

        [YamlMember(Alias = "Email")]
        public string Email { get; set; }

        [YamlMember(Alias = "Disabled")]
        public bool Disabled { get; set; }

        [YamlMember(Alias = "Avatar")]
        public string Avatar { get; set; }

        [YamlMember(Alias = "ServiceAction")]// TODO: Изменить тип для Avatar
        public string ServiceAction { get; set; }

        [YamlMember(Alias = "Orgstructure")]// TODO: Изменить тип для Avatar
        public OrgStructureModel OrgStructure { get; set; }
    }
    public class OrgStructureModel
    {
        [YamlMember(Alias = "Team")]
        public string Team { get; set; }

        [YamlMember(Alias = "Office")]
        public string Office { get; set; }

        [YamlMember(Alias = "Company")]
        public string Company { get; set; }
    }
}
