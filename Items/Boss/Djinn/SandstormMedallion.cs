using Terraria;

namespace AAMod.Items.Boss.Djinn
{
    public class SandstormMedallion : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sandstorm Medallion");
            Tooltip.SetDefault(@"Doubles your stats during a Sandstorm");
        }
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 50;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.accessory = true;
            item.expert = true;
        }

        public override void UpdateEquip(Player p)
        {
			if(p.ZoneSandstorm)
			{
				p.meleeDamage += 1f;
				p.rangedDamage += 1f;
				p.magicDamage += 1f;
				p.minionDamage += 1f;
				p.thrownDamage += 1f;
				p.meleeCrit *= 2;
				p.rangedCrit *= 2;
				p.magicCrit *= 2;
				p.thrownCrit *= 2;
			}
        }
        public override bool CanEquipAccessory(Player player, int slot)
        {
            if (slot < 10)
            {
                int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                {
                    if (slot != i && player.armor[i].type == mod.ItemType<FireFrostMedallion>())
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}