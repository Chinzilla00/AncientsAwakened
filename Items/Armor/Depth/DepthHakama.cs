using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Depth
{
    [AutoloadEquip(EquipType.Legs)]
    public class DepthHakama : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Depth Hakama");
            Tooltip.SetDefault(@"15% increased movement speed
Weightless as shadow itself");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 18;
            item.value = 5000;
            item.rare = ItemRarityID.Green;
            item.defense = 5;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.15f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "AbyssiumBar", 20);
            recipe.AddIngredient(null, "HydraHide", 15);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}