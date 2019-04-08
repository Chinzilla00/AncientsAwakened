using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Abyssal
{
    [AutoloadEquip(EquipType.Body)]
	public class AbyssalGi : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Abyssal Gi");
			Tooltip.SetDefault(@"40% increased movement speed
Weightless as shadow itself");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = 10000;
			item.rare = 3;
			item.defense = 5;
		}

		public override void UpdateEquip(Player player)
		{
            player.moveSpeed += .40f;
		}

        public override void AddRecipes()
        {
            return;
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DepthGi", 1);
            recipe.AddIngredient(null, "DoomiteUPlate", 1);
            recipe.AddIngredient(null, "VikingPlate", 1);
            recipe.AddIngredient(null, "OceanShirt", 1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}