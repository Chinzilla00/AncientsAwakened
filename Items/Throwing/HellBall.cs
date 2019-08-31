using Terraria;
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
            item.maxStack = 5;
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
            item.noMelee = true;
        }

        public override void SetStaticDefaults()
        {
          DisplayName.SetDefault("Scorched Saw");
          Tooltip.SetDefault("");
        }

        public override bool CanUseItem(Player player)       //this make that you can shoot only 1 boomerang at once
        {
            int num = 0;
            for (int i = 0; i < 200; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].type == mod.ProjectileType("HellBallP"))
                {
                    num++;
                }
            }
            if (num > item.stack)
            {
                return false;
            }
            return true;
        }

        public override void AddRecipes()
        {                                                   
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HellstoneBar, 20);              //exeample of how to craft with a modded item
			recipe.AddIngredient(ItemID.SoulofNight, 5);
			recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
