using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Throwing
{
    public class Spookerang : BaseAAItem
    {

        public override void SetDefaults()
        {
			item.useTime = 25;
            item.CloneDefaults(ItemID.PossessedHatchet);
            item.melee = true;
            item.damage = 140;                            
            item.value = 20;
            item.rare = 3;
            item.knockBack = 2;
            item.useStyle = 1;
            item.useAnimation = 19;
            item.useTime = 19;
            item.shoot = mod.ProjectileType("SpookerangP");
			item.width = 54;
            item.height = 54;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Spooky Sawblade");
      Tooltip.SetDefault("A posessed chakram than homes in on enemies because it's possessed by a spooky ghost");
    }


        public override void AddRecipes()
        {                                                   //How to craft this item
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 10);              //exeample of how to craft with a modded item
			recipe.AddIngredient(ItemID.SpookyWood, 50);
			recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
