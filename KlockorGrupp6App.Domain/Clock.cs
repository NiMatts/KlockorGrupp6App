namespace KlockorGrupp6App.Domain;

public class Clock
{
    public int Id { get; set; }
    public string Brand { get; set; } = null!;
    public string Model { get; set; } = null!;
    public decimal Price { get; set; }
    public DateTime Year { get; set; }

    public string CreatedByUserID { get; set; } = null!;
}

