using Terraria;
using Microsoft.Xna.Framework; using Microsoft.Xna.Framework.Graphics; using Terraria.ModLoader;

namespace AAMod.Items.Boss.Hydra
{
    [AutoloadEquip(EquipType.Neck)]
    public class HydraPendant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hydra Pendant");
            Tooltip.SetDefault(@"15% Increased attack speed");
        }
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 50;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.accessory = true;
            item.expert = true;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<AAPlayer>().HydraPendant = true;
        }
    }
}