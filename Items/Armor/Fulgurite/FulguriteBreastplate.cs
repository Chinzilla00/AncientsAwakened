using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Fulgurite
{
    [AutoloadEquip(EquipType.Body)]
	public class FulguriteBreastplate : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Fulgurite Breastplate");
            Tooltip.SetDefault("50% increased Throwing damage");
        }

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 20;
			item.value = 50000;
			item.rare = 5;
			item.defense = 14;
		}

		public override void UpdateEquip(Player player)
		{
			player.thrownDamage *= 1.5f;
            player.moveSpeed *= 1.12f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FulguriteBar", 24);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}