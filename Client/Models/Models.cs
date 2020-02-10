namespace TestTaking.Client.Models
{
  public class PracticeTest
  {
    public string ID { get; set; }
    public string Title { get; set; }
    public string Topic { get; set; }

    public Question[] Questions { get; set; } // Should be an array of questions
    public bool Completed { get; set; }
    public bool Liked { get; set; }
  }
  public class Question
  {
    public string ID { get; set; }
    public string QuestionText { get; set; }
    public string Answer { get; set; }
  }
}
