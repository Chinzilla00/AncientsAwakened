using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Paints
{
    [AutoloadEquip(EquipType.Head)]
    public class UraniumPaint : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Uranium Face Paint");
            Tooltip.SetDefault(@"+60 max mana
3% increased damage reistance
20% increased minion damage");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.value = 50000;
            item.rare = 4;
            item.defense = 3;
        }

        public override void UpdateEquip(Player player)
        {
            player.minionDamage += 0.2f;
            player.statManaMax2 += 60;
            player.endurance *= 1.03f;
        }

        public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
        {
            drawHair = true;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType<Uranium.UraniumChestplate>() && legs.type == mod.ItemType<Uranium.UraniumBoots>();
        }

        public override void UpdateArmorSet(Player player)
        {

            player.setBonus = Lang.ArmorBonus("UraniumPaintBonus");


            player.GetModPlayer<AAPlayer>(mod).uraniumSet = true;
            player.maxMinions += 3;
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(mod, "UraniumBar", 6);
                recipe.AddIngredient(ItemID.BottledWater, 1);
                recipe.AddTile(TileID.BewitchingTable);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}