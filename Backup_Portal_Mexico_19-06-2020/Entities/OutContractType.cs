using System.Collections.Generic;

namespace Entities
{
    public class OutContractType
    {
        public List<ContractType> contractList { get; set; }
        public Response msg { get; set; } = new Response();
    }

    public class ContractType
    {
        public string contractName { get; set; }
        public double contractCode { get; set; }
        public double payableCode { get; set; }
        public double agreementCode { get; set; }
    }
}
