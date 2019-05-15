using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class Scalpel : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 15;
            item.melee = true;
            item.width = 64;
            item.height = 64;

            item.useTime = 19;
            item.useAnimation = 19;
            item.pick = 110;
            item.useStyle = 1;
            item.knockBack = 1;
            item.value = 1000;
            item.rare = 2;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scalpel");
        }

        public override void AddRecipes()  //How to craft this item
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DeathbringerPickaxe);
            recipe.AddIngredient(mod, "Grasscutter");
            recipe.AddIngredient(mod, "Toothpick");
            recipe.AddIngredient(ItemID.MoltenPickaxe);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);  
            recipe.AddRecipe();
        }
    }
}
