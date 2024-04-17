using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Tutorial6_17c.dto;
using Tutorial6_17c.models;

namespace Tutorial6_17c.controllers;
[ApiController]
[Route("api/[controller]")]
public class AnimalsController : ControllerBase
{
    private readonly IConfiguration _configuration;
    public AnimalsController(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    [HttpGet]
    public IActionResult GetAnimals()
    {

        using (SqlConnection connection2 = new SqlConnection()) ;
        //open connectiom
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Docker"));
        connection.Open();
        //create command
        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "select *\nfrom Animal;";
        
        //execute command
        var reader = command.ExecuteReader();
        var animals = new List<Animal>();
        int idAnimalOrdinal = reader.GetOrdinal("IdAnimal");
        int namelOrdinal = reader.GetOrdinal("Name");

        while (reader.Read())
        {
            animals.Add(new Animal()
            {
                IdAnimal = reader.GetInt32(idAnimalOrdinal),
                Name = reader.GetString(namelOrdinal)
            });
        }
        return Ok(animals);
    }

    [HttpPost]
    public IActionResult AddAnimal(AddAnimal animal)
    {
        using (SqlConnection connection2 = new SqlConnection()) ;
        //open connectiom
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Docker"));
        connection.Open();
        //create command
        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "Insert into Animal Values('@animalName', '', '', '');";
        command.Parameters.AddWithValue("@animalName", animal.Name);
        //execute
        command.ExecuteNonQuery();
        return Created("", null);
    }
}