using System;
using ApiMongo.Models;
using ApiMongo.Repository.Collections;
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
            //return Created("",infectadosCollection);
            return StatusCode(201, "Infectado adicionado com sucesso");
        }
        
        [HttpGet]
        public ActionResult ObterInfectados()
        {
            var infectados = infectadosCollection.Find(Builders<Infectado>.Filter.Empty).ToList();
            return Ok(infectados);
        }

        [HttpPut]
        public ActionResult AtualizarInfectados([FromBody] InfectadoDto dto)
        {
            var update = Builders<Infectado>.Update.Set("Sexo", dto.sexo);
            infectadosCollection.UpdateOne(Builders<Infectado>.Filter.Where(_=>_.dataNascimento == dto.dataNascimento), update);
            //return Created("",infectadosCollection);
            return Ok("Atualizado com sucesso");
        }

    [HttpDelete("{_dataNascimento}")]
        public ActionResult DeletarInfectados(DateTime _dataNascimento)
        {
            var infectado = Builders<Infectado>.Filter.Where(_=>_.dataNascimento == _dataNascimento);
            infectadosCollection.DeleteOne(infectado);
            //return Created("",infectadosCollection);
            return Ok("Deletado com sucesso");
        }
        
        
    }
}