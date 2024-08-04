using System.Collections.Generic;
using Exiled.API.Features;
using Exiled.API.Features.Items;

namespace LargerInventory;

public class Inventory
{
    public int Id { get; set; }

    public List<Item> Items { get; set; }

    public void Save(Player player)
    {
        Items.Clear();

        foreach (Item item in player.Items)
        {
            Items.Add(item);
        }
    }

    public static void ChangeInventory(Player player, Inventory inventory)
    {
        Plugin.Singleton.CurrentInventory[player].Save(player);

        Plugin.Singleton.CurrentInventory[player] = inventory;

        player.ClearItems();

        List<Item> itemsCopy = new(inventory.Items);

        foreach (Item item in itemsCopy)
        {
            player.AddItem(item.Type);
        }
    }
}