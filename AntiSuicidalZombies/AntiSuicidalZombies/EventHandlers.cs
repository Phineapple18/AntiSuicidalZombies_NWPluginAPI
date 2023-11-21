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
						if (ev.Target.ReferenceHub.playerEffectsController.TryGetEffect(effect.Key, out StatusEffectBase effectBase) && effect.Value > 0)
						{
							effectBase.ServerSetState(1, effect.Value);
						}
					}
					return false;
				}
				if (udh.TranslationId == DeathTranslations.Crushed.Id && (ev.Target.Room.Name is RoomName.Hcz106 or RoomName.HczArmory or RoomName.HczTestroom))
				{
					var doors = DoorVariant.DoorsByRoom[ev.Target.Room].Where(d => d.RequiredPermissions.RequiredPermissions == KeycardPermissions.None).ToList();
					var door = doors.ElementAt(RandInt.Next(doors.Count)).transform;
					var endpos = door.position + door.transform.forward.normalized + Vector3.up;
					if (!RoomIdUtils.IsWithinRoomBoundaries(ev.Target.Room, endpos))
					{
						endpos -= 2*door.transform.forward.normalized;
					}
					ev.Target.Position = endpos;
					return false;
				}
			}
			return true;
		}

		private static readonly Config config = Plugin.Singleton.PluginConfig;

		private static readonly System.Random RandInt = new System.Random();
	}
}
