using Microsoft.EntityFrameworkCore;
using RelationShip.Config;
using RelationShip.Data;
using RelationShip.Service;
using RelationShip.Signalr;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add database
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//Add Mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Add Scoped
builder.Services.AddRegisterRepository();

//jwt
builder.Services.AddAuthorizationBearer(builder.Configuration);

//Signalr
builder.Services.AddSignalR();

//session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thiết lập thời gian timeout của session
    options.Cookie.HttpOnly = true; // Chỉ cho phép HTTP access, không cho JavaScript access
    options.Cookie.IsEssential = true; // Đặt cookie là bắt buộc (essential) cho việc session hoạt động
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapHub<ChatHub>("/ChatHub");

app.Run();
