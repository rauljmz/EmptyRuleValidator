using System;
using EmptyRuleValidator.Abstraction;
using Sitecore.Data;

namespace EmptyRuleValidator.Data.Database
{
    class ItemRepository : IItemRepository
    {
        private readonly IDatabase _database;

        public ItemRepository(IDatabase database)
        {
            _database = database;
        }

        public IItem Get(Guid id)
        {
            return _database.GetItem(new ID(id));
        }
    }
}