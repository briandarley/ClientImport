namespace ClientImport.Infrastructure.Interfaces
{

    public interface IRecord<T> where T : new()
    {
        string Tier1CompanyId { get; set; }
        
    }
}
