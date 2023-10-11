using CoreLayer.Entities.Abstract;

namespace CoreLayer.Entities.Concrete;

public class Entity:IEntity
{
    public int Id { get; set; }
    public bool isDeleted { get; set; }
}
