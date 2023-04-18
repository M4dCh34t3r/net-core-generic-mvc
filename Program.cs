using Microsoft.EntityFrameworkCore;
using GenericCrudMVC.Models;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Setting up de DB
try {
    Console.WriteLine("REACHED");
    using (SqlConnection connection = new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection"))) {
        connection.Open();
        Console.WriteLine("OPENED");
        String query = "IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='items' and xtype='U')" +
            "CREATE TABLE items (" +
            "ID INTEGER NOT NULL IDENTITY," +
            "Name VARCHAR(50) NOT NULL," +
            "Description VARCHAR(100) NOT NULL" +
            ");";
        using (SqlCommand command = new SqlCommand(query, connection)) { command.ExecuteNonQuery(); }
    }
} catch (Exception ex) {
    Console.WriteLine(ex.Message);
}
    // Add services to the container.
    builder.Services.AddControllersWithViews(); builder.Services.AddRazorPages(); 
builder.Services.AddDbContext<ItemDbContext>(options => {
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
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
