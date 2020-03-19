using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Abyssal
{
    [AutoloadEquip(EquipType.Legs)]
	public class AbyssalHakama : BaseAAItem
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
			item.rare = 4;
			item.defense = 6;
		}

		public override void UpdateEquip(Player player)
		{
			player.moveSpeed += 0.30f;
			player.GetModPlayer<AAPlayer>().MaxMovespeedboost += 0.3f;
		}

		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DepthHakama", 1);
            recipe.AddIngredient(null, "RelicBar", 6);
            recipe.AddIngredient(ItemID.Coral, 6);
            recipe.AddIngredient(null, "Doomite", 6);
            recipe.AddTile(TileID.DemonAltar);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}