using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CustomPlayerEffects;
using Interactables.Interobjects.DoorUtils;
using MapGeneration;
using PlayerRoles;
using PlayerStatsSystem;
using UnityEngine;

using PluginAPI.Core;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using PluginAPI.Events;

namespace AntiSuicidalZombies
{
    public class EventHandlers
    {
		[PluginEvent(ServerEventType.PlayerDamage)]
		public bool OnPlayerDamage(PlayerDamageEvent ev)
		{
			if (ev.Target.Role == RoleTypeId.Scp0492 && ev.DamageHandler is UniversalDamageHandler udh)
			{
				if (udh.TranslationId == DeathTranslations.Tesla.Id && !config.TeslaEffect.IsEmpty())
				{
					foreach (var effect in config.TeslaEffect)
                    {
						if (ev.Target.EffectsManager.TryGetEffect(effect.Key, out StatusEffectBase effectBase) && effect.Value > 0)
                        {
                            effectBase.ServerSetState(1, effect.Value);
                        }
                    }
					return false;
				}
				if (udh.TranslationId == DeathTranslations.Crushed.Id && (ev.Target.Room.Name is RoomName.Hcz106 or RoomName.HczArmory or RoomName.HczTestroom))
				{
                    IEnumerable<DoorVariant> doors = DoorVariant.DoorsByRoom[ev.Target.Room].Where(d => d.RequiredPermissions.RequiredPermissions == KeycardPermissions.None);
					Transform door = doors.ElementAt(RandInt.Next(doors.Count())).transform;
                    Vector3 position = door.position + door.forward.normalized + Vector3.up;
                    if (!RoomIdUtils.IsWithinRoomBoundaries(ev.Target.Room, position))
                    {
                        position -= 2 * door.forward.normalized;
                    }
					ev.Target.Position = position;
					Log.Debug($"Teleported player {ev.Target.Nickname} to safe position after jumping into void.", config.Debug, Plugin.Singleton.pluginHandler.PluginName);
					return false;
				}
			}
			return true;
		}

		private static readonly System.Random RandInt = new();

        private static readonly Config config = Plugin.Singleton.pluginConfig;
    }
}
