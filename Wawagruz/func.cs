using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wawagruz.Models;

namespace Wawagruz
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Cast DataOrigin Interface to Delivery model
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static DeliveryModel CastToDeliveryModel(this DataOrigin Model) => new DeliveryModel()
        {
            ID = Model.ID,
            ClientName = Model.ClientName,
            Adress = Model.Adress,
            TimeOfOrdering = Model.TimeOfOrdering,
            PhoneNumber = Model.PhoneNumber,
            OrderContentType = Model.OrderContentType,
            OrderContentCount = Model.OrderContentCount,
            Status = Model.Status
        };

        /// <summary>
        /// Cast DataOrigin Interface to Order model
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static OrderModel CastToOrderModel(this DataOrigin Model) => new OrderModel()
        {
            ID = Model.ID,
            ClientName = Model.ClientName,
            Adress = Model.Adress,
            TimeOfOrdering = Model.TimeOfOrdering,
            PhoneNumber = Model.PhoneNumber,
            OrderContentType = Model.OrderContentType,
            OrderContentCount = Model.OrderContentCount,
            Status = Model.Status
        };
    }
}
