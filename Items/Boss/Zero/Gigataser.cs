using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Zero
{
    public class Gigataser : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gigataser");
            Tooltip.SetDefault(@"Fires void lightning
Hold to charge the Taser
the longer the taser is charged, the more it penetrates");
        }

        public override void SetDefaults()
        {
            item.noUseGraphic = true;
            item.damage = 400;
            item.noMelee = true;
            item.ranged = true;
            item.width = 74;
            item.height = 24;
            item.useTime = 65;
            item.useAnimation = 65; 
            item.useStyle = 5; 
            item.shoot = mod.ProjectileType("Gigatazer");
            item.channel = true;
            item.knockBack = 12;
            item.value = Item.sellPrice(0, 30, 0, 0);
            item.rare = 9;
            item.shootSpeed = 8f;
            item.crit += 5;
            item.rare = 9;
            AARarity = 13;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ApocalyptitePlate", 5);
            recipe.AddIngredient(null, "UnstableSingularity", 5);
            recipe.AddIngredient(null, "FulguriteTazerblaster");
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}