using System.Reflection;

namespace Basket.API.Core
{
    public class DependencyContextAssemblyCatalogCustom : DependencyContextAssemblyCatalog
    {
        public override IReadOnlyCollection<Assembly> GetAssemblies()
        {
            return [typeof(Program).Assembly];
        }
    }
}