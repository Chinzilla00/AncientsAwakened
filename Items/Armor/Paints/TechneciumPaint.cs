using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Paints
{
    [AutoloadEquip(EquipType.Head)]
    public class TechneciumPaint : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Technecium Face Paint");
            Tooltip.SetDefault(@"+100 max mana
4% increased damage reistance
25% increased minion damage");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.value = 50000;
            item.rare = 4;
            item.defense = 5;
        }

        public override void UpdateEquip(Player player)
        {
            player.minionDamage += 0.25f;
            player.statManaMax2 += 100;
            player.endurance *= 1.04f;
        }

        public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
        {
            drawHair = true;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType<Technecium.TechneciumPlate>() && legs.type == mod.ItemType<Technecium.TechneciumGreaves>();
        }

        public override void UpdateArmorSet(Player player)
        {

            player.setBonus = Lang.ArmorBonus("TechneciumPaintBonus");


            player.GetModPlayer<AAPlayer>(mod).techneciumSet = true;
            player.maxMinions += 4;
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(mod, "TechneciumBar", 6);
                recipe.AddIngredient(ItemID.BottledWater, 1);
                recipe.AddTile(TileID.BewitchingTable);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}