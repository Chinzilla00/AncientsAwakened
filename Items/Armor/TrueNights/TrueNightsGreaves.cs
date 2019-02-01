using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.TrueNights
{
    [AutoloadEquip(EquipType.Legs)]
    public class TrueNightsGreaves : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("True Night's Greaves");
            Tooltip.SetDefault(@"14% increased melee speed
+14% movement speed");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 18;
            item.value = 100000;
            item.rare = 7;
            item.defense = 14;
        }

        public override void UpdateEquip(Player player)
        {
            player.meleeSpeed += 0.14f;
            player.moveSpeed *= 1.14f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "NightsGreaves", 1);
            recipe.AddIngredient(null, "CorruptionCrystal", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}