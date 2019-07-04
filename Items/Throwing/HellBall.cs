using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Throwing
{
    public class HellBall : BaseAAItem
    {

        public override void SetDefaults()
        {
			item.useTime = 25;
            item.CloneDefaults(ItemID.LightDisc);
            item.melee = true;
            item.damage = 42;                            
            item.value = 6;
            item.rare = 5;
            item.knockBack = 5;
            item.useStyle = 1;
            item.useAnimation = 24;
            item.useTime = 24;
            item.shoot = mod.ProjectileType("HellBallP");
			item.width = 56;
            item.height = 56;
        }

        public override void SetStaticDefaults()
        {
          DisplayName.SetDefault("Scorched Saw");
          Tooltip.SetDefault("");
        }

        public override bool CanUseItem(Player player)       //this make that you can shoot only 1 boomerang at once
        {
            if (AAGlobalProjectile.CountProjectiles(mod.ProjectileType("HellBallP")) > 5)
            {
                return false;
            }
            return true;
        }

        public override void AddRecipes()
        {                                                   //How to craft this item
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HellstoneBar, 20);              //exeample of how to craft with a modded item
			recipe.AddIngredient(ItemID.SoulofNight, 5);
			recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
