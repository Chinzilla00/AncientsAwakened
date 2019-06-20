using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Orthrus
{
    [AutoloadEquip(EquipType.Neck)]
    public class StormPendant : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Storm Pendant");
            Tooltip.SetDefault(@"18% Increased damage
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
            player.meleeSpeed += .1f;
            player.meleeDamage += .18f;
            player.rangedDamage += .18f;
            player.magicDamage += .18f;
            player.minionDamage += .18f;
            player.thrownDamage += .18f;
        }

        public override bool CanEquipAccessory(Player player, int slot)
        {
            if (slot < 10)
            {
                int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                {
                    if (slot != i && player.armor[i].type == mod.ItemType<DragonSerpentNecklace>())
                    {
                        return false;
                    }
                    if (slot != i && player.armor[i].type == mod.ItemType<StormCharm>())
                    {
                        return false;
                    }
                    if (slot != i && player.armor[i].type == mod.ItemType<Hydra.HydraPendant>())
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}