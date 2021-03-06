using Bazaro.Web;
using Bazaro.Web.Areas.Identity;
using Bazaro.Web.Models;
using Bazaro.Web.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// read pw for db from .env file or env variable(OS) and add it to the db connectionstring
var root = Directory.GetCurrentDirectory();
var dotenv = Path.Combine(root, ".env");
bool file_exists = DotEnv.LoadFromFile(dotenv);
string pw;
if (!file_exists)
{
    pw = Environment.GetEnvironmentVariable("DB_PW");
    if (pw == null)
    {
        throw new FileLoadException("Please create the .env File!");
    }
}
else
{
    pw = DotEnv.FromEnvVariable("DB_PW");
    if (pw == null)
    {
        throw new Exception("Please set the ENV variable!");
    }
}

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
connectionString = connectionString.Replace("{db_pw}", pw);
builder.Services.AddDbContext<BazaroContext>(options =>
    options.UseNpgsql(connectionString)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors(), ServiceLifetime.Transient);

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<BazaroContext>();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<User>>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<EntryService>();
builder.Services.AddScoped<FolderService>();
builder.Services.AddScoped<ItemService>();
builder.Services.AddScoped<EntryRelationService>();
builder.Services.AddScoped<StatisticService>();
builder.Services.AddScoped<CalendarService>();
builder.Services.AddScoped<ContentTypeSevice>();

builder.Services.AddAntDesign();
builder.Services.AddBlazorContextMenu();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();