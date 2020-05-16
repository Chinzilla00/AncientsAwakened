using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace AAMod.Items.Tools
{
    public class GlowMushpick : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.damage = 7;
            item.melee = true;
            item.width = 32;
            item.height = 32;
            item.useTime = 10;
            item.useAnimation = 19;
            item.pick = 55;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 1;
            item.value = Item.sellPrice(0, 0, 10, 0);
            item.rare = ItemRarityID.Blue;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Glowing Mushpick");
        }

        public override void AddRecipes()  
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.GlowingMushroom, 5);
            recipe.AddIngredient(null, "GlowingMushiumBar", 3);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);  
            recipe.AddRecipe();
        }
    }
}
