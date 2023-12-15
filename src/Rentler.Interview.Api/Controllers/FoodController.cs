using Microsoft.AspNetCore.Mvc;
using Rentler.Interview.Api.Data;
using Rentler.Interview.Client.Models;

namespace Rentler.Interview.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class FoodController : ControllerBase
{
    private readonly ILogger<FoodController> _logger;
    private readonly FoodRepository _repository;

    public FoodController(ILogger<FoodController> logger, FoodRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [HttpGet(Name = "Food")]
    public async Task<IEnumerable<Food>> GetAsync()
    {
        var food = await _repository.GetFood();
        return food;
    }

    [HttpPost(Name = "Food")]
    public async Task<Food> PostAsync([FromBody]Food food)
    {
        food = await _repository.InsertFood(food);
        return food;
    }
}
