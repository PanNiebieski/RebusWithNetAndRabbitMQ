using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7Saga.Saga.Messages
{
    public class MyTimeOut
    {
        public string CaseNumber { get; }

        public MyTimeOut(string caseNumber)
        {
            CaseNumber = caseNumber;
        }
    }
}
