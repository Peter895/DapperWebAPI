using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
///  <param name="TP">Parent Entity</param>
///  <param name="TC">Child Entity</param>
///  <param name="TPK">The Id field  PK to FK relationship</param>
///  <decsription>This generic entity mapping parent child relationship</decsription>
///  <Methods>
///     <Method name="AddChildAction" >Add child entity to parent</Method>
///     <Method name="Map" >Build parent - child relationship</Method>
///     <Method name="ParentKey">define the key field for the relationship</Method>
///  </Methods>
///  <Example>
///         var mapper = new EntityOneToManyMapper<Store, Register, string>()
///           {
///               AddChildAction = (c, o) =>
///                {
///                    if (c.Registers == null)
///                        c.Registers = new List<Register>();
///
///                    c.Registers.Add(new Register(Context.ConnectionId, groupName));
///                },
///                ParentKey = (c) => c.storeName
///            };
///
///            var tmpStore = new Store()
///            {
///                storeName = groupName
///            };
///
///         var tmpRegister = new Register(Context.ConnectionId, groupName);
///        Stores.Add(mapper.Map(tmpStore, tmpRegister));
///  </Example>
/// </summary>
namespace DapperWebAPI.Services
{
    public class EntityOneToManyMapper<TP, TC, TPk>
    {
        private readonly IDictionary<TPk, TP> _lookup = new Dictionary<TPk, TP>();

        public Action<TP, TC> AddChildAction { get; set; }

        public Func<TP, TPk> ParentKey { get; set; }


        public virtual TP Map(TP parent, TC child)
        {
            TP entity;
            var found = true;
            var primaryKey = ParentKey(parent);

            if (!_lookup.TryGetValue(primaryKey, out entity))
            {
                _lookup.Add(primaryKey, parent);
                entity = parent;
                found = false;
            }

            AddChildAction(entity, child);

            return !found ? entity : default(TP);

        }
    }
}
