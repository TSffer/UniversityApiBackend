//1. Usings to work with EntityFrameWork
using Microsoft.EntityFrameworkCore;
using universityApiBackend.DataAccess;
using universityApiBackend.Services;

var builder = WebApplication.CreateBuilder(args);

//2. Conection with SQL Server Express
const string CONNECTION_NAME = "UniversityDB";
var connectionString = builder.Configuration.GetConnectionString(CONNECTION_NAME);

//3. Add Context to Services of builder
builder.Services.AddDbContext<UniversityDBContext>(options => options.UseSqlServer(connectionString));

//7. Add Service of JWT Autorization
//builder.Services.AddJwtTokenServices(builder.Configuration);

// Add services to the container.
builder.Services.AddControllers();

//4. Add Custom Services (folder Services)
builder.Services.AddScoped<IStudentsService, StudentsService>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 5. CORS Configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
    });
});

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

// 6. Tell app to use CORS
app.UseCors("CorsPolicy");

app.Run();
