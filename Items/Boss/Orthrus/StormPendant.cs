using Terraria;
using Microsoft.Xna.Framework; using Microsoft.Xna.Framework.Graphics; using Terraria.ModLoader;

namespace AAMod.Items.Boss.Orthrus
{
    [AutoloadEquip(EquipType.Neck)]
    public class StormPendant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Storm Pendant");
            Tooltip.SetDefault(@"50% Increased damage
10% Increased melee speed");
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
            player.meleeSpeed *= 1.1f;
            player.meleeDamage *= 1.5f;
            player.rangedDamage *= 1.5f;
            player.magicDamage *= 1.5f;
            player.minionDamage *= 1.5f;
            player.thrownDamage *= 1.5f;
        }
    }
}