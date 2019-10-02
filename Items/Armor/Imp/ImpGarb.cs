using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Imp
{
    [AutoloadEquip(EquipType.Body)]
    public class ImpGarb : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Imp Garb");
            Tooltip.SetDefault("7% Increased Minion damage");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 22;
            item.value = 7000;
            item.rare = 2;
            item.defense = 4;
        }

        public override void UpdateEquip(Player player)
        {
            player.minionDamage += 0.07f;
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "DevilSilk", 7);
                recipe.AddTile(TileID.Loom);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}