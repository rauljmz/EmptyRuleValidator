using EmptyRuleValidator.Abstraction;
using EmptyRuleValidator.Data.Items;
using Sitecore.Data;
using Sitecore.Data.Databases;

namespace EmptyRuleValidator.Data.Database
{
    public class DatabaseWrapper : CustomDatabase, IDatabase
    {
        public DatabaseWrapper(Sitecore.Data.Database innerDatabase) : base(innerDatabase)
        {
        }

        public IItem GetItem(ID id)
        {
            return new ItemWrapper(InnerDatabase.GetItem(id));
        }
    }
}