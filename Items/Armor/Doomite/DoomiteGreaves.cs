using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Doomite
{
    [AutoloadEquip(EquipType.Legs)]
    public class DoomiteGreaves : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Doomite Greaves");
            Tooltip.SetDefault(@"12% increased movement speed");
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 20;
            item.rare = 4;
            item.defense = 14;
            item.value = 20000;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.12f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Doomite", 15);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}