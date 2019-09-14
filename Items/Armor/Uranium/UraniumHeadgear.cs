using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Uranium
{
    [AutoloadEquip(EquipType.Head)]
    public class UraniumHeadgear : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Uranium Headgear");
            Tooltip.SetDefault(@"8% increased ranged damage
20% chance to not consume ammo");
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 20;
            item.value = 90000;
            item.rare = 8;
            item.defense = 10;
        }

        public override void UpdateEquip(Player player)
        {
            player.rangedDamage *= 1.08f;
            player.ammoCost80 = true;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("UraniumChestplate") && legs.type == mod.ItemType("UraniumBoots");
        }

        public override void UpdateArmorSet(Player player)
        {

            player.setBonus = Lang.ArmorBonus("UraniumHeadgearBonus");


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