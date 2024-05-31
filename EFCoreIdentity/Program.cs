using EFCoreIdentity.Context;
using EFCoreIdentity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});
builder.Services.AddIdentity<AppUser, AppRole>(op =>
{
    op.Password.RequireNonAlphanumeric = false;
    op.Password.RequireDigit = false;
    op.Password.RequireLowercase = false;
    op.Password.RequireUppercase = false;
    op.Password.RequiredLength = 1;

    op.User.RequireUniqueEmail = true;
    op.SignIn.RequireConfirmedEmail = true;
    op.Lockout.MaxFailedAccessAttempts = 3;
    op.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(30);

}).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
