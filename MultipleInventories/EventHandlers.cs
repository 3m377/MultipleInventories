using System.Collections.Generic;
using Exiled.Events.EventArgs.Player;

namespace LargerInventory;

public class EventHandlers
{
	public void Verified(VerifiedEventArgs ev)
	{
        if (Plugin.Singleton.Inventories.ContainsKey(ev.Player)) Plugin.Singleton.Inventories.Remove(ev.Player);
        if (Plugin.Singleton.CurrentInventory.ContainsKey(ev.Player)) Plugin.Singleton.CurrentInventory.Remove(ev.Player);

        int counter = Plugin.Singleton.Config.InventoryCount;
        List<Inventory> inventories = [];
        while (counter != 0)
        {
            inventories.Add(new Inventory() { Id = counter, Items = [] });

            counter--;
        }

        Plugin.Singleton.Inventories.Add(ev.Player, inventories);
        Plugin.Singleton.CurrentInventory.Add(ev.Player, Plugin.Singleton.Inventories[ev.Player].Find(i => i.Id.Equals(1)));
    }

	public void Spawned(SpawnedEventArgs ev)
	{
        Plugin.Singleton.Inventories.Remove(ev.Player);
        Plugin.Singleton.CurrentInventory.Remove(ev.Player);

        int counter = Plugin.Singleton.Config.InventoryCount;
        List<Inventory> inventories = [];
        while (counter != 0)
        {
            inventories.Add(new Inventory() { Id = counter, Items = [] });

            counter--;
        }

        Plugin.Singleton.Inventories.Add(ev.Player, inventories);
        Plugin.Singleton.CurrentInventory.Add(ev.Player, Plugin.Singleton.Inventories[ev.Player].Find(i => i.Id.Equals(1)));
    }

    public void Dying(DyingEventArgs ev)
    {
        if (!ev.IsAllowed) return;

        List<Inventory> inventories = new(Plugin.Singleton.Inventories[ev.Player]);
        foreach (Inventory inventory in inventories)
        {
            Inventory.ChangeInventory(ev.Player, inventory);
            ev.Player.DropItems();
        }
    }

    public void ItemAdded(ItemAddedEventArgs ev)
	{
		Plugin.Singleton.CurrentInventory[ev.Player].Save(ev.Player);
	}

	public void ItemRemoved(ItemRemovedEventArgs ev)
	{
        Plugin.Singleton.CurrentInventory[ev.Player].Save(ev.Player);
    }
}