using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Terra
{
    [AutoloadEquip(EquipType.Legs)]
	public class TerraGreaves : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Terra Greaves");
            Tooltip.SetDefault(@"10% increased movement speed
5% increased damage");
        }

		public override void SetDefaults()
		{
            item.width = 22;
            item.height = 16;
            item.defense = 22;
            item.rare = 7;
        }

        public override void UpdateEquip(Player player)
        {
            player.allDamage += .05f;
            player.moveSpeed += .1f;
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