using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Nights
{
    [AutoloadEquip(EquipType.Body)]
    public class NightsPlate : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Night's Plate");
            Tooltip.SetDefault("9% increased melee speed");

        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 20;
            item.value = 90000;
            item.rare = 4;
            item.defense = 9;
        }

        public override void UpdateEquip(Player player)
        {
            player.meleeSpeed += 0.09f;
        }
        public override void AddRecipes()
        {
            { 
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ShadowScalemail, 1);
            recipe.AddIngredient(ItemID.JungleShirt, 1);
            recipe.AddIngredient(ItemID.NecroBreastplate, 1);
            recipe.AddIngredient(null, "ImpGarb", 1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
            }
        }
    }
}