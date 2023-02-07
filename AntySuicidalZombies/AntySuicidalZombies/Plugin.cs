using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginAPI.Core;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using PluginAPI.Events;

namespace AntySuicidalZombies
{
    public class Plugin
    {
		public static Plugin Singleton { get; private set; }

		[PluginPriority(LoadPriority.Medium)]
		[PluginEntryPoint("AntySuicidalZombies", "1.0.0", null, "AidualK")]
		public void OnLoad()
		{
			if (!PluginConfig.IsEnabled)
			{
				return;
			}
			Singleton = this;
			EventManager.RegisterEvents<EventHandlers>(this);
			var handler = PluginHandler.Get(this);
			Log.Info($"Loaded plugin {handler.PluginName} by {handler.PluginAuthor}.");
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
