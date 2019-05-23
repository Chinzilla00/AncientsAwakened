using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Madness
{
    [AutoloadEquip(EquipType.Legs)]
    public class MadnessBoots : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Madness Boots");
            Tooltip.SetDefault("Movement speed increased by 7%");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = 1700;
            item.rare = 1;
            item.defense = 4;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.07f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "MadnessFragment", 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}