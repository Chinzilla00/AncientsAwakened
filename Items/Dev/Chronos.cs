using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace AAMod.Items.Dev
{
    public class Chronos : ModItem
    {
        public override void SetDefaults()
        {
            item.useTime = 25;
            item.CloneDefaults(ItemID.Terrarian);

            item.damage = 350;
            item.value = 1000000;
            item.rare = 11;
            item.knockBack = 1;
            item.channel = true;
            item.useStyle = 5;
            item.useAnimation = 18;
            item.useTime = 18;
            item.shoot = mod.ProjectileType("Chronos");
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.Chilled, 1000);
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chronos");
            Tooltip.SetDefault("Time Teller EX \n" +
                "Time is big ball of wibbly-wobbly timey-wimey yo-yos. \n" +
                "-Dallin");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(181, 38, 38);
                }
            }
        }

        public override void UpdateInventory(Player player)
        {
            if (player.accWatch < 3)
                player.accWatch = 3;
        }
		
        public override void AddRecipes()
        {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "TimeTeller");
			recipe.AddIngredient(null, "EXSoul");
			recipe.SetResult(this);
			recipe.AddRecipe();
        }
    }
}
