using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Tribal
{
    [AutoloadEquip(EquipType.Body)]
    public class TribalCloak : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tribal Cloak");
            Tooltip.SetDefault(@"8% Increased magic critical chance
Increases Maximum Mana by 20");
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 24;
            item.value = 90000;
            item.rare = 4;
            item.defense = 6;
        }

        public override void UpdateEquip(Player player)
        {
            player.statManaMax2 += 20;
            player.magicCrit += 8;
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.JungleShirt, 1);
                recipe.AddIngredient(ItemID.ShadowScale, 6);
                recipe.AddIngredient(ItemID.Bone, 6);
                recipe.AddIngredient(null, "DevilSilk", 6);
                recipe.AddTile(TileID.DemonAltar);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.JungleShirt, 1);
                recipe.AddIngredient(ItemID.TissueSample, 6);
                recipe.AddIngredient(ItemID.Bone, 6);
                recipe.AddIngredient(null, "DevilSilk", 6);
                recipe.AddTile(TileID.DemonAltar);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}