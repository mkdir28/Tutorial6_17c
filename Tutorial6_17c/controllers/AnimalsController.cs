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
    
    [HttpGet("/api/animals")]
    public IActionResult GetAnimals()
    {
        //open connectiom
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Docker"));
        connection.Open();
        
        //create command
        SqlCommand orderBy = new SqlCommand();
        orderBy.Connection = connection;
        orderBy.CommandText = "select * \n from Animal \n ORDER BY Name;";
        
        //execute command
        var reader = orderBy.ExecuteReader();
        var animals = new List<Animal>();
        int idAnimalOrdinal = reader.GetOrdinal("IdAnimal");
        int namelOrdinal = reader.GetOrdinal("Name");
        int descriptionOrdinal = reader.GetOrdinal("Description");
        int categoryOrdinal = reader.GetOrdinal("Category");
        int areaOrdinal = reader.GetOrdinal("Area");
        
        while (reader.Read())
        {
            animals.Add(new Animal()
            {
                IdAnimal = reader.GetInt32(idAnimalOrdinal),
                Name = reader.GetString(namelOrdinal),
                Description = reader.GetString(descriptionOrdinal),
                Category = reader.GetString(categoryOrdinal),
                Area = reader.GetString(areaOrdinal)
            });
        }
        return Ok(animals);
    }

    [HttpPost("api/animals")]
    public IActionResult AddAnimal(AddAnimal animal)
    {
        //open connectiom
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Docker"));
        connection.Open();
        
        //create command
        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "Insert into Animal Values('@AnimalName', '@Description', '@Category', '@Area');";
        
        command.Parameters.AddWithValue("@IdAnimal", animal.IdAnimal);
        command.Parameters.AddWithValue("@AnimalName", animal.Name);
        command.Parameters.AddWithValue("@Description", animal.Description);
        command.Parameters.AddWithValue("@Category", animal.Category);
        command.Parameters.AddWithValue("@Area", animal.Area);

        //execute
        command.ExecuteNonQuery();
        return Created("", null);
    }
    
    [HttpPut("/api/animals/{IdAnimal:int}")]
    public IActionResult UpdateAnimal(int id, Animal animal)
    {
        //open connectiom
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Docker"));
        connection.Open();
        
        //create command
        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "UPDATE Animal SET Name=@AnimalName, Description=@Description, Category=@Category, Area=@Area WHERE IdAnimal = @IdAnimal";
        
        command.Parameters.AddWithValue("@IdAnimal", animal.IdAnimal);
        command.Parameters.AddWithValue("@AnimalName", animal.Name);
        command.Parameters.AddWithValue("@Description", animal.Description);
        command.Parameters.AddWithValue("@Category", animal.Category);
        command.Parameters.AddWithValue("@Area", animal.Area);
        
        //execute
        command.ExecuteNonQuery();
        return Created("", null);
    }

    [HttpDelete("/api/animals/{IdAnimal:int}")]
    public IActionResult DeleteAnimal(int id)
    {
        //open connectiom
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Docker"));
        connection.Open();
        
        //create command
        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "DELETE FROM Animal WHERE IdAnimal = @IdAnimal";
        
        command.Parameters.AddWithValue("@IdAnimal", id);
        
        //execute
        command.ExecuteNonQuery();
        return Created("", null);
    }
}