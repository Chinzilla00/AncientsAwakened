using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Uranium
{
    [AutoloadEquip(EquipType.Head)]
    public class UraniumVisor : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Uranium Visor");
            Tooltip.SetDefault(@"17% increased melee damage
7% increased melee critical strike chance");
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 24;
            item.value = 90000;
            item.rare = 8;
            item.defense = 24;
        }

        public override void UpdateEquip(Player player)
        {
            player.meleeCrit += 7;
            player.meleeDamage *= 1.08f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("UraniumChestplate") && legs.type == mod.ItemType("UraniumBoots");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = Lang.ArmorBonus("UraniumVisorBonus");
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