class BBB : Robots
{
    public float version { get; set; }
    public BBB (int id, DateTime create, float version): base(id, "BBB", create) {
        this.version = version;
    }

    public override void ShowData () {
        Console.WriteLine($"Soy BBB. ID: {id}, Modelo: {model}, Creación: {create.ToShortDateString()}, Versión: {version}");
    }

    public int NumberOfFlights() {
        return new Random().Next(3,0);
    }

    public int NumberOfRepairs() {
        return new Random().Next(3,0);
    }
}