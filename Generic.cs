public class Generic<T>
{
    public List<T> Robots;
    public int BonusPoints;

    public Generic()
    {
        Robots = new List<T>();
        BonusPoints = 0;
    }

    public void AddRobots(T robot)
    {
        Robots.Add(robot);
    }

    public int GetBonusPoints()
    {
        int setBonusPoints = 0;
        
        var r2d2Count = Robots.Count(r => r is R2D2);
        var c3poCount = Robots.Count(r => r is C3PO);
        var bb8Count = Robots.Count(r => r is BBB);
        
        setBonusPoints += r2d2Count * 3;  
        setBonusPoints += c3poCount * 2;  
        setBonusPoints += bb8Count * 1;   

        setBonusPoints += BonusPoints;

        return setBonusPoints;
    }

    public void RemoveRobots(T robot)
    {
        Robots.Remove(robot);
    }

    public void AddBonusPoints(int points)
    {
        BonusPoints += points;
    }

    public void ShowAllRobots()
    {
        foreach (T robot in Robots)
        {
            if (robot is Robots robotBase)
            {
                robotBase.ShowData();
            }
        }
    }
}