namespace DocStore.Contract.Entities
{
    public class AppConfigurations
    {
        public static ConnectionString ConnectionStrings { get; set; }
    }

    public class ConnectionString
    {
        public string DefaultConnection { get; set; }
    }
}
