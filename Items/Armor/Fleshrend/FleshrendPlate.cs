using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Fleshrend
{
    [AutoloadEquip(EquipType.Body)]
	public class FleshrendPlate : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Fleshrend Plate");
			Tooltip.SetDefault("7% Increased melee damage");
		}

		public override void SetDefaults()
		{
			item.width = 34;
			item.height = 34;
			item.value = 90000;
			item.rare = 4;
			item.defense = 9;
		}

		public override void UpdateEquip(Player player)
		{
			player.meleeDamage *= 1.07f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.CrimsonScalemail, 1);
            recipe.AddIngredient(ItemID.JungleShirt, 1);
            recipe.AddIngredient(ItemID.NecroBreastplate, 1);
            recipe.AddIngredient(null, "ImpGarb", 1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}