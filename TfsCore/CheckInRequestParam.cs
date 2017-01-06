using System;

namespace TfsCore
{
    public class CheckInRequestParam
    {

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Branch Branch { get; set; }

    }
}
