namespace Core.Interfaces
{
    public interface IOrgLevel
    {
        IOrgLevel ParentTier { get; set; }
        string TierId { get; set; }
        string Name { get; set; }
        string Number { get; set; }
        bool WcLob { get; set; }
    }
}
