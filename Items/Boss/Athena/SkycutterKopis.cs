using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Athena
{
    public class SkycutterKopis : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Skycutter Kopis");
			Tooltip.SetDefault("");
        }
        public override void SetDefaults()
		{
			item.damage = 70;
			item.melee = true;
			item.width = 40;
			item.height = 50;
            item.useTime = 26;
			item.useAnimation = 26;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 4;
			item.value = Item.sellPrice(gold: 1);
			item.rare = ItemRarityID.Yellow;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
            item.shoot = mod.ProjectileType("Skyblade");
            item.shootSpeed = 10;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe;
			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SilverBroadsword, 1);
			recipe.AddIngredient(null, "GoddessFeather", 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.TungstenBroadsword, 1);
			recipe.AddIngredient(null, "GoddessFeather", 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
