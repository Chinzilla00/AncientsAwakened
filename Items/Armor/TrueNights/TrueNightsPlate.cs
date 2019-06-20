using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.TrueNights
{
    [AutoloadEquip(EquipType.Body)]
    public class TrueNightsPlate : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("True Night's Plate");
            Tooltip.SetDefault("14% increased melee speed");

        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 20;
            item.value = 100000;
            item.rare = 7;
            item.defense = 19;
        }

        public override void UpdateEquip(Player player)
        {
            player.meleeSpeed += 0.14f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "NightsPlate", 1);
            recipe.AddIngredient(null, "CorruptionCrystal", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}