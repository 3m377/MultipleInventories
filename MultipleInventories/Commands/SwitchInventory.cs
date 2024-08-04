using System;
using System.Text;
using CommandSystem;
using Exiled.API.Features;
using Exiled.API.Features.Items;

namespace LargerInventory;

[CommandHandler(typeof(ClientCommandHandler))]
public class SwitchInventory : ICommand
{
    public string Command => "switchinv";
    public string[] Aliases { get; } = ["sinv", "si"];
    public string Description { get; } = $"";
    public bool SanitizeResponse { get; }

    private readonly string Usage = "Usage: .switchinv (list/invId)";

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        Player player = Player.Get((CommandSender)sender);

		switch (arguments.Count)
		{
			case < 1:
				{
					response = $"Not enough arguments! {Usage}";
					return false;
				}

			case > 1:
				{
					response = $"Too many arguments! {Usage}";
					return false;
				}
		}

		if (arguments.At(0) == "list" || arguments.At(0) == "l")
		{
			StringBuilder sb = new();
			foreach (Inventory inventory in Plugin.Singleton.Inventories[player])
			{
				if (Plugin.Singleton.CurrentInventory[player] == inventory)
				{
					sb.Append($"[SELECTED] Inventory {inventory.Id}:\n");
				} else
				{
					sb.Append($"Inventory {inventory.Id}:\n");
				}

				int count = 0;

				foreach (Item item in inventory.Items)
				{
					count++;

					if (count != inventory.Items.Count)
					{
						sb.Append($"{item.Type}, ");
					} else
					{
						sb.Append($"{item.Type}\n");
					}
				}
			}
			response = $"Inventory list:\n{sb}";
			return true;
		} else if (int.TryParse(arguments.At(0), out _))
		{
			Inventory inventory = Plugin.Singleton.Inventories[player].Find(i => i.Id.Equals(int.Parse(arguments.At(0))));
			if (inventory == null)
			{
				response = $"Invalid inventory ID {arguments.At(0)}";
				return false;
			} else
			{
				Inventory.ChangeInventory(player, inventory);
				response = $"Set current inventory to {inventory.Id}";
				return true;
			}
		} else
		{
			response = $"Invalid argument! {Usage}";
			return false;
		}
	}
}