using Terraria;

namespace AAMod.Items.Boss.Serpent
{
    public class ArcticMedallion : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Arctic Medallion");
            Tooltip.SetDefault(@"Doubles your stats during a Blizzard");
        }
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 50;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.accessory = true;
            item.expert = true; item.expertOnly = true;
        }

        public override void UpdateEquip(Player p)
        {
			if(p.ZoneRain && p.ZoneSnow)
			{
				p.meleeDamage *= 2f;
				p.rangedDamage *= 2f;
				p.magicDamage *= 2f;
				p.minionDamage *= 2f;
				p.thrownDamage *= 2f;
				p.meleeCrit *= 2;
				p.rangedCrit *= 2;
				p.magicCrit += 2;
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
                    if (slot != i && player.armor[i].type == ModContent.ItemType<FireFrostMedallion>())
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}