using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace AntySuicidalZombies
{
    public class Config
    {
        [Description("Is plugin enabled?")]
        public bool IsEnabled { get; set; } = true;

        [Description("Should Debug be enabled?")]
        public bool Debug { get; set; } = false;

        [Description("Should zombies be blinded after walking into tesla and for how long? Set to 0 or below to disable.")]
        public float BlindEffect { get; set; } = 5f;
    }
}
