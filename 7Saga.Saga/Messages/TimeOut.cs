using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7Saga.Saga.Messages
{
    public class TimeOut
    {
        public string CaseNumber { get; }

        public TimeOut(string caseNumber)
        {
            CaseNumber = caseNumber;
        }
    }
}
