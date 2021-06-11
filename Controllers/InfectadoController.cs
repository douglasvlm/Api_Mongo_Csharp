using Api.Data.Collections;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InfectadoController : ControllerBase
    {
        Data.MongoDB _mongoDB;
        IMongoCollection<Infectado> _infectadosCollection;

        public InfectadoController(Data.MongoDB mongoDB)
        {
            _mongoDB = mongoDB;
            _infectadosCollection = _mongoDB.DB.GetCollection<Infectado>(typeof(Infectado).Name.ToLower());
        }

        [HttpPost]
        public ActionResult SalvarInfectado([FromBody] InfectadoDto dto)
        {
            var infectado = new Infectado(dto.Rg, dto.DataNascimento, dto.Sexo, dto.Latitude, dto.Longitude);

            if (infectado.Rg > 99999999 && infectado.Rg < 999999999)
            {
                _infectadosCollection.InsertOne(infectado);
                return StatusCode(201, "Infectado adicionado com sucesso");
            } else {
                return StatusCode(400, "Bad Request. Numero de RG incorreto");
                //return BadRequest(); 
            }
        }

        [HttpGet]
        public ActionResult ObterInfectados()
        {
            var infectados = _infectadosCollection.Find(Builders<Infectado>.Filter.Empty).ToList();
            
            return Ok(infectados);
        }

        [HttpPut]
        public ActionResult AtualizarInfectados([FromBody] InfectadoDto dto)
        {
            var infectado = new Infectado( dto.Rg, dto.DataNascimento, dto.Sexo, dto.Latitude, dto.Longitude);
            
            _infectadosCollection.UpdateOne(Builders<Infectado>.Filter.Where(_ => _.Rg == dto.Rg), Builders<Infectado>
            .Update.Set("rg", dto.Rg)
            .Set("dataNascimento", dto.DataNascimento)
            .Set("sexo", dto.Sexo)
            .Set("latitude", dto.Latitude)
            .Set("longitude", dto.Longitude));

            var c =  _infectadosCollection.Find(x => x.Rg == dto.Rg).CountDocuments();

            if (c != 0)
            {
                return Ok(c + " documento(s) atualizado(s) com sucesso(s)");
            }
            else
            {
                return NotFound(); 
            }
        }

        [HttpDelete]
        public ActionResult ExcluirInfectados([FromBody] InfectadoDto dto)
        {
             _infectadosCollection.DeleteOne(Builders<Infectado>.Filter.Where(_ => _.Rg == dto.Rg));
            
             return Ok("Excluido com sucesso."); 
        }
    }
}
