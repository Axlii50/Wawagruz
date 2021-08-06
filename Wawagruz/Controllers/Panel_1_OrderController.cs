using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Wawagruz.Data;
using Wawagruz.Models;
using static Wawagruz.Models.EnumesTypes;
using static Wawagruz.ExtensionMethods;

namespace Wawagruz
{
    [NoDirectAccess]
    public class Panel_1_OrderController : Controller
    {
        private readonly WawagruzContext _context;

        public Panel_1_OrderController(WawagruzContext context)
        {
            _context = context;
        }

        // GET: Panel_1_Order
        [Route("Order/index")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Order.ToListAsync());
        }

        // GET: Panel_1_Order/Details/5
        
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderModel = await _context.Order
                .FirstOrDefaultAsync(m => m.ID == id);
            if (orderModel == null)
            {
                return NotFound();
            }

            return View(orderModel);
        }

        
        public async Task<IActionResult> Create()
        {
            var creating = new OrderModel()
            {
                Status = OrderStatus.Nowe_Zamówienie,
                ID = Guid.NewGuid().ToString()
            };
            return View(creating);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create([Bind("ID,ClientName,Adress,TimeOfOrdering,PhoneNumber,OrderContentType,OrderContentCount,Status")] OrderModel model)
        {
            if (ModelState.IsValid)
            {
                _context.Order.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("Index");
        }

        // GET: Panel_1_Order/Edit/5
        
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderModel = await _context.Order.FindAsync(id);
            if (orderModel == null)
            {
                return NotFound();
            }
            return View(orderModel);
        }

        // POST: Panel_1_Order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,ClientName,Adress,TimeOfOrdering,PhoneNumber,OrderContentType,OrderContentCount,Status")] OrderModel orderModel)
        {
            if (id != orderModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Order.Update(orderModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderModelExists(orderModel.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(orderModel);
        }

        //change to finish
        [ActionName("Move")]
        [Route("Move/{id}")]
        public async Task<IActionResult> Move(string id)
        {
            DataOrigin tomove = _context.Order.FirstOrDefaultAsync(m => m.ID == id).Result;

            if (tomove.Status == OrderStatus.Dostarczony)
                tomove.Status = OrderStatus.Do_Odebrania;
            if (tomove.Status == OrderStatus.Nowe_Zamówienie)
                tomove.Status = OrderStatus.Do_Dostarczenia;

            _context.Delivery.Add(tomove.CastToDeliveryModel());
            _context.Order.Remove((OrderModel)tomove);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //return View("Index");
        }


        public async Task<IActionResult> DeliveryPanel()
        {
            return RedirectToAction("Index", "Panel_2_Order");
        }


        // GET: Panel_1_Order/Delete/5

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderModel = await _context.Order
                .FirstOrDefaultAsync(m => m.ID == id);
            if (orderModel == null)
            {
                return NotFound();
            }

            return View(orderModel);
        }

        // POST: Item/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var itemObject = await _context.Order.FindAsync(id);
            _context.Order.Remove(itemObject);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool OrderModelExists(string id)
        {
            return _context.Order.Any(e => e.ID == id);
        }
    }
}
