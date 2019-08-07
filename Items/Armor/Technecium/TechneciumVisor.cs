using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Technecium
{
    [AutoloadEquip(EquipType.Head)]
	public class TechneciumVisor : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Technecium Visor");
            Tooltip.SetDefault(@"4% increased damage resistance
18% increased melee damage & critical strike chance");
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 24;
            item.value = Item.sellPrice(0, 1, 80, 0);
            item.rare = 9;
            item.defense = 28;
        }

        public override void UpdateEquip(Player player)
        {
            player.endurance *= 1.4f;
            player.meleeDamage *= 1.18f;
            player.meleeCrit += 18;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("TechneciumPlate") && legs.type == mod.ItemType("TechneciumGreaves");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = @"Hitting enemies causes you to build up a static charge
Charge is released once you reach a charge level of 4 or you are hit";
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