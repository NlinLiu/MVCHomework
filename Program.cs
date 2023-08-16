using Microsoft.EntityFrameworkCore;
using MVCHomework6.Data;
using MVCHomework6.Data.Database;
using MVCHomework6.Service;

var builder = WebApplication.CreateBuilder(args);
//���d�Ҩϥ� EntityFramework inMemory �S�������Ʈw�����b�O���餺�]�����թMPOC�O�D�`�n�Ϊ��^
builder.Services.AddDbContext<BlogDbContext>(options => options.UseInMemoryDatabase("SkillTreeBlog"));
builder.Services.AddTransient(typeof(ArticleService));
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<BlogDbContext>();
        context.Database.EnsureCreated();
        SeedData.Initialize(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}
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
