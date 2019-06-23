using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Armor.TrueDoomite
{
    [AutoloadEquip(EquipType.Legs)]
	public class TrueDoomiteGreaves : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Overload Doomite Greaves");
            Tooltip.SetDefault(@"25% increased movement speed");

        }

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 24;
			item.value = Item.sellPrice(0, 5, 0, 0);
			item.rare = 7;
			item.defense = 19;
		}
        
		public override void UpdateEquip(Player player)
		{
			player.moveSpeed += 0.25f;
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("DoomiteGreaves"));
			recipe.AddIngredient(null, "VoidCrystal", 1);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}