using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Doomite
{
    [AutoloadEquip(EquipType.Legs)]
    public class DoomiteGreaves : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Doomite Greaves");
            Tooltip.SetDefault(@"+1 Minion slot");
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 20;
            item.rare = ItemRarityID.LightRed;
            item.defense = 7;
            item.value = 9000;
        }

        public override void UpdateEquip(Player player)
        {
            player.maxMinions += 1;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DoomiteUGreaves");
            recipe.AddIngredient(null, "Doomite", 6);
            recipe.AddIngredient(ItemID.Coral, 6);
            recipe.AddIngredient(ItemID.FossilOre, 6);
            recipe.AddIngredient(null, "BroodScale", 6);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}