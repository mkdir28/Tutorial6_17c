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
        using (SqlConnection connection2 = new SqlConnection()) ;
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
        using (SqlConnection connection2 = new SqlConnection());
        //open connectiom
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Docker"));
        connection.Open();
        //create command
        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "Insert into Animal Values('@animalName', '@description', '@category', '@area');";
        command.Parameters.AddWithValue("@idAnuimal", animal.IdAnimal);
        command.Parameters.AddWithValue("@animalName", animal.Name);
        command.Parameters.AddWithValue("@description", animal.Description);
        command.Parameters.AddWithValue("@category", animal.Category);
        command.Parameters.AddWithValue("@area", animal.Area);

        //execute
        command.ExecuteNonQuery();
        return Created("", null);
    }
    
    [HttpPut("/api/animals/{IdAnimal:int}")]
    public IActionResult UpdateStudent(int id, Animal animal)
    {
        using (SqlConnection connection2 = new SqlConnection());
        //open connectiom
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Docker"));
        connection.Open();
        //create command
        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = 
    }
}