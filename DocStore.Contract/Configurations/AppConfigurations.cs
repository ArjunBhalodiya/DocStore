namespace DocStore.Contract.Configurations
{
    public class AppConfigurations
    {
        public ConnectionString ConnectionStrings { get; set; }
        public EmailConfigurations EmailConfigurations { get; set; }
    }

    public class ConnectionString
    {
        public string DefaultConnection { get; set; }
    }
}