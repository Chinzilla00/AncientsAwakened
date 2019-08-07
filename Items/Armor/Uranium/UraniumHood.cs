using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Uranium
{
    [AutoloadEquip(EquipType.Head)]
    public class UraniumHood : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Uranium Hood");
            Tooltip.SetDefault(@"+90 increased max mana
18% increased magic damage and reduced mana consumption");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 20;
            item.value = 90000;
            item.rare = 8;
            item.defense = 6;
        }

        public override void UpdateEquip(Player player)
        {
            player.manaCost *= .82f;
            player.magicDamage *= 1.18f;
            player.statManaMax2 += 90;
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