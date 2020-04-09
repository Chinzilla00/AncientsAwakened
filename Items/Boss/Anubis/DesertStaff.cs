using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Anubis
{
    public class DesertStaff : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Desert Staff");
			Tooltip.SetDefault("Shoots enchanted sand bolt which explodes into bouncing balls on hit");
			Item.staff[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.damage = 60;
			item.magic = true;
			item.mana = 10;
			item.width = 76;
			item.height = 76;
			item.useTime = 25;
			item.useAnimation = 25;
			item.useStyle = 5;
			item.noMelee = true;
			item.knockBack = 5;
			item.value = 10000;
			item.rare = 6;
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("DesertBlast");
			item.shootSpeed = 12f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.AmberStaff, 1);
			recipe.AddIngredient(null, "ForsakenFragment", 5);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}