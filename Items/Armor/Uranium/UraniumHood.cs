using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Uranium
{
    [AutoloadEquip(EquipType.Head)]
    public class UraniumHood : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Uranium Hood");
            Tooltip.SetDefault(@"+60 increased max mana
3% increased damage resistance
8% increased Magic damage");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 20;
            item.value = 90000;
            item.rare = 4;
            item.defense = 5;
        }

        public override void UpdateEquip(Player player)
        {
            player.endurance *= 1.03f;
            player.magicDamage *= 1.08f;
            player.statManaMax2 += 60;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("UraniumChestplate") && legs.type == mod.ItemType("UraniumBoots");
        }

        public override void UpdateArmorSet(Player player)
        {

            player.setBonus = @"Enemies near you are burned by radiation emitted by your armor";


            player.GetModPlayer<AAPlayer>(mod).uraniumSet = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "UraniumBar", 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}