using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Blazing
{
    [AutoloadEquip(EquipType.Legs)]
	public class BlazingSuneate : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Blazing Suneate");
            Tooltip.SetDefault(@"2% increased Damage Resistance
Forged in the flames of the blazing sun");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 18;
            item.value = 90000;
            item.rare = 4;
			item.defense = 8;
		}
		
		public override void UpdateEquip(Player player)
        {
            player.endurance += 0.2f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("KindledSuneate"));
            recipe.AddIngredient(mod.ItemType("OceanBoots"));
            recipe.AddIngredient(ItemID.FossilPants);
            recipe.AddIngredient(mod.ItemType("DoomiteUGreaves"));
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
}