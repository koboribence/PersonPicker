namespace PersonPicker.Models
{
    public class Draw
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string PersonId { get; set; } = string.Empty;

        public DateTime SelectedAt { get; set; } = DateTime.UtcNow;

        public Person? Person { get; set; }
    }
}
