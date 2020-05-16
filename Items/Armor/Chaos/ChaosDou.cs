using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Chaos
{
    [AutoloadEquip(EquipType.Body)]
	public class ChaosDou : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Chaos Dou");
            Tooltip.SetDefault(@"7% increased damage");
        }


        public override void SetDefaults()
		{
			item.width = 26;
			item.height = 20;
			item.value = Item.sellPrice(3, 0, 0, 0);
            item.rare = ItemRarityID.Lime;
            item.defense = 22;
		}

		public override void UpdateEquip(Player player)
		{
            player.allDamage += .07f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("AAMod:ChaosPlates");
            recipe.AddIngredient(null, "ChaosCrystal");
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}