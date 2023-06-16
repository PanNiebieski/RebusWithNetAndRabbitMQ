using _7Saga.Messages;
using _7Saga.Saga.Messages;
using Rebus.Bus;
using Rebus.Handlers;
using Rebus.Pipeline;
using Rebus.Sagas;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace _7Saga.Saga;
public class PayoutSaga : Saga<PayoutSagaData>,
    IAmInitiatedBy<AmountsCalculated>,
    IAmInitiatedBy<TaxesCalculated>,
    IAmInitiatedBy<PayoutMethodSelected>,
    IHandleMessages<MyTimeOut>
{
    static readonly ILogger Logger = Log.ForContext<PayoutSaga>();

    readonly IBus _bus;

    public PayoutSaga(IBus bus)
    {
        _bus = bus;
    }

    protected override void CorrelateMessages(ICorrelationConfig<PayoutSagaData> config)
    {
        // events of interest
        config.Correlate<AmountsCalculated>(m => m.CaseNumber, d => d.CaseNumber);
        config.Correlate<TaxesCalculated>(m => m.CaseNumber, d => d.CaseNumber);
        config.Correlate<PayoutMethodSelected>(m => m.CaseNumber, d => d.CaseNumber);

        // internal verification message
        config.Correlate<MyTimeOut>(m => m.CaseNumber, d => d.CaseNumber);
    }

    public async Task Handle(AmountsCalculated message)
    {
        if (Data.AmountsCalculated) return;

        await StartARaceAgaistTime();

        Logger.Information("Setting {FieldName} to true for case {CaseNumber}", "AmountsCalculated", Data.CaseNumber);
        Data.AmountsCalculated = true;
        await CheckIfAllDone();
    }

    public async Task Handle(TaxesCalculated message)
    {
        if (Data.TaxesCalculated) return;

        await StartARaceAgaistTime();

        Logger.Information("Setting {FieldName} to true for case {CaseNumber}", "TaxesCalculated", Data.CaseNumber);
        Data.TaxesCalculated = true;
        await CheckIfAllDone();
    }

    public async Task Handle(PayoutMethodSelected message)
    {
        if (Data.PayoutMethodSelected) return;

        await StartARaceAgaistTime();

        Logger.Information("Setting {FieldName} to true for case {CaseNumber}", "PayoutMethodSelected", Data.CaseNumber);
        Data.PayoutMethodSelected = true;
        await CheckIfAllDone();
    }

    public async Task Handle(MyTimeOut message)
    {
        Logger.Warning("TIMEOUT -> The saga for case {CaseNumber} was not completed within {TimeoutSeconds} s timeout", Data.CaseNumber, 20);
        await _bus.Publish(new PayoutNotReady(Data.CaseNumber));
        MarkAsComplete();
    }

    async Task StartARaceAgaistTime()
    {
        if (!IsNew) return;

        Logger.Information("StartARaceAgaistTime -> Ordering wake-up call in {TimeoutSeconds} s for case {CaseNumber}", 20, Data.CaseNumber);

        await _bus.DeferLocal(TimeSpan.FromSeconds(20), new MyTimeOut(Data.CaseNumber));
    }

    async Task CheckIfAllDone()
    {
        if (!Data.IsDone) return;
        

        Logger.Information("COMPLETED -> Publishing ready event and marking saga for case {CaseNumber} as complete", Data.CaseNumber);

        await _bus.Publish(new PayoutReady(Data.CaseNumber));

        MarkAsComplete();
    }


}