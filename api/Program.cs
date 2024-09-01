
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
using Api.ErrosValidacao;

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
    var validacao = new ErrosValidacao();
    if(string.IsNullOrEmpty(veiculoDTO.Nome)){
        validacao.Mensagens.Add("O nome não pode ser vazio");
    }
    if(string.IsNullOrEmpty(veiculoDTO.Marca)){
        validacao.Mensagens.Add("A marca não pode ser vazio");
    }
    if(veiculoDTO.Ano <1950){
        validacao.Mensagens.Add("Veiculo fora dos padrões");
    }

    
    if(validacao.Mensagens.Count > 0){
        return Results.BadRequest(validacao);
    }


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



app.MapGet("/Veiculos/{id}", ([FromRoute] int id, IVeiculoServico veiculoServico) =>
{
    var veiculo = veiculoServico.BuscaPorId(id);
    if (veiculo == null) return Results.NotFound();
    return Results.Ok(veiculo);
}).WithTags("Veiculos");


app.MapPut("/Veiculos/{id}", ([FromRoute] int id,VeiculoDTO veiculoDTO, IVeiculoServico veiculoServico) =>
{
    var veiculo = veiculoServico.BuscaPorId(id);
    if (veiculo == null) return Results.NotFound();
    veiculo.Nome = veiculoDTO.Nome;
    veiculo.Marca = veiculoDTO.Marca;
    veiculo.Ano = veiculoDTO.Ano;

    veiculoServico.Atualizar(veiculo);
    return Results.Ok(veiculo);

}).WithTags("Veiculos");


app.MapDelete("/Veiculos/{id}", ([FromRoute] int id, IVeiculoServico veiculoServico) =>
{
    var veiculo = veiculoServico.BuscaPorId(id);
    if (veiculo == null) return Results.NotFound();
   

    veiculoServico.Apagar(veiculo);
        
    return Results.NoContent();
    
}).WithTags("Veiculos");


app.UseSwagger();
app.UseSwaggerUI();
app.Run();
