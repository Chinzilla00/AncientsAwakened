using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.DoomiteU
{
    [AutoloadEquip(EquipType.Body)]
	public class DoomiteUPlate : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dark Doomite Chestplate");
			Tooltip.SetDefault(@"Increases minion damage by 6%");

		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 24;
			item.value = Item.sellPrice (0, 0, 5, 0);
			item.rare = ItemRarityID.Orange;
			item.defense = 4;
		}
		
		public override void UpdateEquip(Player player)
		{
            player.minionDamage += 0.06f;
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DoomiteScrap", 10);
            recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}