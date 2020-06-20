using System.Collections.Generic;

namespace Entities
{
    public class OutPosition
    {
        public List<Position> positionList { get; set; }
        public Response msg { get; set; } = new Response();
    }

    public class Position
    {
        public double positionCode { get; set; }
        public double payableCode { get; set; }
        public string positionName { get; set; }
    }
}
