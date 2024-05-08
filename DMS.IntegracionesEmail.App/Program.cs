
using DMS.IntegracionesEmail.Core.Service;
using DMS.IntegracionesEmail.Infrastructure.Blls;
using DMS.IntegracionesEmail.Infrastructure.Repositories;
using DMS.IntegracionesEmail.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddTransient<ICorreoService, CorreoService>();
builder.Services.AddTransient<ICursoRepository, CursoRepository>();
builder.Services.AddTransient<ICursoBLL, CursoBLL>();
var host = builder.Build();
var app = host.Services.GetRequiredService<ICursoBLL>();
app.Ejecutar();