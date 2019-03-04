using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Paints
{
    [AutoloadEquip(EquipType.Head)]
    public class MythrilPaint : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mythril Face Paint");
            Tooltip.SetDefault(@"26% increased minion damage
+60 mana");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.value = 50000;
            item.rare = 4;
            item.defense = 2;
        }

        public override void UpdateEquip(Player player)
        {
            player.minionDamage += 0.26f;
            player.statManaMax2 += 60;
        }

        public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
        {
            drawHair = true;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemID.MythrilChainmail && legs.type == ItemID.MythrilGreaves;
        }

        public override void UpdateArmorSet(Player player)
        {

            player.setBonus = @"+3 Minion slots";
            player.maxMinions += 3;
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.OrichalcumBar, 6);
                recipe.AddIngredient(ItemID.BottledWater, 1);
                recipe.AddTile(null, "Mortar_Tile");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}