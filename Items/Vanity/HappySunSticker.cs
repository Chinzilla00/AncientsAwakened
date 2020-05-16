using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity
{
    public class HappySunSticker : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 24;
            item.rare = ItemRarityID.Orange;
            item.accessory = true;
            item.vanity = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Happy Sun Sticker");
            Tooltip.SetDefault(@":D");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Sunglasses);
            recipe.AddIngredient(ItemID.SunplateBlock, 5);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}