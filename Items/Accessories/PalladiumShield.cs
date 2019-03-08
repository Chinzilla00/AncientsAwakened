using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Accessories
{
    [AutoloadEquip(EquipType.Shield)]
    public class PalladiumShield : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 22;
            item.value = Item.sellPrice(0, 8, 0, 0);
            item.rare = 4;
            item.accessory = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.noKnockback = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Palladium Shield");
            Tooltip.SetDefault("Grants knockback immunity");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.PalladiumBar, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}