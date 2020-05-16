using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Accessories
{
    [AutoloadEquip(EquipType.HandsOn)]
    public class BotchedBand : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 24;
            item.value = Item.sellPrice(0, 8, 0, 0);
            item.rare = ItemRarityID.LightPurple;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.moveSpeed += .1f;
            player.allDamage += .1f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Botched Band");
            Tooltip.SetDefault(
@"10% Increased movement speed and damage");
        }

    }
}