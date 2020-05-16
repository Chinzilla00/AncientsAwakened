using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

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
            item.rare = ItemRarityID.LightPurple;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 50;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ornate Band");
            Tooltip.SetDefault("+50 Max Life");
        }

    }
}