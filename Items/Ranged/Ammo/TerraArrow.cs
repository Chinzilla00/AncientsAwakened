using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged.Ammo
{
    public class TerraArrow : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Terra Arrow");
			Tooltip.SetDefault(@"Homes in on enemies
Not Consumable");
		}

		public override void SetDefaults()
		{
			item.damage = 12;
			item.ranged = true;
			item.width = 14;
			item.height = 32;
			item.knockBack = 4f;
			item.value = 30;
			item.rare = ItemRarityID.Blue;
			item.shoot = mod.ProjectileType("TerraArrow");
            item.shootSpeed = 1f;
			item.ammo = AmmoID.Arrow;
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
