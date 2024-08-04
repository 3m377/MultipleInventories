using System;
using Exiled.API.Features;
using Player = Exiled.Events.Handlers.Player;
using System.Collections.Generic;

namespace LargerInventory;

public class Plugin : Plugin<Config>
{
    public override string Name => "MultipleInventories";
    public override string Author => "3m377";
    public override string Prefix => "multi_inventory";
    public override Version Version { get; } = new Version(1, 0, 0);
    public override Version RequiredExiledVersion { get; } = new Version(8, 11, 0);

    public EventHandlers EventHandler;
    public static Plugin Singleton;

	public IDictionary<Exiled.API.Features.Player, List<Inventory>> Inventories = new Dictionary<Exiled.API.Features.Player, List<Inventory>>();
    public IDictionary<Exiled.API.Features.Player, Inventory> CurrentInventory = new Dictionary<Exiled.API.Features.Player, Inventory>();

    public override void OnEnabled()
    {
        Singleton = this;
        EventHandler = new EventHandlers();
        Player.Verified += EventHandler.Verified;
        Player.Spawned += EventHandler.Spawned;
        Player.Dying += EventHandler.Dying;
        Player.ItemAdded += EventHandler.ItemAdded;
        Player.DroppedItem += EventHandler.DroppedItem;
    }

    public override void OnDisabled()
    {
        Player.DroppedItem -= EventHandler.DroppedItem;
        Player.ItemAdded -= EventHandler.ItemAdded;
        Player.Dying -= EventHandler.Dying;
        Player.Spawned -= EventHandler.Spawned;
        Player.Verified -= EventHandler.Verified;
        EventHandler = null;
        Singleton = null;
    }
}