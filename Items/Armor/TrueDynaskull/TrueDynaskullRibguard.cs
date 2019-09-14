using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.TrueDynaskull
{
    [AutoloadEquip(EquipType.Body)]
	public class TrueDynaskullRibguard : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Primeval Ribguard");
            Tooltip.SetDefault("18% increased Ranged damage");
        }

		public override void SetDefaults()
		{
			item.width = 28;
			item.height = 18;
			item.value = 100000;
			item.rare = 7;
			item.defense = 19;
		}

		public override void UpdateEquip(Player player)
		{
			player.rangedDamage *= 1.18f;
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