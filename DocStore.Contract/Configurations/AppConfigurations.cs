namespace DocStore.Contract.Entities
{
    public class AppConfigurations
    {
        public ConnectionString ConnectionStrings { get; set; }
    }

    public class ConnectionString
    {
        public string DefaultConnection { get; set; }
    }
}
