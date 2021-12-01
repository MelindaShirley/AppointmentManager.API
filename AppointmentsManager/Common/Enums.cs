using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentsManager.Common
{
    public class Enums
    {
        public enum enmAppointmentStatus
        {   
            Success=0,
            SamePeriod,
            Intersects,
            Overlaps
        }
    }
}
