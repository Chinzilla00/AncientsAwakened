using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Nights
{
    [AutoloadEquip(EquipType.Legs)]
    public class NightsGreaves : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Night's Greaves");
            Tooltip.SetDefault("9% increased melee speed");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 18;
            item.value = 90000;
            item.rare = ItemRarityID.LightRed;
            item.defense = 7;
        }

        public override void UpdateEquip(Player player)
        {
            player.meleeSpeed += 0.09f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ShadowGreaves, 1);
            recipe.AddIngredient(ItemID.JungleSpores, 6);
            recipe.AddIngredient(ItemID.Bone, 6);
            recipe.AddIngredient(null, "DevilSilk", 6);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}