using Microsoft.EntityFrameworkCore;
using WriteOffUley;
using WriteOffUley.Commands;
using WriteOffUley.Commands.AnaliticsWriteOffCommand;
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
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    
}, ServiceLifetime.Singleton);
builder.Services.AddSingleton<TelegramBotService>();
builder.Services.AddSingleton<ICommandExecutor, CommandExecutor>();
builder.Services.AddSingleton<IOperationService, OperationService>();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<BaseCommand, StartCommand>();
builder.Services.AddSingleton<BaseCommand, GetDeleteWriteOffCommand>();
builder.Services.AddSingleton<BaseCommand, SelectDeleteWriteOffCommand>();
builder.Services.AddSingleton<BaseCommand, ChoiceProductCommand>();
builder.Services.AddSingleton<BaseCommand, SelectCategoryCommand>();
builder.Services.AddSingleton<BaseCommand, SelectCountProductsCommand>();
builder.Services.AddSingleton<BaseCommand, OpenWriteOffDayCommand>();
builder.Services.AddSingleton<BaseCommand, SelectAnalyticsCommand>();
builder.Services.AddSingleton<IFinishedCommand, FinishOperationCommand>();
builder.Services.AddSingleton<IWriteOffRepository, WriteOffRepository>();
builder.Services.AddSingleton<IProductRepository, ProductRepository>();
builder.Services.AddSingleton<ICategoryRepository, CategoryRepository>();

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