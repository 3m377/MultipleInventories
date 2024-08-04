using System.ComponentModel;
using Exiled.API.Interfaces;

namespace LargerInventory;

public class Config : IConfig
{
    [Description("Whether or not the plugin is enabled.")]
    public bool IsEnabled { get; set; } = true;

    [Description("Enable debug logs?")]
    public bool Debug { get; set; } = false;

	[Description("How many inventories should each player have?")]
	public int InventoryCount { get; set; } = 3;
}