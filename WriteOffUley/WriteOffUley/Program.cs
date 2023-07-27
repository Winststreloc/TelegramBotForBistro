using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using WriteOffUley;
using WriteOffUley.Commands;
using WriteOffUley.Commands.AnaliticsWriteOffCommand;
using WriteOffUley.Commands.CreateNewProductForWriteOff;
using WriteOffUley.Commands.DeleteWriteOffCommand;
using WriteOffUley.Commands.OpenWriteOffDay;
using WriteOffUley.Interfaces;
using WriteOffUley.Repository;
using WriteOffUley.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnectionPSSQL"));
    
}, ServiceLifetime.Singleton); 
builder.Services.AddSingleton<TelegramBotService>();
builder.Services.AddSingleton<ICommandExecutor, CommandExecutor>();

builder.Services.AddSingleton<BaseCommand, StartCommand>();
builder.Services.AddSingleton<BaseCommand, GetDeleteWriteOffCommand>();
builder.Services.AddSingleton<BaseCommand, SelectDeleteWriteOffCommand>();
builder.Services.AddSingleton<BaseCommand, SelectCategoryCommand>();
builder.Services.AddSingleton<BaseCommand, SelectProductCommand>();
builder.Services.AddSingleton<BaseCommand, SelectCountProductsCommand>();
builder.Services.AddSingleton<BaseCommand, OpenWriteOffDayCommand>();
builder.Services.AddSingleton<BaseCommand, AnalyticsCommand>();
builder.Services.AddSingleton<BaseCommand, SelectAnalyticsCommand>();
builder.Services.AddSingleton<BaseCommand, CreateNewProduct>();
builder.Services.AddSingleton<IFinishedOperationCommand, FinishOperationCommand>();

builder.Services.AddSingleton<IAnalyticsService, AnalyticsService>();
builder.Services.AddSingleton<IProductRepository, ProductRepository>();
builder.Services.AddSingleton<ICategoryRepository, CategoryRepository>();
builder.Services.AddSingleton<IKeyboardService, KeyboardService>();
builder.Services.AddSingleton<IOperationRepository, OperationRepository>();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IWriteOffService, WriteOffService>();
builder.Services.AddSingleton<IWriteOffRepository, WriteOffRepository>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.Services.GetRequiredService<TelegramBotService>().GetBot().Wait();
}

app.UseRouting();


//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();