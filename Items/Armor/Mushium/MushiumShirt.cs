using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Mushium
{
    [AutoloadEquip(EquipType.Body)]
	public class MushiumShirt : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Mushium Shirt");
            Tooltip.SetDefault("15% increased Throwing damage");
        }

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 20;
			item.value = 50;
			item.rare = 1;
			item.defense = 4;
		}

		public override void UpdateEquip(Player player)
		{
			player.thrownDamage *= 1.15f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "MushiumBar", 5);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}