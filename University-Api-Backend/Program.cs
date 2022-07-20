//1.Usings to work with EntityFramework
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using University_Api_Backend;
using University_Api_Backend.DataAccess;
using University_Api_Backend.Services;

var builder = WebApplication.CreateBuilder(args);

// 2. Connection with SQL SERVER Express
const string CONNECTIONNAME = "UniversityDB";
var connectionString = builder.Configuration.GetConnectionString(CONNECTIONNAME);


// 3. Add context to Services of builder
builder.Services.AddDbContext<UniversityDBContext>(options => options.UseSqlServer(connectionString));


// 7. Add Service of JWT  Autorization

builder.Services.AddJwtTokenServices(builder.Configuration);


//TODO: Connection with SQL SERVER EXPRESS
// Add services to the container.



builder.Services.AddControllers();

// 4. Add custom services (folder services)

builder.Services.AddScoped<IStudentService,StudentService>();  // inyectamos para poder user en los controladores
//TODO: add the rest of the services
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICourseService, CourseService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// 8.Add Authorization 
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("UserOnlyPolicy", policy => policy.RequireClaim("UserOnly","User 1"));

});

builder.Services.AddEndpointsApiExplorer();

// 9.  Config swagger to take care of Autorization of JWT
builder.Services.AddSwaggerGen(options =>
{
    // We define de security for Autorization
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization Header using Bearer Scheme"

    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                  {
                     Type = ReferenceType.SecurityScheme,
                     Id = "Bearer"
                  }
            },
            new string[]{}
        }
    });
});


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
