namespace NotifierApi.DataProvider.Options
{
    public sealed record SqlServerOptions(string EncryptedConnectionString)
    {
        public const string SqlServer = nameof(SqlServer);
        public const string Key = "nSW3QGLpp4i529CQ";

        public SqlServerOptions()
            : this(string.Empty)
        { }
    }
}