// See https://aka.ms/new-console-template for more information
using Rebus.Sagas;

public class PayoutSagaData : SagaData
{
    public string CaseNumber { get; set; }

    public bool AmountsCalculated { get; set; }
    public bool TaxesCalculated { get; set; }
    public bool PayoutMethodSelected { get; set; }

    public bool IsDone => AmountsCalculated
                          && TaxesCalculated
                          && PayoutMethodSelected;
}
