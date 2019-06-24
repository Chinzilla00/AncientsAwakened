using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.GlowingMushium
{
    [AutoloadEquip(EquipType.Body)]
	public class ShroomShirt : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Glowing Mushium Shirt");
            Tooltip.SetDefault("2% increased mana regeneration");
        }

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 20;
			item.value = 50;
			item.rare = 1;
            item.defense = 3;
            item.value = Item.sellPrice(0, 0, 25, 0);
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