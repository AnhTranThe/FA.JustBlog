using FA.JustBlog.Data;
using FA.JustBlog.Entities;
using FA.JustBlog.Interface;
using FA.JustBlog.Repository;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FA.JustBlog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            _ = builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString,
                options => options.EnableRetryOnFailure()));


            _ = builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            });

            _ = builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            _ = builder.Services.AddRazorPages();
            _ = builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
            _ = builder.Services.AddIdentity<AppUser, AppRole>()
             .AddEntityFrameworkStores<ApplicationDbContext>()
             .AddDefaultTokenProviders();
            _ = builder.Services.Configure<RouteOptions>(options =>
            {
                options.AppendTrailingSlash = false; // Thêm dấu / vào cuối URL
                options.LowercaseUrls = true; // url chữ thường
                options.LowercaseQueryStrings = false; // không bắt query trong url phải in thường
            });

            _ = builder.Services.AddTransient<IPostRepository, PostRepository>();
            _ = builder.Services.AddTransient<ITagRepository, TagRepository>();
            _ = builder.Services.AddTransient<IUserRepository, UserRepository>();
            _ = builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
            _ = builder.Services.AddScoped<ILoggingRepository, LoggingRepository>();

            _ = builder.Services.AddAuthentication();
            // Truy cập IdentityOptions
            _ = builder.Services.Configure<IdentityOptions>(options =>
            {
                // Thiết lập về Password
                options.Password.RequireDigit = false; // Không bắt phải có số
                options.Password.RequireLowercase = false; // Không bắt phải có chữ thường
                options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
                options.Password.RequireUppercase = false; // Không bắt buộc chữ in
                options.Password.RequiredLength = 3; // Số ký tự tối thiểu của password
                options.Password.RequiredUniqueChars = 1; // Số ký tự riêng biệt

                // Cấu hình Lockout - khóa user
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Khóa 5 phút
                options.Lockout.MaxFailedAccessAttempts = 5; // Thất bại 5 lần thì khóa
                options.Lockout.AllowedForNewUsers = true;

                // Cấu hình về User.
                options.User.AllowedUserNameCharacters = // các ký tự đặt tên user
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;  // Email là duy nhất

                // Cấu hình đăng nhập.
                options.SignIn.RequireConfirmedEmail = false;            // Cấu hình xác thực địa chỉ email (email phải tồn tại)
                options.SignIn.RequireConfirmedPhoneNumber = false;     // Xác thực số điện thoại

            });
            _ = builder.Services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = $"/User/AccessDenied";
            });

            _ = builder.Services.Configure<SecurityStampValidatorOptions>(options =>
            {
                // Trên 30 giây truy cập lại sẽ nạp lại thông tin User (Role)
                // SecurityStamp trong bảng User đổi -> nạp lại thông tinn Security
                options.ValidationInterval = TimeSpan.FromSeconds(30);
            });

            _ = builder.Services.AddAutoMapper(typeof(Program));

            _ = XmlConfigurator.Configure(new FileInfo("log4net.config"));
            _ = builder.Services.AddSingleton(LogManager.GetLogger(typeof(Program)));



            WebApplication app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                using (IServiceScope scope = app.Services.CreateScope())
                {
                    IServiceProvider services = scope.ServiceProvider;
                    SeedData.Initialize(services);

                }

                _ = app.UseMigrationsEndPoint();
            }
            else
            {
                _ = app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                _ = app.UseHsts();
            }

            _ = app.UseHttpsRedirection();
            _ = app.UseStaticFiles();
            _ = app.UseCors("CorsPolicy");
            _ = app.UseRouting();
            _ = app.UseAuthentication();
            _ = app.UseAuthorization();

            _ = app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            _ = app.MapRazorPages();

            app.Run();
        }
    }
}
