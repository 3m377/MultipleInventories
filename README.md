# MultipleInventories
An SCP:SL EXILED plugin that gives players more than one inventory.

## Config
```yaml
multi_inventory:
# Whether or not the plugin is enabled.
  is_enabled: true
  # Enable debug logs?
  debug: false
  # How many inventories should each player have?
  inventory_count: 3
```

## How to use
Using the command `.switchinv (list/invId)` you can list what inventories you have or switch to an available inventory.

## Known issues
- Ammo is not stored between inventories.
