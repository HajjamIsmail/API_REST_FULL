using api.Models;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContosoPizza.Controllers;

[ApiController]
[Route("[controller]")]
public class PizzaController : ControllerBase
{
    public PizzaController()
    {
    }

    // GET all action
    [HttpGet]
    public ActionResult<List<Pizza>> GetAll() => PizzaService.GetAll();
    
    // GET by Id action
    [HttpGet("{id}")]//Réponse : 200 => Ok || 404 => Not found
    public ActionResult<Pizza> Get(int id)
    {
        var pizza = PizzaService.Get(id);

        if(pizza == null)
            return NotFound();

        return pizza;
    }    

    // POST action
    [HttpPost]//Réponse : 201 => CreateAtAction : succes || 400 => Bad Request
    public IActionResult Create(Pizza pizza)
    {            
        // This code will save the pizza and return a result
        PizzaService.Add(pizza);
        //createdAtAction : name of action
        //nameof : éviter de coder en dur le nom d'action
        return CreatedAtAction(nameof(Create), new { id = pizza.Id }, pizza);
    }

    // PUT action
    [HttpPut("{id}")]//Réponse : 204 => NoContent : update with succes  || 400 => Bad request : id not match or not valid
    public IActionResult Update(int id, Pizza pizza)
    {
        // This code will update the pizza and return a result
        if (id != pizza.Id)
        return BadRequest();
        var existingPizza = PizzaService.Get(id);
        if(existingPizza is null)
            return NotFound();
        PizzaService.Update(pizza);           
        return NoContent();
    }

    // DELETE action
    [HttpDelete("{id}")]//Réponse : 204 => NoContent : delete succes || 404 => NotFound : id not found
    public IActionResult Delete(int id)
    {
        // This code will delete the pizza and return a result
        var pizza = PizzaService.Get(id);
        if (pizza is null)
            return NotFound();
        PizzaService.Delete(id);
        return NoContent();
    }
}