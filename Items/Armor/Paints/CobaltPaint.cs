using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace AAMod.Items.Armor.Paints
{
    [AutoloadEquip(EquipType.Head)]
    public class CobaltPaint : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cobalt Face Paint");
            Tooltip.SetDefault(@"18% increased minion damage
+40 Mana");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.value = 50000;
            item.rare = ItemRarityID.LightRed;
            item.defense = 2;
        }

        public override void UpdateEquip(Player player)
        {
            player.minionDamage += 0.18f;
            player.statManaMax2 += 40;
        }

        public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
        {
            drawHair = true;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemID.CobaltBreastplate && legs.type == ItemID.CobaltLeggings;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = Language.GetTextValue("Mods.AAMod.Common.CobaltPaintBonus");
            player.armorEffectDrawOutlines = true;
            player.armorEffectDrawShadow = true;
            player.maxMinions += 2;
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.CobaltBar, 6);
                recipe.AddIngredient(ItemID.BottledWater, 1);
                recipe.AddTile(TileID.BewitchingTable);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}