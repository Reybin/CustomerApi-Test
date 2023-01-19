using DATA.Context;
using Microsoft.EntityFrameworkCore;
using SERVICE.CustomerService;
using SERVICE.CustomerService.Contracts;
using SERVICE.NoteService;
using SERVICE.NoteService.Contract;

var builder = WebApplication.CreateBuilder(args);


//Add DbContext
builder.Services.AddDbContext<ApiContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Context")));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//Add Service
builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddTransient<INoteService, NoteService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
