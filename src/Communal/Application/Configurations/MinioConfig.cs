namespace Communal.Application.Configurations
{
    public class MinioConfig
    {
        public string Connection { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public string RootBucketName { get; set; }
    }
}