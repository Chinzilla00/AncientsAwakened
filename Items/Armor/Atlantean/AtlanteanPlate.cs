using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Armor.Atlantean
{
    [AutoloadEquip(EquipType.Body)]
	public class AtlanteanPlate : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Atlantean Chestplate");
			Tooltip.SetDefault(@"Increases magic damage by 15%
It vibrates with the powers of Atlantis");

		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 24;
			item.value = Item.sellPrice (0, 0, 5, 0);
			item.rare = ItemRarityID.LightRed;
			item.defense = 8;
		}
		
		public override void UpdateEquip(Player player)
		{
            player.magicDamage += 0.15f;
		}
		
		public override void AddRecipes()
		{
            ModRecipe recipe;
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("OceanShirt"));
            recipe.AddIngredient(mod.ItemType("HydraHide"), 8);
            recipe.AddIngredient(null, "RelicBar", 8);
            recipe.AddIngredient(null, "Doomite", 8);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("OceanShirt"));
            recipe.AddIngredient(mod.ItemType("BroodScale"), 8);
            recipe.AddIngredient(ItemID.FossilOre, 8);
            recipe.AddIngredient(null, "Doomite", 8);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
		}
	}
}