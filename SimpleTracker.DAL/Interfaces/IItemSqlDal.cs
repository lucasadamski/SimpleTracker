using SimpleTracker.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTracker.DAL.Interfaces
{
    internal interface IItemSqlDal
    {
        public CreateNewItemResult CreateNewItem(Item item);
    }
}
