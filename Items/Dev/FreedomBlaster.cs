using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Dev
{
    public class FreedomBlaster : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Freedom Blaster");
            Tooltip.SetDefault(@"A fully automatic weapon which fires plasma shots and the occasional charge shot
                                'I may perfer traditional busters, but this is pretty fun!'
                                -Tails");
        }

        public override void SetDefaults()
        {
            
        }
    }
}
