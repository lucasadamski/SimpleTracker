using SimpleTracker.DAL.Interfaces;
using SimpleTracker.DTO;

namespace SimpleTracker.DAL
{
    public class ItemSqlDal : IItemSqlDal
    {
        public CreateNewItemResult CreateNewItem(Item item)
        {
            return new CreateNewItemResult();
        }
    }
}
