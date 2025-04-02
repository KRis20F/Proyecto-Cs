public class Robots
{
    public int id { get ; set ; }
    public string model { get ; set ; }
    public DateTime create{ get ; set ; }
    

    public Robots(int id, string model, DateTime create) {
        this.id = id;
        this.model = model;
        this.create = create;
        
    }

    public virtual void ShowData() {
        Console.WriteLine($"ID:{id} Modelo:{model}, Dia Creada:{create}");
    }

}