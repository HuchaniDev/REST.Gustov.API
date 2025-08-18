using System.Text;
using Microsoft.IdentityModel.Tokens;
using Rest.Api.EndPoints.Menu;
using Rest.Api.Middleware;
using REST.Infrastructure.IoC.Di;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
  options.AddPolicy("CORSPolicy",
    b => b
      .AllowAnyMethod()
      .AllowAnyHeader()
      .AllowCredentials()
      .SetIsOriginAllowed((hosts) => true));
});

// 🔹 REGISTRO DE SERVICIOS PERSONALIZADOS
builder.Services
  .RegisterDataBase(builder.Configuration)
  .RegisterRepositories()
  .RegisterServices();

// 🔹 CONFIGURACIÓN DE AUTENTICACIÓN JWT
// var jwtSettings = builder.Configuration.GetSection("JwtSettings");
// var key = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]);

// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//   .AddJwtBearer(options =>
//   {
//     options.TokenValidationParameters = new TokenValidationParameters
//     {
//       ValidateIssuer = true,
//       ValidateAudience = true,
//       ValidateLifetime = true,
//       ValidateIssuerSigningKey = true,
//       ValidIssuer = jwtSettings["Issuer"],
//       ValidAudience = jwtSettings["Audience"],
//       IssuerSigningKey = new SymmetricSecurityKey(key)
//     };
//   });

//builder.Services.AddAuthorization();

var app = builder.Build();

app.UseCors("CORSPolicy");
app.UseMiddleware<MiddlewareException>();
app.UseMiddleware<NotFoundMiddleware>();
//app.UseMiddleware<AuthorizationMiddleware>();


app.UseSwagger();
app.UseSwaggerUI(c =>
{
  c.SwaggerEndpoint("/swagger/v1/swagger.json", "APIs Rest-Gustov V.1.0");
  c.RoutePrefix = "swagger";
  c.EnableFilter();
});

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}
// app.UseAuthentication();
// app.UseAuthorization();

// 🔹 REGISTRO DE ENDPOINTS
app.MapCategoryEndpoints();

app.Run();
