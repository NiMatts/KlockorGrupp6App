using KlockorGrupp6App.Infrastructure;

namespace KlockorGrupp6App.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();

            builder.Services.AddInfrastructure(builder.Configuration);

            var app = builder.Build();

            //app.UseAuthentication(); //test morgon
            app.UseAuthorization();

            app.MapControllers();
            app.Run();
        }
    }
}
