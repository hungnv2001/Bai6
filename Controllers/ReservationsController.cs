using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bai6.Context;
using Bai6.Model;
using Microsoft.IdentityModel.Tokens;

namespace Bai6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly Bai6Context _context;

        public ReservationsController(Bai6Context context)
        {
            _context = context;
        }

        // GET: api/Reservations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations([FromQuery]string? keyword)
        {
          if (_context.Reservations == null)
          {
              return NotFound();
          }
            var lstData= await _context.Reservations.ToListAsync();
            if (!keyword.IsNullOrEmpty())
            {
                lstData=lstData.Where(p=>p.Name.ToUpper().Contains(keyword.ToUpper())).ToList();
            }
            return Ok(lstData);
        }

        // GET: api/Reservations/5
        [HttpGet("getByID/{id}")]
        public async Task<ActionResult<Reservation>> GetReservation(int id)
        {
          if (_context.Reservations == null)
          {
              return NotFound();
          }
            var reservation = await _context.Reservations.FindAsync(id);

            if (reservation == null)
            {
                return NotFound();
            }

            return reservation;
        }

        // PUT: api/Reservations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("update")]
        public async Task<IActionResult> PutReservation( Reservation reservation)
        {
          

            _context.Entry(reservation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
             
            }

            return NoContent();
        }

        // POST: api/Reservations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Reservation>> PostReservation(Reservation reservation)
        {
          if (_context.Reservations == null)
          {
              return Problem("Entity set 'Bai6Context.Reservations'  is null.");
          }
         
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReservation", new { id = reservation.Id }, reservation);
        }

        // DELETE: api/Reservations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            if (_context.Reservations == null)
            {
                return NotFound();
            }
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReservationExists(int id)
        {
            return (_context.Reservations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
