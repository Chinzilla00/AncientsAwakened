using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Hydra
{
    [AutoloadEquip(EquipType.Neck)]
    public class HydraPendant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hydra Pendant");
            Tooltip.SetDefault(@"7% Increased damage");
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
            player.meleeDamage += .07f;
            player.rangedDamage += .07f;
            player.magicDamage += .07f;
            player.minionDamage += .07f;
            player.thrownDamage += .07f;
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
                    if (slot != i && player.armor[i].type == mod.ItemType<Orthrus.StormPendant>())
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}