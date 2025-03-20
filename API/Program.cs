
using API.Extensions;



var builder = WebApplication.CreateBuilder(args);


builder.Services.AddApplicationService(builder.Configuration);

//adds validation service to the code for jwt
builder.Services.AddIdentityService(builder.Configuration); 
var app = builder.Build();

// Configure the HTTP request pipeline.
//http://localhost:4200/
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200/","http://localhost:4200","https://localhost:4200"));

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
