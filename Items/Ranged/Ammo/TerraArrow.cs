using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged.Ammo
{
    public class TerraArrow : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Terra Arrow");
			Tooltip.SetDefault(@"Homes in on enemies
Not Consumable");
		}

		public override void SetDefaults()
		{
			item.damage = 17;
			item.ranged = true;
			item.width = 14;
			item.height = 32;            //You need to set the item consumable so that the ammo would automatically consumed
			item.knockBack = 4f;
			item.value = 30;
			item.rare = 1;
			item.shoot = mod.ProjectileType("TerraArrow");   //The projectile shoot when your weapon using this ammo
			item.shootSpeed = 1f;                  //The speed of the projectile
			item.ammo = AmmoID.Arrow;              //The ammo class this ammo belongs to.
		}

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 300);
        }

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.HolyArrow, 999);
			recipe.AddIngredient(null, "ReaperArrow", 999);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
