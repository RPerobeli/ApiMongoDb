using ApiMongo.Models;
using ApiMongo.Repository.Collections;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;


namespace ApiMongo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InfectadoController : ControllerBase
    {
        Repository.MongoDbContext mongoDb;
        IMongoCollection<Infectado> infectadosCollection;
        public InfectadoController(Repository.MongoDbContext _mongoDB)
        {
            mongoDb = _mongoDB;
            infectadosCollection = mongoDb.DB.GetCollection<Infectado>(typeof(Infectado).Name.ToLower());
        }

        [HttpPost]
        public ActionResult SalvarInfectado([FromBody] InfectadoDto dto)
        {
            var infectado = new Infectado(dto.dataNascimento, dto.sexo, dto.latitude, dto.longitude);
            infectadosCollection.InsertOne(infectado);
            return Created("",infectadosCollection);
            // return StatusCode(201, "Infectado adicionado com sucesso");
        }
        
        
    }
}