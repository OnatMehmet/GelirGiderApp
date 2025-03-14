using Microsoft.EntityFrameworkCore;

namespace GelirGiderApp.Models.Configurations
{
    internal class IndexAnnotation
    {
        private IndexAttribute _indexAttribute;

        public IndexAnnotation(IndexAttribute indexAttribute)
        {
            _indexAttribute = indexAttribute;
        }
    }
}