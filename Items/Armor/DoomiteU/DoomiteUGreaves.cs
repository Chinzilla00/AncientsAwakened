using Terraria;
using Terraria.ModLoader;
using Terraria.ID;


namespace AAMod.Items.Armor.DoomiteU
{
    [AutoloadEquip(EquipType.Legs)]
	public class DoomiteUGreaves : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dark Doomite Greaves");
            Tooltip.SetDefault(@"Increases minion damage by 4%
Increases movement speed by 15%");

        }

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 24;
			item.value = Item.sellPrice(0, 0, 5, 0);
			item.rare = 3;
			item.defense = 4;
		}
		
		public override void UpdateEquip(Player player)
		{
            player.minionDamage += 0.04f;
			player.moveSpeed += 0.15f;
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DemoniteBar, 8);
			recipe.AddIngredient(ItemID.MeteoriteBar, 8);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
			
			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.CrimtaneBar, 8);
			recipe.AddIngredient(ItemID.MeteoriteBar, 8);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}