# AntiSuicidalZombies_NWPluginAPI
Plugin for SCP-SL game, that prevents Zombies from suiciding on Tesla or by jumping into void.

## Features
- Zombies can have effect(s) applied to them after walking into Tesla.
- Zombies, who jumped into void, are teleported to one of the doors with no keycard permission in the same room. The affected rooms are: in Hcz106, HczTestRoom and HczArmory.

## Config
|Name|Type|Default value|Description|
|---|---|---|---|
|is_enabled|bool|true|Is plugin enabled?|
|debug|bool|false|Should Debug be enabled?|
|tesla_effect|Dictionary<string, float>|Blinded: 5 Deafened: 5|Effect(s) and duration applied to zombies after walking into tesla. Value must be higher than 0.|
