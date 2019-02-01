using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
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
                "- Tails\n" +
                "Spur EX");
        }

        public override void SetDefaults()
        {
            item.width = 58;
            item.height = 40;
            item.ranged = true;
            item.damage = 400;
            item.scale = .65f;
            item.shoot = mod.ProjectileType("PlasmaShot");
            item.shootSpeed = 30f;
            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = 5;
            Item.sellPrice(1, 0, 0, 0);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2 (3, 1);
        }
    }
}
