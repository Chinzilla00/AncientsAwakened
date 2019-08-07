using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Technecium
{
    [AutoloadEquip(EquipType.Body)]
	public class TechneciumPlate : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Technecium Plate");
            Tooltip.SetDefault(@"6% Damage resistance");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 18;
            item.value = Item.sellPrice(0, 1, 80, 0);
            item.rare = 9;
            item.defense = 17;
        }

        public override void UpdateEquip(Player player)
        {
            player.endurance *= 1.06f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "TechneciumBar", 24);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}