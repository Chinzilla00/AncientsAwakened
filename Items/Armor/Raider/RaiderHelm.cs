using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Armor.Raider
{
    [AutoloadEquip(EquipType.Head)]
	public class RaiderHelm : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Raider Helmet");
        }

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 24;
            item.value = Item.sellPrice(0, 0, 5, 0);
            item.rare = 4;
            item.defense = 10;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("VikingHelm"));
            recipe.AddIngredient(mod.ItemType("DepthFukumen"));
            recipe.AddIngredient(mod.ItemType("OceanHelm"));
            recipe.AddIngredient(mod.ItemType("DoomiteUHelm"));
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}