using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Armor.Viking
{
    [AutoloadEquip(EquipType.Head)]
	public class VikingHelm : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Viking Helmet");
        }

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 24;
            item.value = Item.sellPrice(0, 0, 5, 0);
            item.rare = ItemRarityID.Orange;
            item.defense = 8;
        }
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null,"RelicBar", 14);
            recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}