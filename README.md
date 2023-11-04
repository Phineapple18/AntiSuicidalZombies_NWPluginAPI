# AntiSuicidalZombies_NWPluginAPI
Plugin AntiSuicidalZombies for NW PluginAPI. Prevent Zombies from suiciding on Tesla or by jumping into void (in Hcz106, HczTestRoom and HczArmory).
- Zombies can be blinded after walking into Tesla by setting BlindEffect bigger than 0.
- Zombies that jump into void will be teleported to one of the doors in the room, that doesn't require any keycard permission.

## Config
|Name|Type|Default value|Description|
|---|-----------------------------------|---|---|
|is_enabled|bool|true|Is plugin enabled?|
|debug|bool|false|Should Debug be enabled?|
|tesla_effect|Dictionary<string, float>|Blinded: 5 Deafened: 5|Effect(s) and duration applied to zombies after walking into tesla. Value must be higher than 0.|
