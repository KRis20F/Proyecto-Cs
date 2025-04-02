public class R2D2 : Robots
{
    public int Version { get; set; }
    public R2D2(int id, DateTime create, int version) : base(id, "R2D2", create)
    {
        this.Version = version;
    }

    public override void ShowData()
    {
        Console.WriteLine($"R2D2. ID: {id}, Modelo: {model}, Creación: {create.ToShortDateString()}, Versión: {Version}");
    }

    public int NumberOfBattles()
    {
        return new Random().Next(1, 10);
    }
}