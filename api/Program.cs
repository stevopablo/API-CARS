
using Api.AdmServicos;
using Api.IAdmServico;
using Api.Db.context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.HomeView;
using Api.IVeiculoServico;
using Api.Entidades;
using api.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Register DbContext
builder.Services.AddDbContext<MyDbContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("mysql"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("mysql")));
});

// Register other services
builder.Services.AddScoped<IAdmServico, AdmServicos>();
builder.Services.AddScoped<IVeiculoServico, VeiculoServico>();

var app = builder.Build();

app.MapGet("/", () => Results.Json(new HomeView())).WithTags("Home");

app.MapGet("/administradores/test", () => "aaaaaaaaaaaaaaaaaaa");

app.MapPost("/administradores/login", ([FromBody] api.DTOs.LoginDTO loginDTO, IAdmServico admServico) =>
{
    if (admServico.Login(loginDTO) != null)
    {
        return Results.Ok("Login com sucesso");
    }
    else
    {
        return Results.Unauthorized();
    }
}).WithTags("Administradores");



// add
app.MapPost("/Veiculos", ([FromBody] api.DTOs.VeiculoDTO veiculoDTO, IVeiculoServico veiculoServico) =>
{
    var veiculo = new Veiculo{
        Nome = veiculoDTO.Nome,
        Marca = veiculoDTO.Marca,
        Ano = veiculoDTO.Ano}; 

    veiculoServico.Incluir(veiculo);
        return Results.Created($"/Veiculo/{veiculo.Id}", veiculo);

}).WithTags("Veiculos");

// find

app.MapGet("/Veiculos", ([FromQuery] int pagina, IVeiculoServico veiculoServico ) =>
{
    var veiculos = veiculoServico.Todos(pagina);

    return Results.Ok(veiculos);

}).WithTags("Veiculos");



app.UseSwagger();
app.UseSwaggerUI();
app.Run();
