using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Armor.Raider
{
    [AutoloadEquip(EquipType.Legs)]
	public class RaiderLegs : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Raider Greaves");
        }

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 24;
			item.value = Item.sellPrice(0, 0, 5, 0);
			item.rare = 4;
			item.defense = 12;
		}

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("VikingBoots"));
            recipe.AddIngredient(mod.ItemType("DepthHakama"));
            recipe.AddIngredient(mod.ItemType("OceanBoots"));
            recipe.AddIngredient(mod.ItemType("DoomiteUGreaves"));
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}