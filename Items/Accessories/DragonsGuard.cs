using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Accessories
{
    [AutoloadEquip(EquipType.Shield)]
    public class DragonsGuard : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 22;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = ItemRarityID.Blue;
            item.accessory = true;
            item.defense = 3;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<AAPlayer>().DragonsGuard = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon's Guard");
            Tooltip.SetDefault(@"Enemies that strike you are set ablaze");
        }
    }
}
