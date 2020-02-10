using TestTaking.Server.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace TestTaking.Server.Services
{
  public class PracticeTestService
  {
    private readonly IMongoCollection<PracticeTest> _practiceTests; //? Underscores typically notate private properties that will use other Properties for access to it

    public PracticeTestService(IPracticeTestsDatabaseSettings settings)
    {
      var client = new MongoClient(settings.ConnectionString); //? MongoClients require a singleton service lifetime so in Startup Config set this service to singleton!
      var database = client.GetDatabase(settings.DatabaseName);

      _practiceTests = database.GetCollection<PracticeTest>(settings.PracticeTestsCollectionName);
    }

    //? The RUD operations in Mongo all take filter lambdas here
    public async Task<IEnumerable<PracticeTest>> Get() => // Grab all practice tests
            await _practiceTests.Find(practiceTest => true).ToListAsync(); //? FindAsync is an option, BUT ToListAsync returns actual docs whereas FindAsync returns a cursor to grab said docs!
    public async Task<PracticeTest> Get(string id) => // Grab based on ID
        await _practiceTests.Find<PracticeTest>(practiceTest => practiceTest.ID == id).FirstOrDefaultAsync(); //? Same idea here as FindAsync vs Find & ToListAsync

    public async Task<PracticeTest> Create(PracticeTest practiceTest) // Could also just forget the return making //@returns Task or void with a 1-liner body 
    {
      //? The moment await is called, the func is paused. To avoid pausing, you could assign an async func to a task var instead, 
      //? but when you need an async func to be successful before doing anything else. Await it!
      await _practiceTests.InsertOneAsync(practiceTest);
      return practiceTest;
    }

    //? Could return void BUT Task = better by enabling proper await. Void typically signifies a top level func or event handler
    public async Task Update(string id, PracticeTest practiceTestIn) => // Update based on ID and the obj itself. Obj required!
        await _practiceTests.ReplaceOneAsync(practiceTest => practiceTest.ID == id, practiceTestIn);

    public async Task Remove(PracticeTest practiceTestIn) => // Delete based on obj
        await _practiceTests.DeleteOneAsync(practiceTest => practiceTest.ID == practiceTestIn.ID);
    public async Task Remove(string id) => // Delete based on obj ID
        await _practiceTests.DeleteOneAsync(practiceTest => practiceTest.ID == id);
  }

}