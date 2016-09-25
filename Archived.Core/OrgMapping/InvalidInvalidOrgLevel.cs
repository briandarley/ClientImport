using Core.Infrastructure;
using Core.Interfaces;

namespace Core.OrgMapping
{
    public class InvalidInvalidOrgLevel : IInvalidOrgLevel
    {
        public ReasonTypes Reason { get; set; }
        public int Level { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
            
        public string ParentName { get; set; }
    }
}
