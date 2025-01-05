using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CustomPlayerEffects;
using Interactables.Interobjects;
using Interactables.Interobjects.DoorUtils;
using MapGeneration;
using PlayerRoles;
using PlayerStatsSystem;
using PluginAPI.Core;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using PluginAPI.Events;
using UnityEngine;

namespace AntiSuicidalZombies
{
    internal class EventHandlers
    {
        [PluginEvent(ServerEventType.PlayerDamage)]
		internal bool OnPlayerDamage(PlayerDamageEvent ev)
		{
			if (ev.Target.Role == RoleTypeId.Scp0492 && ev.DamageHandler is UniversalDamageHandler udh)
			{
                if (udh.TranslationId == DeathTranslations.Tesla.Id && config.TeslaEffects != null)
                {
                    foreach (var effect in config.TeslaEffects)
                    {
                        if (ev.Target.EffectsManager.TryGetEffect(effect.Key, out StatusEffectBase effectBase))
                        {
                            effectBase.ServerSetState(effect.Value.Intensity, effect.Value.Duration);
                        }
                    }
                    Log.Debug($"Enabled effects for player {ev.Target.Nickname} after walking into a tesla gate.", config.Debug, pluginName);
                    return false;
                }
                if (udh.TranslationId == DeathTranslations.Crushed.Id)
                {
                    if (udh.Damage == 200f && config.CrushedEffects != null)
                    {
                        foreach (var effect in config.CrushedEffects)
                        {
                            if (ev.Target.EffectsManager.TryGetEffect(effect.Key, out StatusEffectBase effectBase))
                            {
                                effectBase.ServerSetState(effect.Value.Intensity, effect.Value.Duration);
                            }
                        }
                        Log.Debug($"Enabled effects for player {ev.Target.Nickname} after being crushed by a bulk door.", config.Debug, pluginName);
                        return false;
                    }
                    Vector3 position;
                    if (ev.Target.Room == null)
                    {
                        position = AlphaWarheadNukesitePanel.Singleton.transform.position + 3 * Vector3.up;
                    }
                    else if (ev.Target.Room.Name == RoomName.HczWarhead)
                    {
                        if (config.CrushedEffects != null)
                        {
                            foreach (var effect in config.CrushedEffects)
                            {
                                if (ev.Target.EffectsManager.TryGetEffect(effect.Key, out StatusEffectBase effectBase))
                                {
                                    effectBase.ServerSetState(effect.Value.Intensity, effect.Value.Duration);
                                }
                            }
                        }
                        ElevatorChamber.TryGetChamber(ElevatorGroup.Nuke01, out ElevatorChamber chamber1);
                        ElevatorChamber.TryGetChamber(ElevatorGroup.Nuke02, out ElevatorChamber chamber2);
                        Vector3 chamber1Position = chamber1.transform.position;
                        Vector3 chamber2Position = chamber2.transform.position;
                        position = Vector3.Distance(ev.Target.Position, chamber1Position) < Vector3.Distance(ev.Target.Position, chamber2Position) ? chamber1Position : chamber2Position;
                        position += 2 * Vector3.up;
                    }
                    else
                    {
                        IEnumerable<DoorVariant> doors = DoorVariant.DoorsByRoom[ev.Target.Room].Where(d => d.RequiredPermissions.RequiredPermissions == KeycardPermissions.None);
                        Transform door = doors.ElementAt(RandInt.Next(doors.Count())).transform;
                        position = door.position + door.forward.normalized + Vector3.up;
                        if (!RoomIdUtils.IsWithinRoomBoundaries(ev.Target.Room, position))
                        {
                            position -= 2 * door.forward.normalized;
                        }
                    }
                    ev.Target.EffectsManager.DisableEffect<PitDeath>();
                    ev.Target.Position = position;
                    Log.Debug($"Teleported player {ev.Target.Nickname} to safe position after jumping into a void or being crushed by an elevator.", config.Debug, pluginName);
                    return false;
                }
            }
			return true;
		}

		private static readonly System.Random RandInt = new();

        private static readonly Config config = Plugin.Singleton.pluginConfig;

        private static readonly string pluginName = Plugin.Singleton.pluginHandler.PluginName;
    }
}
