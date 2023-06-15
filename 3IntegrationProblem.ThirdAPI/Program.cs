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


app.MapPost("/check", (Request request) =>
{
    var response = new Response(true);
    return response;
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