using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Armor.Atlantean
{
    [AutoloadEquip(EquipType.Legs)]
	public class AtlanteanGreaves : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Atlantean Greaves");
            Tooltip.SetDefault(@"Increases magic critical strike chance by 10%
Allows to freely move in liquids");

        }

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 24;
            item.value = 90000;
            item.rare = 4;
			item.defense = 6;
		}
        
		public override void UpdateEquip(Player player)
		{
			player.magicCrit += 10;
            player.accFlipper = true;
			player.ignoreWater = true;
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "OceanBoots", 1);
            recipe.AddIngredient(null, "DepthHakama", 1);
            recipe.AddIngredient(null, "DoomiteUGreaves", 1);
            recipe.AddIngredient(null, "VikingBoots", 1);
            recipe.AddTile(TileID.DemonAltar);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}