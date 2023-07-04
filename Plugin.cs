using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginAPI.Core;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using PluginAPI.Events;

namespace AntiSuicidalZombies
{
    public class Plugin
    {
		public static Plugin Singleton { get; private set; }

		[PluginPriority(LoadPriority.Medium)]
		[PluginEntryPoint("AntiSuicidalZombies", "1.0.2", null, "CarverGoofballJunior")]
		public void OnLoad()
		{
			if (!PluginConfig.IsEnabled)
			{
				return;
			}
			Singleton = this;
			EventManager.RegisterEvents<EventHandlers>(this);
			var handler = PluginHandler.Get(this);
			Log.Info($"Loaded plugin {handler.PluginName} by {handler.PluginAuthor}.", handler.PluginName);
		}

		[PluginUnload]
		public void OnUnload()
		{
			EventManager.UnregisterEvents<EventHandlers>(this);
			Singleton = null;
		}

		[PluginConfig] public Config PluginConfig;
	}
}
