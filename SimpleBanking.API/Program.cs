using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SimpleBanking.API.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<Dictionary<string, Account>>();
builder.Services.AddSingleton<IDepositService, DepositService>();
builder.Services.AddSingleton<IWithdrawService, WithdrawService>();
builder.Services.AddSingleton<ITransferService, TransferService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
