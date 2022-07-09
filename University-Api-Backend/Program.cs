//1.Usings to work with EntityFramework
using Microsoft.EntityFrameworkCore;
using University_Api_Backend.DataAccess;
using University_Api_Backend.Services;

var builder = WebApplication.CreateBuilder(args);

// 2. Connection with SQL SERVER Express
const string CONNECTIONNAME = "UniversityDB";
var connectionString = builder.Configuration.GetConnectionString(CONNECTIONNAME);


// 3. Add context to Services of builder
builder.Services.AddDbContext<UniversityDBContext>(options => options.UseSqlServer(connectionString));


// 7. Add Service of JWT  Autorization

//TODO:builder.Services.AddJwtTokenServices(builder.Configuration);


//TODO: Connection with SQL SERVER EXPRESS
// Add services to the container.



builder.Services.AddControllers();

// 4. Add custom services (folder services)

builder.Services.AddScoped<IStudentService,StudentService>();  // inyectamos para poder user en los controladores
//TODO: add the rest of the services
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICourseService, CourseService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle



builder.Services.AddEndpointsApiExplorer();

// 8. TODO: Config swagger to take care of Autorization of JWT
builder.Services.AddSwaggerGen();


// CORS configuration

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "corsPolicy", builder =>
    {
        builder.AllowAnyOrigin(); // cualquier aplicación puede hacer peticiones
        builder.AllowAnyMethod(); // se puede usar put,get,...
        builder.AllowAnyHeader(); // que nos pueda enviar cualquier cabecera

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

app.UseCors("corsPolicy");

app.Run();
