namespace TestTaking.Server.Models
{
  public class PracticeTestsDatabaseSettings : IPracticeTestsDatabaseSettings
  {
    public string PracticeTestsCollectionName { get; set; }
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
  }

  public interface IPracticeTestsDatabaseSettings
  {
    string PracticeTestsCollectionName { get; set; }
    string ConnectionString { get; set; }
    string DatabaseName { get; set; }
  }
}