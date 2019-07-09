using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace AAMod.Items.Tools
{
    public class Mushpick : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.damage = 7;
            item.melee = true;
            item.width = 32;
            item.height = 32;
            item.useTime = 12;
            item.useAnimation = 21;
            item.pick = 50;
            item.useStyle = 1;
            item.knockBack = 1;
            item.value = Item.sellPrice(0, 0, 10, 0);
            item.rare = 1;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mushpick");
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
