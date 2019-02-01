using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.TrueDynaskull
{
    [AutoloadEquip(EquipType.Legs)]
	public class TrueDynaskullGreaves : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("True Dynaskull Greaves");
            Tooltip.SetDefault(@"80% Increased throwing critical chance
+12% movement speed");

        }

		public override void SetDefaults()
		{
            item.width = 30;
			item.height = 28;
			item.value = 100000;
			item.rare = 7;
			item.defense = 12;
		}

		public override void UpdateEquip(Player player)
		{
			player.thrownCrit += 80;
            player.moveSpeed *= 1.12f;
		}

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DynaskullGreaves", 1);
            recipe.AddIngredient(null, "DesertCrystal", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}