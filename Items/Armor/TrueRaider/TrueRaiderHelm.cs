using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Armor.TrueRaider
{
    [AutoloadEquip(EquipType.Head)]
	public class TrueRaiderHelm : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("True Raider Helmet");
            Tooltip.SetDefault(@"Increases melee speed by 15%");
        }

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 24;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 7;
            item.defense = 17;
        }
		
		public override void UpdateEquip(Player player)
		{
           player.meleeSpeed += 0.15f;
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("RaiderHelm"));
			recipe.AddIngredient(null, "IceCrystal", 1);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}