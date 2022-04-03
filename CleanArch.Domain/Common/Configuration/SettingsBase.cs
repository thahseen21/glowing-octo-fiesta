using System.Reflection;

namespace CleanArch.Domain.Common.Configurations
{
    public interface ISettingsBase
    {
        void Validate();
    }

    public class SettingsBase : ISettingsBase
    {
        public void Validate()
        {
            PropertyInfo[] properties = GetType().GetProperties();

            foreach (var property in properties)
            {
                if (string.IsNullOrWhiteSpace(property.GetValue(this)!.ToString()))
                {
                    throw new ArgumentNullException($"{property.Name} in {property.ReflectedType?.FullName} was null or empty");
                }
            }
        }
    }
}
