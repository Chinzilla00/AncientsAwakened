using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Accessories
{
    [AutoloadEquip(EquipType.Shield)]
    public class CapShield : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 22;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = 1;
            item.accessory = true;
            item.defense = 10;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<AAPlayer>(mod).CapShield = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon's Guard");
            Tooltip.SetDefault(@"Enemies that strike you are set ablaze");
        }
    }
}