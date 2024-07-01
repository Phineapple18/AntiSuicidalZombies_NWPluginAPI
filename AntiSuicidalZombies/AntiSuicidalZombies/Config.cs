using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;

namespace AntiSuicidalZombies
{
    public class Config
    {

        [Description("Should plugin be enabled?")]
        public bool IsEnabled { get; set; } = true;

        [Description("Should Debug be enabled?")]
        public bool Debug { get; set; } = false;

        [Description("Effect(s) and duration applied to zombies after walking into tesla. Value must be higher than 0.")]
        public Dictionary<string, float> TeslaEffect { get; set; } = new Dictionary<string, float>
        {
            { "Blinded", 5f},
            { "Deafened", 5f }
        };
    }
}
