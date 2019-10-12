using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Paints
{
    [AutoloadEquip(EquipType.Head)]
    public class PalladiumPaint : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Palladium Face Paint");
            Tooltip.SetDefault(@"15% increased minion damage
+40 Mana");
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
            player.minionDamage += 0.15f;
            player.statManaMax2 += 40;
        }

        public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
        {
            drawHair = true;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemID.PalladiumBreastplate && legs.type == ItemID.PalladiumLeggings;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = @"+1 Minion Slot
Greatly increases life regeneration after striking an enemy";
            player.maxMinions += 1;
            player.GetModPlayer<AAPlayer>().Palladium = true;
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.PalladiumBar, 6);
                recipe.AddIngredient(ItemID.BottledWater, 1);
                recipe.AddTile(TileID.BewitchingTable);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}