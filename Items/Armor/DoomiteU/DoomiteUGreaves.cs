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
			DisplayName.SetDefault("Doomite Greaves");
            Tooltip.SetDefault(@"Increases minion damage by 5%");
        }

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 24;
			item.value = Item.sellPrice(0, 0, 5, 0);
			item.rare = 3;
			item.defense = 5;
		}

        public override void UpdateEquip(Player player)
        {
            player.minionDamage += 0.05f;
        }

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Doomite", 2);
            recipe.AddIngredient(null, "DoomiteScrap", 8);
            recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}