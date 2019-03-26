using Terraria;
using Microsoft.Xna.Framework; using Microsoft.Xna.Framework.Graphics; using Terraria.ModLoader;

namespace AAMod.Items.Boss.AH
{
    public class HeartOfPassion : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Heart of Passion");
            Tooltip.SetDefault(@"Your magic attacks and minions grow stronger the less health you have
Magic attacks and Minions inflict 'On Fire!'
Below 2/3 of your maximum life, Your mana regenerates much faster
Below 1/3 of your maximum life, your magic attacks and minions inflict Daybroken instead of 'On Fire'");
        }
        public override void SetDefaults()
        {
            item.width = 66;
            item.height = 78;
            item.value = Item.sellPrice(0, 40, 0, 0);
            item.rare = 11;
            item.accessory = true;
            item.expert = true;
            item.defense = 3;
        }
        

        public override void UpdateEquip(Player player)
        {
            player.magicDamage += (1 - player.statLife / player.statLifeMax);
            player.minionDamage += (1 - player.statLife / player.statLifeMax);
            player.GetModPlayer<AAPlayer>(mod).HeartP = true;

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
                    if (slot != i && player.armor[i].type == mod.ItemType<HeartOfSorrow>())
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}