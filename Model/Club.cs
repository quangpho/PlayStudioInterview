namespace Model;

public class Club
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public IList<Player> Members { get; set; } 
}