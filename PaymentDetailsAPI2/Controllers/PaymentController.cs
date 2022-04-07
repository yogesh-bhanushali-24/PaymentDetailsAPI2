﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentDetailsAPI2.Models;

namespace PaymentDetailsAPI2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PaymentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Payment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentDetails>>> Getdetails()
        {
            return await _context.details.ToListAsync();
        }

        // GET: api/Payment/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDetails>> GetPaymentDetails(int id)
        {
            var paymentDetails = await _context.details.FindAsync(id);

            if (paymentDetails == null)
            {
                return NotFound();
            }

            return paymentDetails;
        }

        // PUT: api/Payment/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentDetails(int id, PaymentDetails paymentDetails)
        {
            if (id != paymentDetails.PaymentDetailId)
            {
                return BadRequest();
            }

            _context.Entry(paymentDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentDetailsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Payment
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PaymentDetails>> PostPaymentDetails(PaymentDetails paymentDetails)
        {
            _context.details.Add(paymentDetails);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPaymentDetails", new { id = paymentDetails.PaymentDetailId }, paymentDetails);
        }

        // DELETE: api/Payment/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PaymentDetails>> DeletePaymentDetails(int id)
        {
            var paymentDetails = await _context.details.FindAsync(id);
            if (paymentDetails == null)
            {
                return NotFound();
            }

            _context.details.Remove(paymentDetails);
            await _context.SaveChangesAsync();

            return paymentDetails;
        }

        private bool PaymentDetailsExists(int id)
        {
            return _context.details.Any(e => e.PaymentDetailId == id);
        }
    }
}
