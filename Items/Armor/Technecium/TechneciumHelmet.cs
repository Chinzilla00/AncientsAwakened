using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Technecium
{
    [AutoloadEquip(EquipType.Head)]
	public class TechneciumHelmet : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Technecium Helmet");
            Tooltip.SetDefault(@"4% increased damage resistance
10% increased magic damage & critical strike chance
+100 increased mana");
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 24;
            item.value = Item.sellPrice(0, 1, 80, 0);
            item.rare = 4;
            item.defense = 5;
        }

        public override void UpdateEquip(Player player)
        {
            player.endurance *= 1.04f;
            player.magicDamage *= 1.1f;
            player.magicCrit += 12;
            player.statManaMax2 += 100;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("TechneciumPlate") && legs.type == mod.ItemType("TechneciumGreaves");
        }

        public override void UpdateArmorSet(Player player)
        {

            player.setBonus = @"Enemies that hit you are electrified";


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