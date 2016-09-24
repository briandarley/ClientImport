using Core.Infrastructure;
using Core.Interfaces;

namespace Core.JwsModels
{
    public class InvalidOrgLevel: IInvalidOrgLevel
    {
        public ReasonTypes Reason { get; set; }
        public int Level { get; set; }
        public string Name { get; set; }
        public string ParentName { get; set; }
        public string Number { get; set; }


        public static bool operator ==(InvalidOrgLevel item1, InvalidOrgLevel item2)
        {
            if (ReferenceEquals(item1, null)) return false;
            if (ReferenceEquals(item2, null)) return false;
            
            if (item1.Name != item2.Name) return false;
            if (item1.Number != item2.Number) return false;
            if (item1.ParentName != item2.ParentName) return false;
            if (item1.Level != item2.Level) return false;
            if (item1.Reason != item2.Reason) return false;
            return true;
        }

        public static bool operator !=(InvalidOrgLevel item1, InvalidOrgLevel item2)
        {
            return !(item1 == item2);
        }

        public override string ToString()
        {
            return $"{Level}\t\t{Number}\t\t{Name}\t\t\t\t{ParentName}";
        }
    }
}
