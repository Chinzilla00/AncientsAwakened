using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace AAMod.Projectiles
{
    public class AkumaTooth : ShenTooth
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Akuma Tooth");
        }
    }
}
