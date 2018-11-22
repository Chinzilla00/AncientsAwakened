using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class NightSkyYoyo : ModItem
    {

        public override void SetDefaults()
        {
			item.useTime = 1000;
            item.CloneDefaults(ItemID.Chik);

            item.damage = 43;                            
            item.value = 10;
            item.rare = 3;
            item.knockBack = 1;
            item.channel = true;
            item.useStyle = 5;
            item.useAnimation = 25;
            item.useTime = 25;
            item.shoot = mod.ProjectileType("NightSkyYoyoProjectile");           
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Night Sky Yoyo");
      Tooltip.SetDefault("");
    }


        public override void AddRecipes()
        {                                                   //How to craft this item
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.CorruptYoyo, 1);              //exeample of how to craft with a modded item
			recipe.AddIngredient(ItemID.JungleYoyo, 1);
			recipe.AddIngredient(ItemID.Valor, 1);
			recipe.AddIngredient(ItemID.Cascade, 1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
