using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Ytrium
{
    [AutoloadEquip(EquipType.Head)]
    public class YtriumHeadgear : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Yttrium Headgear");
            Tooltip.SetDefault(@"10% increased movement speed");
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 24;
            item.value = 70000;
            item.rare = 2;
            item.defense = 6;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += .1f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("YtriumPlate") && legs.type == mod.ItemType("YtriumGreaves");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = @"You can do a lightning-quick dash.";
            player.rangedDamage += .1f;
            player.dash = 2;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "YtriumBar", 12);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}