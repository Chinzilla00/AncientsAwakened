using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class Nightaxe : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 15;
            item.melee = true;
            item.width = 64;
            item.height = 64;

            item.useTime = 18;
            item.useAnimation = 18;
            item.pick = 110;
            item.useStyle = 1;
            item.knockBack = 1;
            item.value = 10;
            item.rare = 4;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nightaxe");
        }

        public override void AddRecipes()  //How to craft this item
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.NightmarePickaxe);
            recipe.AddIngredient(mod, "Grasscutter");
            recipe.AddIngredient(mod, "Toothpick");
            recipe.AddIngredient(ItemID.MoltenPickaxe);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);  
            recipe.AddRecipe();
        }
    }
}
