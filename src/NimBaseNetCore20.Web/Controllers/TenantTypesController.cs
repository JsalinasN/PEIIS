using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NimBaseNetCore20.Data;
using NimBaseNetCore20.Domain;

namespace NimBaseNetCore20.Web.Controllers
{
    public class TenantTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TenantTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TenantTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.TenantTypes.ToListAsync());
        }

        // GET: TenantTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenantType = await _context.TenantTypes
                .SingleOrDefaultAsync(m => m.TenantTypeId == id);
            if (tenantType == null)
            {
                return NotFound();
            }

            return View(tenantType);
        }

        // GET: TenantTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TenantTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenantTypeId,Name,Description,Status,CreationDate,ChangeDate,CreationUser,ChangeUser")] TenantType tenantType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tenantType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tenantType);
        }

        // GET: TenantTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenantType = await _context.TenantTypes.SingleOrDefaultAsync(m => m.TenantTypeId == id);
            if (tenantType == null)
            {
                return NotFound();
            }
            return View(tenantType);
        }

        // POST: TenantTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TenantTypeId,Name,Description,Status,CreationDate,ChangeDate,CreationUser,ChangeUser")] TenantType tenantType)
        {
            if (id != tenantType.TenantTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tenantType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TenantTypeExists(tenantType.TenantTypeId))
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
            return View(tenantType);
        }

        // GET: TenantTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenantType = await _context.TenantTypes
                .SingleOrDefaultAsync(m => m.TenantTypeId == id);
            if (tenantType == null)
            {
                return NotFound();
            }

            return View(tenantType);
        }

        // POST: TenantTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tenantType = await _context.TenantTypes.SingleOrDefaultAsync(m => m.TenantTypeId == id);
            _context.TenantTypes.Remove(tenantType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TenantTypeExists(int id)
        {
            return _context.TenantTypes.Any(e => e.TenantTypeId == id);
        }
    }
}
