namespace Models
{
    public class DataBase
    {
        public List<Case> Cases { get; } = new List<Case>()
        {
            new Case
            {
                Id = Guid.NewGuid(),
                Name = "First Case",
                Value = 1,
                TimeStemp = DateTime.UtcNow
            },
            new Case
            {
                Id = Guid.NewGuid(),
                Name = "Second Case",
                Value = 2,
                TimeStemp = DateTime.UtcNow
            },
        };
    }
}
