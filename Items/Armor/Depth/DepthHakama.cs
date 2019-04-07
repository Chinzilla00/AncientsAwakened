using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Depth
{
    [AutoloadEquip(EquipType.Legs)]
	public class DepthHakama : ModItem
	{
		public override void SetStaticDefaults()
		{
            base.SetStaticDefaults();
            DisplayName.SetDefault("Depth Hakama");
            Tooltip.SetDefault(@"20% increased movement speed
Weightless as shadow itself");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 18;
			item.value = 5000;
			item.rare = 2;
			item.defense = 3;
		}

		public override void UpdateEquip(Player player)
		{
			player.moveSpeed += 0.20f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "AbyssiumBar", 20);
            recipe.AddIngredient(null, "HydraHide", 15);
            recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}