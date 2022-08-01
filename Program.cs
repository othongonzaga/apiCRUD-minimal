using Microsoft.EntityFrameworkCore;
using WebMinimalApiOracle.Config;
using WebMinimalApiOracle.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var stringConexao = "User Id=SYSTEM;Password=1234;Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST =localhost)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=XE)))";
builder.Services.AddDbContext<Contexto>
    (options => options.UseOracle(stringConexao));

var app = builder.Build();
app.UseSwagger();

app.MapPost("AdicionarProduto", async (Produto produto, Contexto contexto) =>
{
    contexto.Produto.Add(produto);
    contexto.SaveChangesAsync();
});

app.MapPost("ExcluirProduto/{id}", async (int id, Contexto contexto) =>
{
    var produto = await contexto.Produto.FirstOrDefaultAsync(p => p.ID == id);
    if(produto != null)
    {
        contexto.Produto.Remove(produto);
        await contexto.SaveChangesAsync();
    }
    contexto.SaveChangesAsync();
});

app.MapGet("ListarProdutos", async (Contexto contexto) =>
{
    return await contexto.Produto.ToListAsync();
});

app.MapGet("ObterProduto/{id}", async (int id, Contexto contexto) =>
{
    return await contexto.Produto.FirstOrDefaultAsync(p => p.Id == id);
});


app.UseSwaggerUI();
app.Run();