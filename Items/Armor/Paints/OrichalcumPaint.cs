using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Paints
{
    [AutoloadEquip(EquipType.Head)]
    public class OrichalcumPaint : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Oricalcum Face Paint");
            Tooltip.SetDefault(@"22% increased minion damage
+80 mana");
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
            player.minionDamage += 0.22f;
            player.statManaMax2 += 80;
        }

        public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
        {
            drawHair = true;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemID.OrichalcumBreastplate && legs.type == ItemID.OrichalcumLeggings;
        }

        public override void UpdateArmorSet(Player player)
        {

            player.setBonus = Lang.ArmorBonus("OrichalcumPaintBonus");
            player.maxMinions += 2;
            player.onHitPetal = true;
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.OrichalcumBar, 6);
                recipe.AddIngredient(ItemID.BottledWater, 1);
                recipe.AddTile(TileID.BewitchingTable);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}