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
			item.rare = ItemRarityID.LightRed;
			item.defense = 9;
		}

		public override void UpdateEquip(Player player)
		{
			player.meleeDamage += .07f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.CrimsonScalemail, 1);
            recipe.AddIngredient(ItemID.JungleSpores, 8);
            recipe.AddIngredient(ItemID.Bone, 8);
            recipe.AddIngredient(null, "DevilSilk", 8);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}