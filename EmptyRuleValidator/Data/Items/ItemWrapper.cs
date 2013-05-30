using EmptyRuleValidator.Abstraction;
using EmptyRuleValidator.Data.Database;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace EmptyRuleValidator.Data.Items
{
    public class ItemWrapper : CustomItem, IItem    
    {
        public ItemWrapper(Item innerItem) : base(innerItem)
        {
            Database = new DatabaseWrapper(base.Database);
            ID = InnerItem.ID;
        }

        public new IDatabase Database { get; set; }

        public ID ID { get; set; }
            
        
    }
}