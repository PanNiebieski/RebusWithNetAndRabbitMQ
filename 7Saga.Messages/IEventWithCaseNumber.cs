namespace _7Saga.Messages
{
    public interface IEventWithCaseNumber
    {
        string CaseNumber { get; }
    }

    public class PayoutMethodSelected : IEventWithCaseNumber
    {
        public string CaseNumber { get; }

        public PayoutMethodSelected(string caseNumber)
        {
            CaseNumber = caseNumber;
        }
    }

    public class PayoutNotReady : IEventWithCaseNumber
    {
        public string CaseNumber { get; }

        public PayoutNotReady(string caseNumber)
        {
            CaseNumber = caseNumber;
        }
    }

    public class AmountsCalculated : IEventWithCaseNumber
    {
        public string CaseNumber { get; }

        public AmountsCalculated(string caseNumber)
        {
            CaseNumber = caseNumber;
        }
    }

    public class PayoutReady : IEventWithCaseNumber
    {
        public string CaseNumber { get; }

        public PayoutReady(string caseNumber)
        {
            CaseNumber = caseNumber;
        }
    }

    public class TaxesCalculated : IEventWithCaseNumber
    {
        public string CaseNumber { get; }

        public TaxesCalculated(string caseNumber)
        {
            CaseNumber = caseNumber;
        }
    }

}