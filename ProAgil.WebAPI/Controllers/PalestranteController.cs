using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain;
using ProAgil.Repository;

namespace ProAgil.WebAPI.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]
    
    public class PalestranteController : ControllerBase
    {
        private readonly IProAgilRepository _repo;

        public PalestranteController(IProAgilRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("{PalestranteId}")]
        public async Task<IActionResult> Get(int PalestranteId)
        {
            try
            {
                var results = await _repo.GetAllPalestranteAsync(PalestranteId, true);
                
                return Ok(results); 
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }
        }

        [HttpGet("getByName/{name}")]
        public async Task<IActionResult> Get(string tema)
        {
            try
            {
                var results = await _repo.GetAllPalestranteAsyncByName(tema, true);
                
                return Ok(results); 
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post(Palestrante model)
        {
            try
            {
                _repo.Add(model);

                if (await _repo.SaveChangesAsync())
                    {
                        return Created($"/api/palestrante/{model.Id}", model);
                    }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }

            return BadRequest();
        }
        [HttpPut]
        public async Task<IActionResult> Put(int PalestranteId, Evento model)
        {
            try
            {
                var palestrante = await _repo.GetAllPalestranteAsync(PalestranteId, false);

                if(palestrante == null) return NotFound();    

                _repo.Update(model);

                if (await _repo.SaveChangesAsync())
                    {
                        return Created($"/api/evento/{model.Id}", model);
                    }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }

            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int PalestranteId)
        {
            try
            {
                var palestrante = await _repo.GetAllPalestranteAsync(PalestranteId, false);

                if(palestrante == null) return NotFound();    

                _repo.Delete(palestrante);

                if (await _repo.SaveChangesAsync())
                    {
                        return Ok();
                    }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }

            return BadRequest();
        }
    }
}