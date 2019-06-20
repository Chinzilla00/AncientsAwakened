using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.TrueAbyssal
{
    [AutoloadEquip(EquipType.Legs)]
	public class TrueAbyssalHakama : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            base.SetStaticDefaults();
            DisplayName.SetDefault("Perfect Abyssal Hakama");
            Tooltip.SetDefault(@"40% increased movement speed
15% increased ranged damage");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 18;
			item.value = 50000;
			item.rare = 7;
			item.defense = 15;
		}

		public override void UpdateEquip(Player player)
		{
			player.rangedDamage += .15f;
			player.moveSpeed += 0.40f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("AbyssalHakama"));
			recipe.AddIngredient(null, "MireCrystal", 1);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}