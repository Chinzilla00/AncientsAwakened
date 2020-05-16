using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Blazing
{
    [AutoloadEquip(EquipType.Body)]
	public class BlazingDou : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Blazing Dao");
			Tooltip.SetDefault(@"2% increased Damage Resistance
2% increased Melee Damage
Forged in the flames of the blazing sun");
		}

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 24;
			item.rare = ItemRarityID.LightRed;
			item.defense = 8;
		}

        public override void UpdateEquip(Player player)
        {
            player.endurance += 0.02f;
            player.meleeDamage += 0.02f;
        }
		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("KindledDou"));
            recipe.AddIngredient(ItemID.Coral, 8);
            recipe.AddIngredient(ItemID.FossilOre, 8);
            recipe.AddIngredient(mod.ItemType("Doomite"), 8);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
}
