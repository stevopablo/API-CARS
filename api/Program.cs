var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/test", () => "vamoaxcorxaaaaaaaaaaaaaaa");
app.MapPost("/login",(api.DTOs.LoginDTO LoginDTO)=>{
    if(LoginDTO.Email == "adm@teste.com" && LoginDTO.Senha == "123456"){
        return Results.Ok("Login com sucesso");
    }else{
        return Results.Unauthorized();
    }
});

app.Run();
