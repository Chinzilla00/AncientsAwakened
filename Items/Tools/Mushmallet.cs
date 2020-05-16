using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace AAMod.Items.Tools
{
    public class Mushmallet : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.damage = 10;
            item.melee = true;
            item.width = 40;
            item.height = 30;
            item.useTime = 22;
            item.useAnimation = 30;
            item.hammer = 50;
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
            DisplayName.SetDefault("Mushmallet");
        }

        public override void AddRecipes()  
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Mushroom, 5);
            recipe.AddIngredient(null, "MushiumBar", 3);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);  
            recipe.AddRecipe();
        }
    }
}
