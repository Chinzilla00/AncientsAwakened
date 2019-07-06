using BaseMod;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.MushroomMonarch
{
    public class GlowingMushiumBar : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 24;
            item.maxStack = 99;
            item.rare = 1;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.rare = 1;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.createTile = mod.TileType("GlowingMushiumBar");
            item.value = Terraria.Item.sellPrice(0, 0, 9, 0);
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Glowing Mushium Bar");
            Tooltip.SetDefault("Glowy");
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return BaseUtility.MultiLerpColor((Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.White, lightColor, lightColor, Color.White);
        }

        public override void AddRecipes()
        {                                                   
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "GlowingMushium", 3);              //example of how to craft with a modded item
            recipe.AddTile(TileID.Furnaces);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
