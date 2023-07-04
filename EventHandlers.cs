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
using PluginAPI.Core;
using PluginAPI.Enums;
using PluginAPI.Events;
using PluginAPI.Core.Zones;

namespace AntiSuicidalZombies
{
    public class EventHandlers
    {
		[PluginEvent(ServerEventType.PlayerDamage)]
		public bool OnPlayerDamage(PlayerDamageEvent ev)
		{
			if (ev.Target.Role == RoleTypeId.Scp0492 && ev.Player == null)
			{
				UniversalDamageHandler uDH = (UniversalDamageHandler)ev.DamageHandler;
				if (uDH.TranslationId == 13)
				{
					if (config.BlindEffect > 0)
					{
						ev.Target.EffectsManager.EnableEffect<Blinded>(config.BlindEffect, true);
					}
					return false;
				}
				if (uDH.TranslationId == 20 && (ev.Target.Room.Name == RoomName.Hcz106 || ev.Target.Room.Name == RoomName.HczArmory || ev.Target.Room.Name == RoomName.HczTestroom))
				{
					var roomdoors = DoorVariant.DoorsByRoom[ev.Target.Room].Where(d => d.RequiredPermissions.RequiredPermissions == KeycardPermissions.None).ToList();
					var doorpos = roomdoors.ElementAt(RandInt.Next(roomdoors.Count)).transform.position;
					doorpos += Vector3.forward * 0.2f;
					if (!RoomIdUtils.IsWithinRoomBoundaries(ev.Target.Room, doorpos))
                    {
						doorpos += Vector3.back * 0.2f;
					}
					doorpos += Vector3.up;
					ev.Target.Position = doorpos;
					return false;
				}
			}
			return true;
		}

		private static readonly Config config = Plugin.Singleton.PluginConfig;

		private static readonly System.Random RandInt = new System.Random();
	}
}
