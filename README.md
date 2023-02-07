# AntiSuicidalZombies_NWPluginAPI
Plugin AntiSuicidalZombies for NW PluginAPI. Prevent Zombies from suiciding on Tesla or by jumping into void (in Hcz106, HczTestRoom and HczArmory).
- Zombies can be blinded after walking into Tesla by setting BlindEffect bigger than 0.
- Zombies that jump into void will be teleported to one of the doors in the room, that doesn't require any keycard permission.

## Config
|Name|Type|Default value|Description|
|---|---|---|---|
|is_enabled|bool|true|Is plugin enabled?|
|debug|bool|false|Should Debug be enabled?|
|blind_effect|float|5|Should zombies be blinded after walking into tesla and for how long? Set to 0 or below to disable.|
