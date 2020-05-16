using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Armor.Hoodlum
{
    [AutoloadEquip(EquipType.Body)]
	public class HoodlumShirt : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Hopping Hoodlum Shirt");
            Tooltip.SetDefault(@"10% increased melee speed
+1 max minion
Enemies are more likely to target you
Hopping Mad.");
        }


        public override void SetDefaults()
		{
			item.width = 26;
			item.height = 20;
			item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = ItemRarityID.Yellow;
            item.defense = 28;
		}

        public override void UpdateEquip(Player player)
		{
            player.meleeSpeed += .1f;
            player.maxMinions += 1;
            player.aggro += 2;
        }
		
		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "RajahPelt", 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}