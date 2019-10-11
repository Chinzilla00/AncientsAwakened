using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Hydra
{
    [AutoloadEquip(EquipType.Neck)]
    public class HydraPendant : BaseAAItem
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
            item.expert = true; item.expertOnly = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.allDamage += .07f;
        }

        public override bool CanEquipAccessory(Player player, int slot)
        {
            if (slot < 10)
            {
                int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                {
                    if (slot != i && player.armor[i].type == Terraria.ModLoader.ModContent.ItemType<DragonSerpentNecklace>())
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}