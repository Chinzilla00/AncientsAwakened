using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Uranium
{
    [AutoloadEquip(EquipType.Body)]
    public class UraniumChestplate : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Uranium Chestplate");
            Tooltip.SetDefault(@"7% increased damage & critical strike chance");
        }

        public override void UpdateEquip(Player player)
        {
            player.allDamage += .07f;
            player.meleeCrit += 7;
            player.rangedCrit += 7;
            player.magicCrit += 7;
            player.thrownCrit += 7;
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 20;
            item.value = 90000;
            item.rare = 8;
            item.defense = 24;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "UraniumBar", 24);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}