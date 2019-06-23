using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Throwing
{
	public class OrderDisc : BaseAAItem
	{
		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.LightDisc);
			item.melee = true;
			item.shootSpeed = 16f;
			item.stack = 1;
			item.useTime = 12;
			item.damage = 75;                            
			item.value = 20;
			item.rare = 5;
			item.knockBack = 4;
			item.useStyle = 1;
			item.useAnimation = 12;
			item.shoot = mod.ProjectileType("OrderDiscP");
			item.width = 46;
			item.height = 46;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Order Disc");
			Tooltip.SetDefault("Ignores enemy defense");
		}

        public override bool CanUseItem(Player player)       //this make that you can shoot only 1 boomerang at once
        {
            int num16 = 0;
            for (int num17 = 0; num17 < 1000; num17++)
            {
                if (Main.projectile[num17].active && Main.projectile[num17].owner == Main.myPlayer && Main.projectile[num17].type == item.shoot)
                {
                    num16++;
                }
            }
            if (num16 >= item.stack)
            {
                return false;
            }
            return true;
        }

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("OrderBar"), 15);
			recipe.AddIngredient(ItemID.Ectoplasm, 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
