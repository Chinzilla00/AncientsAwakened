﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Magic
{
    public class WandOfSparkingEX : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Starburst Wand");
            Tooltip.SetDefault(@"Hold to charge the wand
Wand of Sparking EX");
        }

        public override void SetDefaults()
        {
            item.mana = 8;
            item.width = 74;
            item.height = 34;
            item.magic = true;
            item.damage = 300;
            item.shoot = mod.ProjectileType("SparkWand");
            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.channel = true;
            Item.sellPrice(3, 0, 0, 0);
            item.noMelee = true;
			item.rare = ItemRarityID.Purple;
			item.shootSpeed = 12f;
			item.noUseGraphic = true;
            item.expert = true; item.expertOnly = true;
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.WandofSparking);
                recipe.AddIngredient(null, "EXSoul");
                recipe.AddTile(null, "QuantumFusionAccelerator");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}
