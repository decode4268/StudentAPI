namespace StudentAPI.Helper
{
    public class ConfigService
    {
        private readonly IConfiguration _config;
        public ConfigService(IConfiguration configuration)
        {
            _config = configuration;
        }

        public string GetAppName()
        {
            return _config["ApplicationSetting:AppName"];
        }
    }
}
