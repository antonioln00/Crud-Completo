using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace web_api.Controllers
{
    public class MedicosController : ApiController
    {
        Repositories.Medico _repositorioMedico;

        public MedicosController()
        {
            _repositorioMedico = new Repositories.Medico(Configurations.Databases.getConnectionString());
        }

        public async Task<IHttpActionResult> Get()
        {
            try
            {
                return Ok(await _repositorioMedico.GetAll());
            }
            catch (Exception ex)
            {
                Logger.Log.write(ex, Configurations.Logger.getFullPath());
                return InternalServerError(ex);
            }            
        }

        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                Models.Medico medico = await _repositorioMedico.GetById(id);

                if (medico.Id == 0)
                    return NotFound();

                return Ok(medico);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }            
        }

        public async Task<IHttpActionResult> Post([FromBody] Models.Medico medico)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _repositorioMedico.Add(medico);

                if (medico.Id == 0)
                {
                    return BadRequest();
                }

                return Ok(medico);
            }
            catch (Exception ex)
            {
                Logger.Log.write(ex, Configurations.Logger.getFullPath());
                return InternalServerError();
            }
                

            
        }

        public async Task<IHttpActionResult> Put(int id, [FromBody] Models.Medico medico)
        {
            try
            {
                if(medico.Id != id)
                {
                    return BadRequest("Objeto não relacionado com a URL invocada. Ids diferentes");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                bool update = await _repositorioMedico.Update(medico);

                if (!update)
                {
                    return NotFound();
                }

                return Ok(medico);
            }
            catch (Exception ex)
            {
                Logger.Log.write(ex, Configurations.Logger.getFullPath());
                return InternalServerError(ex);
            }
        }

        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                bool excluir = await _repositorioMedico.Delete(id);

                if (!excluir)
                {
                    return NotFound();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                Logger.Log.write(ex, Configurations.Logger.getFullPath());
                return InternalServerError(ex);
            }
        }
    }
}
