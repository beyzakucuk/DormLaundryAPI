using Contracts;
using Microsoft.EntityFrameworkCore;
using Entities;
using Repositories;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEntityFrameworkNpgsql().AddDbContext<RepositoryContext>(
    o => o.UseNpgsql(builder.Configuration.GetConnectionString("postgres"),
    b => b.MigrationsAssembly("DormLaundryAPI")));
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(y => y.AddPolicy("my-policy", y => y.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod()));
builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
builder.Services.AddScoped<AdminService>();
builder.Services.AddScoped<StudentService>();
builder.Services.AddScoped<MachineService>();
builder.Services.AddScoped<TurnService>();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("my-policy");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
