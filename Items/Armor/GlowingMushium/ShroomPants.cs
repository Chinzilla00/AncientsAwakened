using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.GlowingMushium
{
    [AutoloadEquip(EquipType.Legs)]
	public class ShroomPants : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Glowing Mushium Pants");
            Tooltip.SetDefault("2% increased mana regeneration");

        }

		public override void SetDefaults()
		{
            item.width = 22;
			item.height = 18;
			item.value = 50;
			item.rare = 1;
			item.defense = 2;
		}

		public override void UpdateEquip(Player player)
        {
            player.manaRegenBonus += 2;
        }

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "GlowingMushiumBar", 5);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}