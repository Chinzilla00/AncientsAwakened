using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class SandScimitar : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sand Scimitar");
		}
		public override void SetDefaults()
		{
			item.damage = 10;
			item.melee = true;
			item.width = 30;
			item.height = 35;
			item.useTime = 24;
			item.useAnimation = 24;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 2;
			item.value = 3000;
			item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Sandstone, 30);
			recipe.AddIngredient(ItemID.Ruby, 1);
			recipe.AddIngredient(ItemID.Sapphire, 1);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
