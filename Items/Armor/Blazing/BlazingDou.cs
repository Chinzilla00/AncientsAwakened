using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Blazing
{
    [AutoloadEquip(EquipType.Body)]
	public class BlazingDou : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Blazing Dao");
			Tooltip.SetDefault(@"Increases melee damage by 10%
Forged in the flames of the blazing sun");
		}

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 24;
			item.value = 60000;
			item.rare = 4;
			item.defense = 8;
		}

		public override void UpdateEquip(Player player)
		{
			player.meleeDamage += 0.1f;
			player.buffImmune[BuffID.OnFire] = true;
        }

		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("KindledDou"));
            recipe.AddIngredient(mod.ItemType("OceanShirt"));
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
}