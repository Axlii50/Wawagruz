using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Wawagruz.Data;
using Wawagruz.Models;
using static Wawagruz.Models.EnumesTypes;

namespace Wawagruz
{
    [NoDirectAccess]
    public class Panel_2_OrderController : Controller
    {
        private readonly WawagruzContext _context;

        public Panel_2_OrderController(WawagruzContext context)
        {
            _context = context;
        }

        // GET: Panel_2_Order
        [Route("Delivery/index")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Delivery.ToListAsync());
        }

        // GET: Panel_2_Order/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderModel = await _context.Delivery
                .FirstOrDefaultAsync(m => m.ID == id);
            if (orderModel == null)
            {
                return NotFound();
            }

            return View(orderModel);
        }

        [Route("Delivery/GotBack/({id}")]
        public async Task<IActionResult> GotBack(string id)
        {
            //made partially
            
            DataOrigin model = await _context.Delivery.FirstOrDefaultAsync(m => m.ID == id);
            model.Status = OrderStatus.Zakonczony;

            _context.Delivery.Remove((DeliveryModel)model);
            _context.Order.Add(model.CastToOrderModel());

            await _context.SaveChangesAsync();

            return View("Index", await _context.Delivery.ToListAsync());
        }

        [Route("Delivery/Delivered/({id}")]
        public async Task<IActionResult> Delivered(string id)
        {
            DataOrigin model = await _context.Delivery.FirstOrDefaultAsync(m => m.ID == id);
            model.Status = OrderStatus.Dostarczony;
            _context.Delivery.Remove((DeliveryModel)model);
            _context.Order.Add(model.CastToOrderModel());
            await _context.SaveChangesAsync();

            return View("Index", await _context.Delivery.ToListAsync());
        }
    }
}
