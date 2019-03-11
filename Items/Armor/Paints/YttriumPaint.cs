using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Paints
{
    [AutoloadEquip(EquipType.Head)]
    public class YttriumPaint : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Yttrium Face Paint");
            Tooltip.SetDefault(@"+50 Max Mana
8% increased movement speed
15% increased minion damage");
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
            player.statManaMax2 += 50;
            player.moveSpeed += 0.15f;
        }

        public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
        {
            drawHair = true;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType<Items.Armor.Ytrium.YtriumPlate>() && legs.type == mod.ItemType<Items.Armor.Ytrium.YtriumGreaves>();
        }

        public override void UpdateArmorSet(Player player)
        {

            player.setBonus = @"+2 Minion slots
You can do a lightning-quick dash";
            player.dash = 2;
            player.maxMinions += 2;
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(mod, "YtriumBar", 6);
                recipe.AddIngredient(ItemID.BottledWater, 1);
                recipe.AddTile(TileID.BewitchingTable);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}