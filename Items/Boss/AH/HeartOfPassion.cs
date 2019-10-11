using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.AH
{
    public class HeartOfPassion : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Heart of Passion");
            Tooltip.SetDefault(@"Your magic attacks and minions grow stronger the less health you have
Magic attacks and Minions inflict Dragonfire
Below 2/3 of your maximum life, Your mana regenerates much faster
Below 1/3 of your maximum life, your magic attacks and minions inflict Daybroken instead of 'On Fire'");
        }
        public override void SetDefaults()
        {
            item.width = 66;
            item.height = 78;
            item.value = Item.sellPrice(0, 10, 0, 0);
            item.rare = 11;
            item.accessory = true;
            item.expert = true; item.expertOnly = true;
            item.defense = 3;
        }
        

        public override void UpdateEquip(Player player)
        {
            player.magicDamage += 1 - player.statLife / player.statLifeMax;
            player.minionDamage += 1 - player.statLife / player.statLifeMax;
            player.GetModPlayer<AAPlayer>().HeartP = true;

            if (player.statLife > (player.statLifeMax * (2/3)))
            {
                player.manaRegenBonus += 6;
            }
        }

        public override bool CanEquipAccessory(Player player, int slot)
        {
            if (slot < 10)
            {
                int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                {
                    if (slot != i && player.armor[i].type == ModContent.ItemType<HeartOfSorrow>())
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity12;
                }
            }
        }
    }
}