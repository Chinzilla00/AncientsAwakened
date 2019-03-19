﻿using System;
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
            DisplayName.SetDefault("Freedom Star");
            Tooltip.SetDefault("Tails' trusty blaster.\n" +
                "Hold the use button to charge, and then release a powerful Freedom Blast!\n" +
                "\"You mess with me or my friends, and your ass is Stardust.\" \n" +
                "- Tails\n" +
                "Mobian Buster EX");
        }

        public override void SetDefaults()
        {
            item.width = 40;
            item.height = 32;
            item.ranged = true;
            item.damage = 400;
            item.shoot = mod.ProjectileType("FreedomStar");
            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = 5;
            item.channel = true;
            Item.sellPrice(1, 0, 0, 0);
            item.noMelee = true;
            item.noUseGraphic = true;
        }
    }
}
