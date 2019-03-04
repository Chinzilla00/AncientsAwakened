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
			DisplayName.SetDefault("Fulgurite Breastplate");
            Tooltip.SetDefault("7% Increased Damage");
        }

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 20;
			item.value = 40000;
			item.rare = 5;
			item.defense = 17;
		}

		public override void UpdateEquip(Player player)
		{
			player.thrownDamage *= 1.07f;
            player.meleeDamage *= 1.07f;
            player.rangedDamage *= 1.07f;
            player.magicDamage *= 1.07f;
            player.minionDamage *= 1.07f;
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