using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Accessories
{
    [AutoloadEquip(EquipType.HandsOn)]
    public class ShadowBand : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 44;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = 1;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.moveSpeed += .15f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shadow Band");
            Tooltip.SetDefault(@"15% increased movement speed");
        }
    }
}