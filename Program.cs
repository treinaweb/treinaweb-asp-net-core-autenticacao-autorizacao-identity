using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Models.Contexts;
using WebApplication4.Validators.Medico;
using WebApplication4.ViewModels.Medico;
using WebApplication4.Validators.Paciente;
using WebApplication4.ViewModels.Paciente;
using WebApplication4.Validators.Consulta;
using WebApplication4.ViewModels.Consulta;
using WebApplication4.Validators.MonitoramentoPaciente;
using WebApplication4.ViewModels.MonitoramentoPaciente;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
builder.Services.AddDbContext<SisMedContext>();
builder.Services.AddScoped<IValidator<AdicionarMedicoViewModel>, AdicionarMedicoValidator>();
builder.Services.AddScoped<IValidator<EditarMedicoViewModel>, EditarMedicoValidator>();
builder.Services.AddScoped<IValidator<AdicionarPacienteViewModel>, AdicionarPacienteValidator>();
builder.Services.AddScoped<IValidator<EditarPacienteViewModel>, EditarPacienteValidator>();
builder.Services.AddScoped<IValidator<AdicionarConsultaViewModel>, AdicionarConsultaValidator>();
builder.Services.AddScoped<IValidator<EditarConsultaViewModel>, EditarConsultaValidator>();
builder.Services.AddScoped<IValidator<AdicionarMonitoramentoViewModel>, AdicionarMonitoramentoPacienteValidator>();
builder.Services.AddScoped<IValidator<EditarMonitoramentoViewModel>, EditarMonitoramentoPacienteValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
