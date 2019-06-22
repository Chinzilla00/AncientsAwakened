using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Nightmare
{
    [AutoloadEquip(EquipType.Body)]
	public abstract class NightmareBreastplate : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("7% increased damage");
			DisplayName.SetDefault("Nightmare Breastplate");
		}

		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 20;
			item.value = Item.sellPrice(0, 1, 68, 0);
			item.rare = 5;
			item.defense = 10;
		}
		
		public override void UpdateEquip(Player player)
		{
			player.thrownDamage += 0.07f;
            player.rangedDamage += 0.07f;
            player.magicDamage += 0.07f;
            player.meleeDamage += 0.07f;
            player.minionDamage += 0.07f;
        }
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "Nightmare_Bar", 8);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}