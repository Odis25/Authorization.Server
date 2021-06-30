namespace IdentityServer.Data
{
    public class DbInitializer
    {
        public static void Initialize(AuthDbContext dbContext)
        {
            dbContext.Database.EnsureCreated();
        }
    }
}
