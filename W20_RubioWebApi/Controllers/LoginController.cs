using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using W20_RubioWebApi.Models;

namespace W20_RubioWebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/Login")]
    public class LoginController : ApiController
    {

        // POST api/Login/RegisterLogin
        [HttpPost]
        [Route("RegisterLogin")]
        public IHttpActionResult RegisterLogin(LoginModel login)
        {
            using (IDbConnection cnn = new ApplicationDbContext().Database.Connection)
            {
                // insertamos el login por defecto 1 para indicar que esta login cuando recibimos el login ya activo
                string sql = "INSERT INTO dbo.Logins(Id, Name, Login) " +
                    $"VALUES('{login.Id}','{login.Name}', 1)";
                try
                {
                    cnn.Execute(sql);
                }
                catch (Exception ex)
                {
                    return BadRequest("Error inserting login in database: " + ex.Message);
                }
                finally
                {
                    cnn.Close();
                }

                return Ok();
            }
        }

        [HttpPost]
        [Route("CerrarLogin")]
        public IHttpActionResult CerrarLogin(LoginModel login)
        {
            using(IDbConnection cnn = new ApplicationDbContext().Database.Connection)
            {
                string sql = "DELETE FROM dbo.Logins " +
                    $"where id = {login.Id}";

                try
                {
                    cnn.Execute(sql);

                }catch(Exception ex)
                {
                    return BadRequest("Error cerrando login in database: " + ex.Message);
                }
                finally
                {
                    cnn.Close();
                }
            }

            return Ok();
        }

        [HttpGet]
        [Route("UsuariosEnLinea")]
        public List<string> UsuariosEnLinea()
        {
            List<string> resultado = new List<string>();

            using (IDbConnection cnn = new ApplicationDbContext().Database.Connection)
            {
                string sql = "select * from dbo.Logins where Login = 1";

                try
                {
                    resultado = cnn.Query<string>(sql).ToList();
                }
                catch(Exception ex)
                {
                    throw new Exception("Error para ver la lista: " + ex.Message);
                }
                finally
                {
                    cnn.Close();
                }

            }

                return resultado;
        }

    }
}
