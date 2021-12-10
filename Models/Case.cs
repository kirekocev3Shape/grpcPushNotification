namespace Models
{
    public class Case
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public DateTime TimeStemp { get; set; }
    }
}
