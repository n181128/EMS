using APIProject.DataAccess.Interfaces;
using APIProject.DataAccess.Models;
using APIProject.DataAccess;
using APIProject.Business;
using Microsoft.EntityFrameworkCore;
using APIProject.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SqldbContext>(Options => Options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection")));
//builder.Services.AddMvc().AddNewtonsoftJson(options => { options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; });

builder.Services.AddTransient<IEmployeeDataAccess, EmployeeDataAccess>();
builder.Services.AddTransient<IEmployeeBusiness, EmployeeBusiness>();
builder.Services.AddTransient<IDepartmentDataAccess, DepartmentDataAccess>();
builder.Services.AddTransient<IDepartmentBusiness, DepartmentBusiness>();


builder.Services.AddAutoMapper(typeof(APIProject.Utilities.AutoMapper));

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
builder.Services.AddControllers();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(policy=>policy.WithOrigins("http://localhost:7168", "https://localhost:7168")
.AllowAnyMethod().WithHeaders(HeaderNames.ContentType));
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
