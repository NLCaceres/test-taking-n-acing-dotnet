using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestTaking.Shared.Models;

namespace TestTaking.Server.Controllers
{
  [Route("api/[controller]")] // Controller names are simply PracticeTests (minus controller suffix & case insensitive)
  [ApiController] // By default 404 is returned across the Web API
  public class PracticeTestsController : ControllerBase
  {
    private readonly PracticeTestContext _context;

    public PracticeTestsController(PracticeTestContext context)
    {
      _context = context;
    }

    // GET: api/PracticeTests
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PracticeTest>>> GetPracticeTests()
    {
      return await _context.PracticeTests.ToListAsync();
    }

    // GET: api/PracticeTests/5
    // Appends to [Route] attribute above! Adding [Route("added")] would make it api/PracticeTests/added/5 
    // Alternatively [Route("/added")] would call this action when api/PracticeTests/added was hit!
    [HttpGet("{id}")]
    public async Task<ActionResult<PracticeTest>> GetPracticeTest(int id)
    {
      var practiceTest = await _context.PracticeTests.FindAsync(id);

      if (practiceTest == null)
      {
        return NotFound();
      }

      return practiceTest;
    }

    // PUT: api/PracticeTests/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see https://aka.ms/RazorPagesCRUD.
    //@returns 204 No Content Response successful || 400 Bad Request if no ID match
    [HttpPut("{id}")] // Use PATCH to do partial updates (PUT requires the entire object to edit)
    public async Task<IActionResult> PutPracticeTest(int id, PracticeTest practiceTest)
    {
      if (id != practiceTest.ID)
      {
        return BadRequest();
      }

      _context.Entry(practiceTest).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!PracticeTestExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }

    // POST: api/PracticeTests
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see https://aka.ms/RazorPagesCRUD.
    [HttpPost]
    public async Task<ActionResult<PracticeTest>> PostPracticeTest(PracticeTest practiceTest)
    {
      _context.PracticeTests.Add(practiceTest);
      await _context.SaveChangesAsync();

      // nameof = C# operator - takes a var, type, or member & gives a string representation (useful here to grab a func name ref)
      //@params 2. Specifies route to use (base it on ID) 3. Object to post
      //@returns 201 HTTP status (preferred for POST)
      return CreatedAtAction(nameof(GetPracticeTest), new { id = practiceTest.ID }, practiceTest); //Last 2 params specify route (based on ID) & object to post
    }

    // DELETE: api/PracticeTests/5
    //@returns 200 OK meaning successful delete + object deleted
    [HttpDelete("{id}")]
    public async Task<ActionResult<PracticeTest>> DeletePracticeTest(int id)
    {
      var practiceTest = await _context.PracticeTests.FindAsync(id);
      if (practiceTest == null)
      {
        return NotFound();
      }

      _context.PracticeTests.Remove(practiceTest);
      await _context.SaveChangesAsync();

      return practiceTest;
    }

    private bool PracticeTestExists(int id)
    {
      return _context.PracticeTests.Any(e => e.ID == id);
    }
  }
}
