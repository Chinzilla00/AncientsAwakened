using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.TrueFleshrend
{
    [AutoloadEquip(EquipType.Legs)]
	public class TrueFleshrendGreaves : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("True Fleshrend Greaves");
			Tooltip.SetDefault(@"13% increased melee damage
9% increased movement speed");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 20;
			item.value = 100000;
			item.rare = 16;
			item.defense = 10;
		}

		public override void UpdateEquip(Player player)
		{
			player.meleeDamage *= 1.13f;
            player.moveSpeed *= 1.09f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FleshrendGreaves", 1);
            recipe.AddIngredient(null, "CrimsonCrystal", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}