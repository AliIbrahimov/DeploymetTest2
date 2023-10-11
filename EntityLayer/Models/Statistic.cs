using CoreLayer.Entities.Concrete;

namespace EntityLayer.Models;

public class Statistic:Entity
{
    public int Customer{ get; set; }
    public int ProjectsDone { get; set; }
    public int WinAwards { get; set; }
}
