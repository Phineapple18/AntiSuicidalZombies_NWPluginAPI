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
		[PluginPriority(LoadPriority.Medium)]
		[PluginEntryPoint("AntiSuicidalZombies", "2.0.0", null, "Phineapple18")]
		public void OnLoad()
		{
			if (!pluginConfig.IsEnabled)
			{
                Log.Warning("Plugin AntiSuicidalZombies is disabled.", "AntiSuicidalZombies");
                return;
			}
			Singleton = this;
            pluginHandler = PluginHandler.Get(this);
            EventManager.RegisterEvents<EventHandlers>(this);
            Log.Info($"Loaded plugin {pluginHandler.PluginName} by {pluginHandler.PluginAuthor}.", pluginHandler.PluginName);
		}

        public PluginHandler pluginHandler;

        [PluginConfig] public Config pluginConfig;

        public static Plugin Singleton { get; private set; }
    }
}
