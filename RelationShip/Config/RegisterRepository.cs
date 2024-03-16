using RelationShip.Interfaces;
using RelationShip.Repository;

namespace RelationShip.Service
{
    public static class RegisterRepository
    {
        public static void AddRegisterRepository(this IServiceCollection service)
        {
            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<IPostRepository, PostRepository>();
            service.AddScoped<ICaregoryReposirory, CategoryRepository>();
            service.AddScoped<IAuthReposirory, AuthRepository>();
            service.AddScoped<IMailRepository, MailRepository>();
            service.AddScoped<Bcypt>();
            service.AddHttpContextAccessor();
        }
    }
}
