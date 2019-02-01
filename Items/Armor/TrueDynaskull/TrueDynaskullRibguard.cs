using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.TrueDynaskull
{
    [AutoloadEquip(EquipType.Body)]
	public class TrueDynaskullRibguard : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("True Dynaskull Ribguard");
            Tooltip.SetDefault("60% increased Throwing damage");
        }

		public override void SetDefaults()
		{
			item.width = 28;
			item.height = 18;
			item.value = 100000;
			item.rare = 7;
			item.defense = 17;
		}

		public override void UpdateEquip(Player player)
		{
			player.thrownDamage *= 1.6f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DynaskullRibguard", 1);
            recipe.AddIngredient(null, "DesertCrystal", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}