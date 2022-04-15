namespace UserRegistration.Models
{
    interface IUserModel
    {
        string FullName { get; set; }
        string Email { get; set; }
        bool Disabled { get; set; }
        string Avatar { get; set; }
    }
}
