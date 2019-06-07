using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Abyssal
{
    [AutoloadEquip(EquipType.Legs)]
	public class AbyssalHakama : ModItem
	{
		public override void SetStaticDefaults()
		{
            base.SetStaticDefaults();
            DisplayName.SetDefault("Abyssal Hakama");
            Tooltip.SetDefault(@"30% increased movement speed
Weightless as shadow itself");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 18;
            item.value = 90000;
            item.rare = 2;
			item.defense = 5;
		}

		public override void UpdateEquip(Player player)
		{
			player.moveSpeed += 0.30f;
		}

		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DepthHakama", 1);
            recipe.AddIngredient(null, "DoomiteUGreaves", 1);
            recipe.AddIngredient(null, "VikingBoots", 1);
            recipe.AddIngredient(null, "OceanBoots", 1);
            recipe.AddTile(TileID.DemonAltar);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}