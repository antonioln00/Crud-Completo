using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace web_api.Controllers
{
    public class ClientesController : ApiController
    {
        private readonly Repositories.Cliente _repositorioCliente;

        public ClientesController()
        {
            _repositorioCliente = new Repositories.Cliente(Configurations.Databases.getConnectionString());
        }
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                return Ok(await _repositorioCliente.GetAll());
            } catch (Exception ex)
            {
                Logger.Log.write(ex, Configurations.Logger.getFullPath());
                return InternalServerError(ex);
            }
        }
        
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                Models.Cliente cliente = await _repositorioCliente.GetById(id);

                if (cliente.Id == 0)
                    return NotFound();

                return Ok(cliente);
            }
            catch (Exception ex)
            {
                Logger.Log.write(ex, Configurations.Logger.getFullPath());
                return InternalServerError(ex);
            }

        }

        public async Task<IHttpActionResult> Post([FromBody] Models.Cliente cliente)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await _repositorioCliente.Add(cliente);

                if (cliente.Id == 0)
                    return BadRequest();

                return Ok(cliente);
                

            }
            catch (Exception ex)
            {
                Logger.Log.write(ex, Configurations.Logger.getFullPath());
                return InternalServerError(ex);
            }
        }

        public async Task<IHttpActionResult> Put(int id, [FromBody]Models.Cliente cliente)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if(cliente.Id != id)
                    return BadRequest("Objeto não relacionado com a URL invocada.Ids diferentes.");

                bool update = await _repositorioCliente.Update(cliente);

                if(!update) 
                    return NotFound();

                return Ok(cliente);


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
                bool delete = await _repositorioCliente.Delete(id);

                if (!delete)
                    return NotFound();

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
