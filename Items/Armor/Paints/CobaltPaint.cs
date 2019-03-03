using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Paints
{
    [AutoloadEquip(EquipType.Head)]
    public class CobaltPaint : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cobalt Face Paint");
            Tooltip.SetDefault("10% increased minion damage");
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
            player.minionDamage += 0.1f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemID.CobaltBreastplate && legs.type == ItemID.CobaltLeggings;
        }

        public override void UpdateArmorSet(Player player)
        {

            player.setBonus = @"+40 Mana
+2 Minion slots";
            player.statManaMax2 += 40;
            player.maxMinions += 2;
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.CobaltBar, 6);
                recipe.AddIngredient(ItemID.BottledWater, 6);
                recipe.AddTile(null, "Mortar_Tile");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}