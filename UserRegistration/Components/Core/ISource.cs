using UserRegistration.Models;

namespace UserRegistration.Components.Core
{
    public interface ISource
    {
        UserSourceModel[] Read();
    }
}
