using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Ytrium
{
    [AutoloadEquip(EquipType.Head)]
    public class YtriumMask : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Yttrium Mask");
            Tooltip.SetDefault(@"8% increased movement speed
15% increased melee speed");
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 24;
            item.value = 70000;
            item.rare = 4;
            item.defense = 4;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed *= 1.08f;
            player.meleeSpeed *= 1.08f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("YtriumPlate") && legs.type == mod.ItemType("YtriumGreaves");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = @"You can do a lightning-quick dash.";
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