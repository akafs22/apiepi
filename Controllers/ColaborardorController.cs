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
    public class ColaborardorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ColaborardorController(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Retorma os cadastros exisentes
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     Get do cadastro os dados retornados serão
        ///       {
        ///           "id": 0,
        ///           "nome": "Nome de aluno",
        ///           "curso": "Nome do curso",
        ///           "ra": 1234
        ///       }.
        ///       
        /// </remarks>
        /// <response code = "200"> Sucesso no retorno dos dados</reponse>
        // GET: api/Entrega

        // GET: api/Colaborardor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Colaborador>>> GetColaboradors()
        {
          if (_context.Colaboradors == null)
          {
              return NotFound();
          }
            return await _context.Colaboradors.ToListAsync();
        }

        // GET: api/Colaborardor/5
        [HttpGet("{id}")]
        [Authorize("Admin")]
        public async Task<ActionResult<Colaborador>> GetColaborador(int id)
        {
          if (_context.Colaboradors == null)
          {
              return NotFound();
          }
            var colaborador = await _context.Colaboradors.FindAsync(id);

            if (colaborador == null)
            {
                return NotFound();
            }

            return colaborador;
        }
        /// <summary>
        /// Alteração conforme dados passados para o id informado
        /// </summary>

        // PUT: api/Colaborardor/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize("Admin")]
        public async Task<IActionResult> PutColaborador(int id, Colaborador colaborador)
        {
            if (id != colaborador.CodigoId)
            {
                return BadRequest();
            }

            _context.Entry(colaborador).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColaboradorExists(id))
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

        // POST: api/Colaborardor
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Colaborador>> PostColaborador(Colaborador colaborador)
        {
          if (_context.Colaboradors == null)
          {
              return Problem("Entity set 'AppDbContext.Colaboradors'  is null.");
          }
            _context.Colaboradors.Add(colaborador);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ColaboradorExists(colaborador.CodigoId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetColaborador", new { id = colaborador.CodigoId }, colaborador);
        }

        // DELETE: api/Colaborardor/5
        [HttpDelete("{id}")]
        [Authorize("Admin")]
        public async Task<IActionResult> DeleteColaborador(int id)
        {
            if (_context.Colaboradors == null)
            {
                return NotFound();
            }
            var colaborador = await _context.Colaboradors.FindAsync(id);
            if (colaborador == null)
            {
                return NotFound();
            }

            _context.Colaboradors.Remove(colaborador);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ColaboradorExists(int id)
        {
            return (_context.Colaboradors?.Any(e => e.CodigoId == id)).GetValueOrDefault();
        }

        [HttpGet("Epis")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Entrega>>> GetEpiColab(int id){
            if (_context.Entregas == null){
                return NotFound();
            }
            else{
                var epi = await _context.Entregas.Where(e=>e.ColaboradorId == id).ToListAsync();
                if (epi == null){
                    return NotFound();
                }
                else
                {
                    return epi;
                }
            }
        }
    }
}
