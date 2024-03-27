using Microsoft.EntityFrameworkCore;
using WebCheckerAPI.BackgroundTasks;
using WebCheckerAPI.EntityFrameworkStuff;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddDbContext<RequestDbContext>(options =>
{
	options.UseNpgsql(builder.Configuration.GetConnectionString("RequestStoreManagment"));
});

builder.Services.AddCors(options =>
{
	options.AddPolicy(name: MyAllowSpecificOrigins, policy =>
	{
		policy.WithOrigins("http://localhost:8080").AllowAnyMethod()
		.AllowAnyHeader();
	});
});
builder.Services.AddHttpClient();

builder.Services.AddSingleton<IHostedService, CommandService>();

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

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
