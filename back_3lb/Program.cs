using System.Net.Mime;
using back_3lb.Data;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

string directory = "C:\\Users\\Саня\\Desktop\\ВУЗ\\3курс\\БЭК\\sample_files";


app.MapGet("/HTML", () =>
{
    string html = "<h1><b>Какой то HTML</b></h1><p>Вот у нас тег p типо параграф.</p><br><span>И спенчик тут вот.</span>";
    //return IResult()
    return Results.Content(html, "text/html");
});

app.MapGet("/text", () =>
{
    string plain = "Просто текст круто!";
    return Results.Content(plain, "text/plain");
});

app.MapGet("/JSON", () =>
{
    var json = new
    {
        text = "ну типо текст",
        qwe = 52
    };
    return Results.Json(json);
});

app.MapGet("/XML", () =>
{
    string fileName = "sample.xml";
    var fileBytes = File.ReadAllBytes(Path.Combine(directory, fileName));
    return Results.File(fileBytes, MediaTypeNames.Application.Xml, fileName);
});


app.MapGet("/CSV", () =>
{
    string fileName = "sample.csv";
    var fileBytes = File.ReadAllBytes(Path.Combine(directory, fileName));
    return Results.File(fileBytes, MediaTypeNames.Text.Csv, fileName);
});


app.MapGet("/BIN", (HttpContext context) =>
{
    string fileName = "sample.bin";
    var stream = File.OpenRead(Path.Combine(directory, fileName));
    context.Response.RegisterForDispose(stream);
    //var fileBytes = File.ReadAllBytes(Path.Combine(directory, fileName));
    return Results.File(stream, MediaTypeNames.Application.Octet, fileName);
});

app.MapGet("/IMG", () =>
{
    string fileName = "sample.jpg";
    var fileBytes = File.ReadAllBytes(Path.Combine(directory, fileName));
    return Results.File(fileBytes, MediaTypeNames.Image.Jpeg, fileName);
});

app.MapGet("/PDF", () =>
{
    string fileName = "sample.pdf";
    var fileBytes = File.ReadAllBytes(Path.Combine(directory, fileName));
    return Results.File(fileBytes, MediaTypeNames.Application.Pdf, fileName);
});


app.MapGet("/", () =>
{
    return Results.LocalRedirect("/1");
});

app.MapGet("/1", () =>
{
    return Results.Ok("Вас редиректнуло \\_(-_-)_/");
});


// Реализация лб 5

app.MapControllers();


//app.MapGet("api/products", () =>
//{
//    List<Product> products = new List<Product>();
//    products.Add(new Product { Name = "КолаКола)", Description = "Очень вкусно и сахар много", Price = 100 });
//    products.Add(new Product { Name = "ПепсиПепси)", Description = "НЕ Очень вкусно и сахар много", Price = 200 });
//    products.Add(new Product { Name = "Тархун)", Description = "Ну че вам объяснять это тархун", Price = 10 }
//    );
//    return Results.Ok(products);
//});

//app.MapPost("api/products", ([FromBody] List<Product> products) =>
//{

//    return Results.Ok(new { message = "Данные успешно добавленны в базы данных", products });
//});

//app.MapGet("api/students", () =>
//{
//List<Student> students = new List<Student>();
//students.Add(new Student { Name = "Коля", Specialization = "ИСИТ" });
//students.Add(new Student { Name = "Петя", Specialization = "Дизайн" });
//students.Add(new Student { Name = "Саня", Specialization = "Веб разработка" }
//);
//return Results.Ok(students);
//});

//app.MapPost("api/students", ([FromBody] List<Student> students) =>
//{

//    return Results.Ok(new { message = "Данные успешно добавленны в базы данных", students });
//});

//app.MapGet("api/orders", () =>
//{
//    List<Order> orders = new List<Order>();
//    orders.Add(new Order { Student = new Student { Name = "Коля", Specialization = "ИСИТ" }, Product = new Product { Name = "КолаКола)", Description = "Очень вкусно и сахар много", Price = 100 } });
//    orders.Add(new Order { Student = new Student { Name = "Петя", Specialization = "Дизайн" }, Product = new Product { Name = "ПепсиПепси)", Description = "НЕ Очень вкусно и сахар много", Price = 200 } });
//    orders.Add(new Order { Student = new Student { Name = "Саня", Specialization = "Веб разработка" }, Product = new Product { Name = "Тархун)", Description = "Ну че вам объяснять это тархун", Price = 10 } }
//    );
//    return Results.Ok(orders);
//});

//app.MapPost("api/orders", ([FromBody] List<Order> orders) =>
//{

//    return Results.Ok(new { message = "Данные успешно добавленны в базы данных", orders });
//});



app.Run();

//public struct Order
//{
//    public Student Student { get; set; }
//    public Product Product { get; set; }
//}

//public struct Product
//{
//    public string Name { get; set; }
//    public string Description { get; set; }
//    public int Price { get; set; }
//}

//public struct Student
//{
//    public string Name { get; set; }
//    public string Specialization { get; set; }

//}
