using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestTaking.Server.Models;
using TestTaking.Server.Services;

namespace TestTaking.Server.Controllers
{
  [Route("api/[controller]")] // Controller names are simply ModelEntitys (e.g. PracticeTests) (no 'controller' suffix & case insensitive)
  [ApiController] // By default 404 is returned across the Web API
  public class PracticeTestsController : ControllerBase
  {
    private readonly PracticeTestService _practiceTestService;

    public PracticeTestsController(PracticeTestService practiceTestService)
    {
      _practiceTestService = practiceTestService;
    }

    // GET: api/PracticeTests //! READ
    [HttpGet]
    /* The following can be written a few different ways depending on flexibility needed or return type needed!
       public async Task<IActionResult> GetPracticeTests - which uses the interface version to allow any ActionResult to be returned
       public async Task<ActionResult<IEnumerable<PracticeTest>>> - Similarly uses an interface - C# enumerable collections (so you can foreach it)
       public async Task<ActionResult<IList<PracticeTest>>> - Or even just an IList (which also happens to implement IEnumerable)
       public async Task<ActionResult<List<PracticeTest>>> => 'new ObjectResult(await _practiceTestService.Get())' 
       - Above is super specific! and uses ActionResult as often expected with controllers!
       Lesson here - be specific! BUT consider the generic as needed. C# can infer A TON using interfaces, generics and the <> (type parameter operator)
    */
    public async Task<List<PracticeTest>> GetPracticeTests() => new List<PracticeTest>(await _practiceTestService.Get());

    // GET: api/PracticeTests/5 //! READ
    // Appends to [Route] attribute above! Adding [Route("added")] would make it api/PracticeTests/added/5 
    // Alternatively [Route("/added")] would call this action when api/PracticeTests/added was hit!
    [HttpGet("{id:length(24)}", Name = "GetPracticeTest")] //! ID must be 24, Name works as a ref for others to call as needed (e.g. the POST/Create later)
    public async Task<ActionResult<PracticeTest>> GetPracticeTest(string id)
    {
      var practiceTest = await _practiceTestService.Get(id);
      if (practiceTest == null) return NotFound();
      return practiceTest;
    }

    // POST: api/PracticeTests - //! CREATE
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see https://aka.ms/RazorPagesCRUD.
    [HttpPost]
    // nameof = C# operator - takes a var, type, or member & gives a string representation (useful here to grab a func name ref)
    //@params 2. Specifies route to use (base it on ID) 3. Object to post
    //@returns 201 HTTP status (preferred for POST)    
    public async Task<ActionResult<PracticeTest>> PostPracticeTest(PracticeTest practiceTest)
    {
      await _practiceTestService.Create(practiceTest);
      return CreatedAtRoute("GetPracticeTest", new { id = practiceTest.ID }, practiceTest);
    }

    // PUT: api/PracticeTests/5 //! UPDATE
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see https://aka.ms/RazorPagesCRUD.
    //@returns 204 No Content Response successful || 400 Bad Request if no ID match
    [HttpPut("{id:length(24)}")] // Use PATCH to do partial updates (PUT requires the entire object to edit)
    public async Task<IActionResult> PutPracticeTest(string id, PracticeTest practiceTest)
    {
      var testFromDB = await _practiceTestService.Get(id);
      if (testFromDB == null) return NotFound(); // Could also use a BadRequest!

      await _practiceTestService.Update(id, practiceTest);

      return NoContent();
    }

    // DELETE: api/PracticeTests/5
    //@returns 200 OK meaning successful delete + object deleted
    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> DeletePracticeTest(string id)
    {
      var testFromDB = await _practiceTestService.Get(id);
      if (testFromDB == null) return NotFound();

      await _practiceTestService.Remove(testFromDB.ID);

      return NoContent();
    }
  }
}
