
namespace Onnorokom.Forum.DataAccessLayer.Entities
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
}
