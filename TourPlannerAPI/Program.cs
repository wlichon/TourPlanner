global using TourPlannerAPI.Data;
global using Npgsql.EntityFrameworkCore.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using JsonTools;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


/*using (SqlConnection conn = new SqlConnection(builder.Configuration.GetConnectionString("TourPlannerDB")))
{
    conn.Open(); // throws if invalid
}
*/


builder.Services.AddControllers();


builder.Services.AddMvc().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.Converters.Add(new TimespanConverter());

});


builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("TourPlannerDB"));
});


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

app.UseAuthorization();

app.MapControllers();

app.Run();
