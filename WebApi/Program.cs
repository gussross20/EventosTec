using Business;
using Business.Settings;
using WebApi;

var builder = WebApplication.CreateBuilder(args);

// Habilita CORS para aceptar TODO
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTodo", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


// Add services to the container.
//GRR: IOC Repository
builder.Services.AddRepositoryViProduce(builder.Configuration.GetConnectionString("WebAppDB") ?? "");

// GRR: IOC Business
builder.Services.AddBusiness();

builder.Services.AddControllers();
//GRR: IOC Email settings
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection(nameof(EmailSettings)));
builder.Services.AddTransient<EmailService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("PermitirTodo");
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
