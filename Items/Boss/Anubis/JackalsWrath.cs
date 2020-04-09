using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Anubis
{
    public class JackalsWrath : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Jackal's Wrath");
            Tooltip.SetDefault("Shoots out a wall-piercing returning phantom blade on swing");
        }

		public override void SetDefaults()
		{
			item.autoReuse = true;
			item.useStyle = 1;
			item.useAnimation = 20;
			item.useTime = 20;
			item.knockBack = 5f;
			item.width = 24;
			item.height = 28;
			item.damage = 30;
			item.UseSound = SoundID.Item71;
			item.rare = 6;
			item.shoot = mod.ProjectileType("PhantomBlade");
			item.shootSpeed = 14f;
			item.value = 10000;
			item.melee = true;
            item.noMelee = true;
            item.noUseGraphic = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.GoldAxe, 1);
			recipe.AddIngredient(null, "ForsakenFragment", 5);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe(); 
			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.PlatinumAxe, 1);
			recipe.AddIngredient(null, "ForsakenFragment", 5);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
