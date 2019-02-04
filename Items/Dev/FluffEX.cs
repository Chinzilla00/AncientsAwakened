using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Dev
{
    public class FluffEX : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Puff");
            Tooltip.SetDefault(@"Rapidly throws hairballs that slow your opponents
Fluff EX");
        }

        public override void SetDefaults()
        {
            item.damage = 220;
            item.magic = true;
            item.width = 32;
            item.height = 32;
            item.noUseGraphic = true;
            item.useTime = 15;
            item.useAnimation = 15;
            item.shoot = mod.ProjectileType("FluffEX");
            item.shootSpeed = 12f;
            item.useStyle = 5;
            item.knockBack = 0;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = 8;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.expert = true;
        }
        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "Fluff");
                recipe.AddIngredient(null, "EXSoul");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}