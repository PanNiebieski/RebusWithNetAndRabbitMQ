var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

int i = 2;


app.MapPost("/check", (Request request) =>
{
    Random r = new Random();
    var ra = r.Next(0, 200);
    i++;

    if (i % 3 == 0)
    {
        var response = new Response(true);
        return response;
    }
    else if (i % 2 == 0)
    {
        var response = new Response(false);
        return response;
    }
    else
    {
        throw new Exception("NOT WORKING MUMUMU");
    }


})
.WithName("check")
.WithOpenApi();


app.Run();



public class Request
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Pesel { get; set; }
}

public class Response
{
    public Response(bool confirmed)
    {
        Confirmed = confirmed;
    }

    public bool Confirmed { get; set; }
}