using Archived.Core.Infrastructure;

namespace Archived.Core.Interfaces
{
    public interface IInvalidOrgLevel
    {
        ReasonTypes Reason { get; set; }
        int Level { get; set; }
        string Name { get; set; }
        string ParentName { get; set; }
        string Number { get; set; }


    }
}
