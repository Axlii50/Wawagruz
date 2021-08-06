using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wawagruz.Models
{
    public interface DataOrigin
    {
         string ID { get; set; }

         string ClientName { get; set; }

         string Adress { get; set; }

         DateTime TimeOfOrdering { get; set; }

         string PhoneNumber { get; set; }

         EnumesTypes.BagTypes OrderContentType { get; set; }

         int OrderContentCount { get; set; }

         EnumesTypes.OrderStatus Status { get; set; }
    }
}
