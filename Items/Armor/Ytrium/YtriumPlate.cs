using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Ytrium
{
    [AutoloadEquip(EquipType.Body)]
    public class YtriumPlate : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Yttrium Chestplate");
            Tooltip.SetDefault(@"10% increased movement speed");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 18;
            item.value = 70000;
            item.rare = 2;
            item.defense = 6;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += .1f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "YtriumBar", 24);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}