using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;


namespace AAMod.Items.Armor.Tribal
{
    [AutoloadEquip(EquipType.Head)]
    public class TribalHat : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tribal Hat");
            Tooltip.SetDefault(@"8% Increased magic critical chance
Increases maximum mana by 20");
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 24;
            item.value = 90000;
            item.rare = 4;
            item.defense = 7;
        }

        public override void UpdateEquip(Player player)
        {
            player.statManaMax2 += 20;
            player.magicCrit += 8;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("TribalCloak") && legs.type == mod.ItemType("TribalKilt");
        }

        public override void UpdateArmorSet(Player player)
        {

            player.setBonus = Language.GetTextValue("Mods.AAMod.Common.TribalHatBonus");

            player.manaCost *= 0.7f;
            player.manaFlower = true;
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.JungleHat, 1);
                recipe.AddIngredient(ItemID.ShadowScale, 8);
                recipe.AddIngredient(ItemID.Bone, 8);
                recipe.AddIngredient(null, "DevilSilk", 8);
                recipe.AddTile(TileID.DemonAltar);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.JungleHat, 1);
                recipe.AddIngredient(ItemID.TissueSample, 8);
                recipe.AddIngredient(ItemID.Bone, 8);
                recipe.AddIngredient(null, "DevilSilk", 8);
                recipe.AddTile(TileID.DemonAltar);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}