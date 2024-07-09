using OutOfOfficeApp;
using static System.Formats.Asn1.AsnWriter;
using OutOfOfficeApp;
using OutOfOfficeApp.Entities;
using OutOfOfficeApp.Services;
using AutoMapper;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<OOODbContext>();
builder.Services.AddScoped<Seeder>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IApprovalRequestService, ApprovalRequestService>();
builder.Services.AddScoped<ILeaveRequestService, LeaveRequestService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);



var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    
}

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<Seeder>();
seeder.Seed();



/*app.UseHttpsRedirection();*/

app.UseSwagger();
app.MapSwagger();
app.UseSwaggerUI();

/*app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "OutOfOfficeApp");
});*/

app.UseStaticFiles();

app.UseRouting();

/*app.UseAuthorization();*/
app.MapControllers();

app.MapRazorPages();

app.Run();
