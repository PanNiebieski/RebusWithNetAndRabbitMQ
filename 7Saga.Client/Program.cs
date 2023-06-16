﻿// configure Serilog to only log warnings
using _7Saga.Common;
using _7Saga.Messages;
using Rebus.Config;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Debug()
    .MinimumLevel.Warning()
    .CreateLogger();

using var bus = Configure.OneWayClient()
    .ConfigureEndpoint(EndpointRole.Client)
    .Start();

while (true)
{
    Console.Write("Case number > ");
    var caseNumber = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(caseNumber))
    {
        Console.WriteLine("Quitting...");
        return;
    }

    Console.WriteLine(@"Which event to publish?
    a) AmountsCalculated
    t) TaxesCalculated
    p) PayoutMethodSelected
    ");
    var key = ReadKey("atp");

    switch (key)
    {
        case 'a':
            bus.Publish(new AmountsCalculated(caseNumber)).Wait();
            break;
        case 't':
            bus.Publish(new TaxesCalculated(caseNumber)).Wait();
            break;
        case 'p':
            bus.Publish(new PayoutMethodSelected(caseNumber)).Wait();
            break;
    }

}

static char ReadKey(IEnumerable<char> allowedCharacters)
{
    var chars = new HashSet<char>(allowedCharacters.Select(char.ToLowerInvariant));

    while (true)
    {
        var key = char.ToLowerInvariant(Console.ReadKey(true).KeyChar);
        if (!chars.Contains(key)) continue;
        return key;
    }
}