using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Armor.Atlantean
{
    [AutoloadEquip(EquipType.Legs)]
	public class AtlanteanGreaves : BaseAAItem
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
			item.value = Item.sellPrice(0, 0, 5, 0);
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
            ModRecipe recipe;
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("OceanBoots"));
            recipe.AddIngredient(mod.ItemType("HydraHide"), 6);
            recipe.AddIngredient(null, "RelicBar", 6);
            recipe.AddIngredient(null, "Doomite", 6);
            recipe.AddTile(TileID.DemonAltar);
			recipe.SetResult(this);
			recipe.AddRecipe();
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("OceanBoots"));
            recipe.AddIngredient(mod.ItemType("BroodScale"), 6);
            recipe.AddIngredient(ItemID.FossilOre, 6);
            recipe.AddIngredient(null, "Doomite", 6);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
	}
}