using System.Reflection;

namespace Catalog.API.Core
{
    public class DependencyContextAssemblyCatalogCustom : DependencyContextAssemblyCatalog
    {
        public override IReadOnlyCollection<Assembly> GetAssemblies()
        {
            return [typeof(Program).Assembly];
        }
    }
}