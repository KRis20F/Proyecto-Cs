public class C3PO : Robots
{
    public C3PO(int id, DateTime create) : base(id, "C3PO", create){}

    public override void ShowData(){
        Console.WriteLine($"Soy C3PO. ID: {id}, Modelo: {model}, Creación: {create.ToShortDateString()}");
    }

    public int NumberOfRepairs() {
        return new Random().Next(0,1);
    }

    
}