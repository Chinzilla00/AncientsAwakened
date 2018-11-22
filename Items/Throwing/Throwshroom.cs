using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Throwing
{
    public class Throwshroom : ModItem
    {

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.Shuriken);
            item.maxStack = 30;
            item.damage = 15;                            
            item.value = 20;
            item.rare = 1;
            item.knockBack = 2;
            item.useStyle = 1;
            item.useAnimation = 19;
            item.useTime = 19;
            item.shoot = mod.ProjectileType("Throwshroom");
			item.width = 54;
            item.height = 54;
            item.autoReuse = false;
            item.noUseGraphic = true;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Throwshroom");
    }


        public override void AddRecipes()
        {                                                   //How to craft this item
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "MushiumBar", 2);
			recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
