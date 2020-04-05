using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Terra
{
    [AutoloadEquip(EquipType.Body)]
	public class TerraPlate : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Terra Chestplate");
            Tooltip.SetDefault(@"5% increased damage");
        }


        public override void SetDefaults()
		{
			item.width = 26;
			item.height = 20;
			item.value = Item.sellPrice(3, 0, 0, 0);
            item.rare = 7;
            item.defense = 22;
		}

		public override void UpdateEquip(Player player)
		{
            player.allDamage += .05f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("AAMod:TerraPlates");
            recipe.AddIngredient(null, "TerraCrystal");
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}