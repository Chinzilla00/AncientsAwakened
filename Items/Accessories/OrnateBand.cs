using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Accessories
{
    [AutoloadEquip(EquipType.HandsOn)]
    public class OrnateBand : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 22;
            item.value = Item.sellPrice(0, 8, 0, 0);
            item.rare = 6;
            item.accessory = true;
            item.defense = 8;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ornate Band");
            Tooltip.SetDefault("");
        }

    }
}