using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Depth
{
    [AutoloadEquip(EquipType.Body)]
    public class DepthGi : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Depth Gi");
            Tooltip.SetDefault(@"40% increased movement speed
Weightless as shadow itself");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = 10000;
            item.rare = ItemRarityID.Green;
            item.defense = 5;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += .40f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "AbyssiumBar", 25);
            recipe.AddIngredient(null, "HydraHide", 20);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}