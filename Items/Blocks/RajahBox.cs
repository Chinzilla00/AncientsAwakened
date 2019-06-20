using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace AAMod.Items.Blocks
{
    public class RajahBox : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rajah Rabbit Music Box");
            Tooltip.SetDefault(@"Plays 'JUSTICE' by Spectral Aves");
        }

        public override void SetDefaults()
        {
            item.useStyle = 1;
            item.useTurn = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.autoReuse = true;
            item.consumable = true;
            item.createTile = mod.TileType("RajahBox");
            item.width = 36;
            item.height = 36;
            item.rare = 4;
            item.value = 10000;
            item.accessory = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MusicBox);
            recipe.AddIngredient(null, "Carrot", 20);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
