using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TestTaking.Server.Models
{
  public class PracticeTest
  {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)] // Accepts string instead of ObjectId types
    public string ID { get; set; }
    public string Title { get; set; }
    public string Topic { get; set; }

    public Question[] Questions { get; set; } // Should be an array of questions
    public bool Completed { get; set; }
    public bool Liked { get; set; }
  }
  public class Question
  {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ID { get; set; }
    public string QuestionText { get; set; }
    public string Answer { get; set; }
  }
}
