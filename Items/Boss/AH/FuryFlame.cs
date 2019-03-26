using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace AAMod.Items.Boss.AH
{
    public class FuryFlame : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fury Flame");
            Tooltip.SetDefault("Allows you to blast explosive flames at your foes");
        }

        public override void SetDefaults()
        {
            item.damage = 80;
            item.noMelee = true;
            item.ranged = true;
            item.width = 64;
            item.height = 46;
            item.useTime = 2;
            item.useAnimation = 15;
            item.useStyle = 5;
            item.shoot = mod.ProjectileType("FuryFlame");
            item.knockBack = 0;
            item.value = Item.buyPrice( 1, 0, 0, 0);
            item.rare = 11;
            item.UseSound = SoundID.Item20;
            item.autoReuse = true;
            item.shootSpeed = 7f;
            item.noUseGraphic = true;
        }


        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
    }
}
