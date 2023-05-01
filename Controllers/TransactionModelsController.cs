using E_G_FinalProject.Models.Entities;
using E_G_FinalProject.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_G_FinalProject.Controllers
{
    public class TransactionModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TransactionModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TransactionModels
        public async Task<IActionResult> Index()
        {
            return _context.Transactions != null ?
                        View(await _context.Transactions.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Transactions'  is null.");
        }


        // GET: TransactionModels/AddOrEdit
        // GET: TransactionModels/AddOrEdit/5 <---corresponding record id
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new TransactionModel());
            }
            else
            {
                var transactionModel = await _context.Transactions.FindAsync(id);
                if (transactionModel == null)
                {
                    return NotFound();
                }
                return View(transactionModel);
            }
        }

        //POST: TransactionModels/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [Bind("TransactionId,AccountNumber,AccountName,BankName,SWIFTCode,Amount,Date")] TransactionModel transactionModel)
        {
            if (ModelState.IsValid)
            {
                if (id == 0)
                    _context.Add(transactionModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(transactionModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var transactionModel = await _context.Transactions.FindAsync(id);
            if (transactionModel == null)
            {
                return NotFound();
            }
            return View(transactionModel);

        }

        // POST: TransactionModels/AddOrEdit

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("TransactionId,AccountNumber,AccountName,BankName,SWIFTCode,Amount,Date")] TransactionModel transactionModel)
        {
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    transactionModel.Date = DateTime.Now;
                    _context.Add(transactionModel);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    try
                    {
                        _context.Update(transactionModel);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TransactionModelExists(transactionModel.TransactionId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }

                //returns a json object
                return RedirectToAction(nameof(Index));
            }
            return Content("Malformed JSON Object");
        }

        // GET: TransactionModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Transactions == null)
            {
                return NotFound();
            }

            var transactionModel = await _context.Transactions
                .FirstOrDefaultAsync(m => m.TransactionId == id);
            if (transactionModel == null)
            {
                return NotFound();
            }

            return View(transactionModel);
        }

        // POST: TransactionModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Transactions == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Transactions'  is null.");
            }
            var transactionModel = await _context.Transactions.FindAsync(id);
            if (transactionModel != null)
            {
                _context.Transactions.Remove(transactionModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionModelExists(int id)
        {
            return (_context.Transactions?.Any(e => e.TransactionId == id)).GetValueOrDefault();
        }
    }
}
