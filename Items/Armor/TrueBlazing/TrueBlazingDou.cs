using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.TrueBlazing
{
    [AutoloadEquip(EquipType.Body)]
	public class TrueBlazingDou : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("True Blazing Dao");
			Tooltip.SetDefault(@"Increases melee damage by 20%
Forged in the flames of the blazing sun");
		}

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 24;
			item.value = 60000;
			item.rare = 7;
			item.defense = 18;
		}

		public override void UpdateEquip(Player player)
		{
			player.meleeDamage += 0.2f;
			player.buffImmune[BuffID.OnFire] = true;
        }

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("BlazingDou"));
			recipe.AddIngredient(null, "InfernoCrystal", 1);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}