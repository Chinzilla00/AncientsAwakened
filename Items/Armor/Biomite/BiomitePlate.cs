using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Biomite
{
    [AutoloadEquip(EquipType.Body)]
	public class BiomitePlate : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Biomite Crystalmail");
		}

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 24;
			item.value = 6000;
			item.rare = ItemRarityID.Green;
			item.defense = 5;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "TerraShard", 25);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}