using CleanArch.Domain.Common.Configurations;

namespace CleanArch.Domain.Common.Configuration
{
    public class AppSettings : IAppSettings
    {
        public ConnectionString ConnectionStrings { get; set; } = null!;

        public void Validate()
        {
            ConnectionStrings.Validate();
        }
    }

    public interface IAppSettings
    {
        public ConnectionString ConnectionStrings { get; set; }
    }
}
