using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class Grasscutter : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 5;
            item.melee = true;
            item.width = 32;
            item.height = 32;

            item.useTime = 22;
            item.useAnimation = 22;
            item.pick = 60;
            item.useStyle = 1;
            item.knockBack = 1;
            item.value = 10;
            item.rare = 2;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Grasscutter");
        }

        public override void AddRecipes()  //How to craft this item
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Vine, 2);
            recipe.AddIngredient(ItemID.Stinger, 2);
            recipe.AddIngredient(ItemID.JungleSpores, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);  
            recipe.AddRecipe();
        }
    }
}
