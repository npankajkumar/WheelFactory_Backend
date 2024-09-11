using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using WheelFactory.Models;

namespace WheelFactory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly WheelContext _context;

        public TransactionsController(WheelContext wc)
        {
            _context = wc;
        }


        // GET: api/Transactions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransaction(int id)
        {
            // Find the transaction by ID and include related Order
            var transaction = await _context.Transactions.Where(a => a.OrderId == id).ToListAsync();
                                            

            return Ok(transaction);
        }


        // GET: api/Transactions
        public async Task<IActionResult> GetTransactions()
        {
            // Include related Order data in the response
            var trans = await _context.Transactions.ToListAsync();
            return Ok(trans);
        }

        

        [HttpPost("{id}")]
        public async Task<IActionResult> PostTranscation(Transaction t) {
        
            _context.Transactions.Add(t);
            await _context.SaveChangesAsync();
            return Ok(t);

          
        }


    }
}



