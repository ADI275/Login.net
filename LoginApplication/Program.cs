using LoginApplication.Model;
using LoginApplication.Validators;
using LoginApplication.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// builder.Configuration.AddJsonFile("appsettings.json");

builder.Services.AddServerSideBlazor();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AuthConnectionString")));
builder.Services.AddDbContext<StockDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("StockConnectionString")));
builder.Services.AddDbContext<BranchDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("BranchConnectionString")));
builder.Services.AddDbContext<RecordDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("RecordConnectionString")));
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<AuthDbContext>().AddRoleManager<RoleManager<IdentityRole>>().AddRoles<IdentityRole>();
/*builder.Services.AddAuthorization(options => options.AddPolicy("Admin", policy => policy.RequireClaim("Admin"));*/
builder.Services.ConfigureApplicationCookie(config =>
{
    config.LoginPath = "/Login";
});

var app = builder.Build();

/*app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "EditPrice/{name}"
    );
});*/
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();
app.MapBlazorHub();

app.Run();
