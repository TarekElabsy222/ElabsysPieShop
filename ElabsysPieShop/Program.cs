using ElabsysPieShop.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace ElabsysPieShop
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			var connectionString = builder.Configuration.GetConnectionString
				("DefaultConnction") ?? throw new InvalidOperationException("Connection string 'ElabsysPieShopDbContextConnection' not found.");
            builder.Services.AddDbContext<ElabsysPieShopDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
			builder.Services.AddScoped<IPieRepository, PieRepository>();
			builder.Services.AddScoped<IOrderRepositiory, OrderRepositiory>();

			builder.Services.AddScoped<IShoppingCart, ShoppingCartRepository>(sp=>ShoppingCartRepository.GetCart(sp));
			builder.Services.AddSession();
			builder.Services.AddHttpContextAccessor();
			

			builder.Services.AddDefaultIdentity<IdentityUser>()
				.AddEntityFrameworkStores<ElabsysPieShopDbContext>();


			// Add services to the container.
			builder.Services.AddControllersWithViews()
				.AddJsonOptions(options =>
				{
					options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
				});
            builder.Services.AddRazorPages();
            builder.Services.AddConnections();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseSession();
			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();
			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();
            DbInitalizer.Seed(app);

			app.Run();
		}
	}
}
