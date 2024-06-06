using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using apiepi.Context;
using apiepi.Models;
using Microsoft.AspNetCore.Authorization;

namespace apiepi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EpiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EpiController(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Retorma o epis exisentes
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     Get do epi os dados retornados serão
        ///       {
        ///           
        ///       }.
        ///       
        /// </remarks>
        /// <response code = "200"> Sucesso no retorno dos dados</reponse>

        // GET: api/Epi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Epi>>> GetEpis()
        {
          if (_context.Epis == null)
          {
              return NotFound();
          }
            return await _context.Epis.ToListAsync();
        }

        // GET: api/Epi/5
        [HttpGet("{id}")]
        [Authorize("Admin")]
        public async Task<ActionResult<Epi>> GetEpi(int id)
        {
          if (_context.Epis == null)
          {
              return NotFound();
          }
            var epi = await _context.Epis.FindAsync(id);

            if (epi == null)
            {
                return NotFound();
            }

            return epi;
        }
        /// <summary>
        /// Alteração conforme dados passados para o id informado
        /// </summary>


        // PUT: api/Epi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize("Admin")]
        public async Task<IActionResult> PutEpi(int id, Epi epi)
        {
            if (id != epi.EpiId)
            {
                return BadRequest();
            }

            _context.Entry(epi).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EpiExists(id))
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

        // POST: api/Epi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Epi>> PostEpi(Epi epi)
        {
          if (_context.Epis == null)
          {
              return Problem("Entity set 'AppDbContext.Epis'  is null.");
          }
            _context.Epis.Add(epi);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EpiExists(epi.EpiId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEpi", new { id = epi.EpiId }, epi);
        }

        // DELETE: api/Epi/5
        [HttpDelete("{id}")]
        [Authorize("Admin")]
        public async Task<IActionResult> DeleteEpi(int id)
        {
            if (_context.Epis == null)
            {
                return NotFound();
            }
            var epi = await _context.Epis.FindAsync(id);
            if (epi == null)
            {
                return NotFound();
            }

            _context.Epis.Remove(epi);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EpiExists(int id)
        {
            return (_context.Epis?.Any(e => e.EpiId == id)).GetValueOrDefault();
        }
    }
}
