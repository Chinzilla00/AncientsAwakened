using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Fleshrend
{
    [AutoloadEquip(EquipType.Legs)]
	public class FleshrendGreaves : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fleshrend Greaves");
			Tooltip.SetDefault("7% increased melee damage");

		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 20;
			item.value = 90000;
			item.rare = ItemRarityID.LightRed;
			item.defense = 8;
		}

		public override void UpdateEquip(Player player)
		{
			player.meleeDamage += .07f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.CrimsonGreaves, 1);
            recipe.AddIngredient(ItemID.JungleSpores, 6);
            recipe.AddIngredient(ItemID.Bone, 6);
            recipe.AddIngredient(null, "DevilSilk", 6);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}