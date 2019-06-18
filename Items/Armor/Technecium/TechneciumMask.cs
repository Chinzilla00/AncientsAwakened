using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Technecium
{
    [AutoloadEquip(EquipType.Head)]
	public class TechneciumMask : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Technecium Mask");
            Tooltip.SetDefault(@"4% increased damage resistance
14% increased ranged damage & critical strike chance");
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 24;
            item.value = Item.sellPrice(0, 1, 80, 0);
            item.rare = 4;
            item.defense = 8;
        }

        public override void UpdateEquip(Player player)
        {
            player.endurance *= 1.03f;
            player.rangedDamage *= 1.14f;
            player.rangedCrit += 14;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("TechneciumPlate") && legs.type == mod.ItemType("TechneciumGreaves");
        }

        public override void UpdateArmorSet(Player player)
        {

            player.setBonus = @"Enemies that hit you are inflicted with electrified";


            player.GetModPlayer<AAPlayer>(mod).techneciumSet = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "TechneciumBar", 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}