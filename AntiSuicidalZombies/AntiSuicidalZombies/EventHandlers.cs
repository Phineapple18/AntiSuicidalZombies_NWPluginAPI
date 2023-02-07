using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginAPI.Core.Attributes;
using PluginAPI.Core;
using PluginAPI.Enums;
using CustomPlayerEffects;
using Interactables.Interobjects.DoorUtils;
using PlayerRoles;
using PlayerStatsSystem;
using MapGeneration;
using UnityEngine;

namespace AntiSuicidalZombies
{
    public class EventHandlers
    {
		[PluginEvent(ServerEventType.PlayerDamage)]
		public bool OnPlayerDamage(Player player, Player attacker, DamageHandlerBase damageHandler)
		{
			if (player.Role == RoleTypeId.Scp0492)
			{
				UniversalDamageHandler uDH = (UniversalDamageHandler)damageHandler;
				if (uDH.TranslationId == 13)
				{
					if (config.BlindEffect > 0)
					{
						player.EffectsManager.EnableEffect<Blinded>(config.BlindEffect, true);
					}
					return false;
				}
				if (uDH.TranslationId == 20 && (player.Room.Name == RoomName.Hcz106 || player.Room.Name == RoomName.HczArmory || player.Room.Name == RoomName.HczTestroom))
				{
					var roomdoors = DoorVariant.DoorsByRoom[player.Room].Where(d => d.RequiredPermissions.RequiredPermissions == KeycardPermissions.None).ToList();
					var doorpos = roomdoors.ElementAt(RandInt.Next(roomdoors.Count)).transform.position;
					doorpos += Vector3.forward * 0.2f;
					doorpos += Vector3.up;
					player.Position = doorpos;
					return false;
				}
			}
			return true;
		}

		private static Config config = Plugin.Singleton.PluginConfig;

		private static readonly System.Random RandInt = new System.Random();
	}
}
