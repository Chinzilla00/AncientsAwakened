using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Paints
{
    [AutoloadEquip(EquipType.Head)]
    public class TitaniumPaint : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Titanium Face Paint");
            Tooltip.SetDefault(@"27% increased minion damage
+100 mana");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.value = 50000;
            item.rare = 4;
            item.defense = 4;
        }

        public override void UpdateEquip(Player player)
        {
            player.minionDamage += 0.27f;
            player.statManaMax2 += 100;
        }

        public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
        {
            drawHair = true;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemID.TitaniumBreastplate && legs.type == ItemID.TitaniumLeggings;
        }

        public override void UpdateArmorSet(Player player)
        {

            player.setBonus = Lang.ArmorBonus("TitaniumPaintBonus");
            player.onHitDodge = true;
            player.maxMinions += 4;
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.TitaniumBar, 6);
                recipe.AddIngredient(ItemID.BottledWater, 1);
                recipe.AddTile(TileID.BewitchingTable);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}