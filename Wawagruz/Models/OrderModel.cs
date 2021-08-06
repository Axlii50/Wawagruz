using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Wawagruz.Models
{
    public class EnumesTypes
    {
        public enum OrderStatus
        {
            Zakonczony,
            Do_Odebrania,
            Dostarczony,
            Do_Dostarczenia,
            Nowe_Zamówienie
        }

        public enum BagTypes
        {
            bag1 = 100,
            bag2 = 200, 
            bag3 = 300
        }

        public static SelectList GetBagTypes()
        {
            return new SelectList(Enum.GetValues(typeof(BagTypes)));
        }
        public static SelectList GetOrderTypes()
        {
            return new SelectList(Enum.GetValues(typeof(OrderStatus)));
        }

    }

    public class OrderModel : DataOrigin
    {
        public string ID { get ; set ; }
        public string ClientName { get ; set ; }
        public string Adress { get ; set ; }
        [DataType(DataType.Date)]
        public DateTime TimeOfOrdering { get ; set ; }
        public string PhoneNumber { get ; set ; }
        public EnumesTypes.BagTypes OrderContentType { get ; set ; }
        public int OrderContentCount { get ; set ; }
        public EnumesTypes.OrderStatus Status { get ; set ; }
    }
    public class DeliveryModel : DataOrigin
    {
        public string ID { get ; set ; }
        public string ClientName { get ; set ; }
        public string Adress { get ; set ; }
        [DataType(DataType.Date)]
        public DateTime TimeOfOrdering { get ; set ; }
        public string PhoneNumber { get ; set ; }
        public EnumesTypes.BagTypes OrderContentType { get ; set ; }
        public int OrderContentCount { get ; set ; }
        public EnumesTypes.OrderStatus Status { get ; set ; }
    }

}
