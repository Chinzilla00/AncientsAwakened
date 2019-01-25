using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Dev
{
    public class FreedomStar : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Freedom Star");
            Tooltip.SetDefault("Tails' trusty blaster.\n" +
                "Hold the use button to charge, and then release a powerful plasma beam!\n" +
                "\"You mess with me or my friends, and your ass is Stardust.\" \n" +
                "- Tails");
        }

        public override void SetDefaults()
        {
            item.width = 58;
            item.height = 40;
            item.ranged = true;
            item.damage = 500;
            item.shoot = mod.ProjectileType("PlasmaShot");
            item.useTime = 10;
        }
    }
}
