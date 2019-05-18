using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Rajah
{
    public class BaneOfTheBunny : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Bane Of The Bunny");
            Tooltip.SetDefault(@"Right click to use as a spear
Left click to use as a javelin");
		}

		public override void SetDefaults()
		{
            item.damage = 270;
            item.melee = true;
            item.width = 92; 
            item.height = 92;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.channel = true;
            item.useAnimation = 20;
            item.useStyle = 5;
            item.useTime = 20;
            item.knockBack = 4f;
            item.value = Item.buyPrice(1, 0, 0, 0);
            item.shoot = mod.ProjectileType("BaneS");
            item.shootSpeed = 4f;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.useTime = 15;
                item.useAnimation = 15;
                item.UseSound = SoundID.Item1;
                item.useStyle = 5;
                item.shoot = mod.ProjectileType("BaneS");  
                item.shootSpeed = 10f;
                item.autoReuse = true;
            }
            else
            {
                item.useAnimation = 13;
                item.useTime = 13;
                item.UseSound = SoundID.Item1;
                item.useStyle = 1;
                item.shoot = mod.ProjectileType("BaneT");
                item.shootSpeed = 10f;
                item.autoReuse = true;
            }
            return base.CanUseItem(player);
        }
    }
}