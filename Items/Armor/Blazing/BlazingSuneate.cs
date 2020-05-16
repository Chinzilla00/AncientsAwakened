using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Blazing
{
    [AutoloadEquip(EquipType.Legs)]
	public class BlazingSuneate : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Blazing Suneate");
            Tooltip.SetDefault(@"1% increased Damage Resistance
2% increased Melee Damage
Forged in the flames of the blazing sun");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 18;
			item.rare = ItemRarityID.LightRed;
			item.defense = 8;
		}
		
		public override void UpdateEquip(Player player)
        {
            player.endurance += 0.01f;
            player.meleeDamage += 0.02f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("KindledSuneate"));
            recipe.AddIngredient(ItemID.Coral, 6);
            recipe.AddIngredient(ItemID.FossilOre, 6);
            recipe.AddIngredient(mod.ItemType("Doomite"), 6);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
}