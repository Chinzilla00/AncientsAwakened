using Terraria;

namespace AAMod.Items.Boss.Sagittarius
{
    public class SagShield : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sagittarius Shield");
            Tooltip.SetDefault(@"Pressing the ability hotkey puts up a barrier around you to protect you from damage
While shielded, you cannot use items
While shielded, your health regeneration is increased dramatically
Shield lasts for 5 seconds
Shield has a 5 minute cooldown");
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
            p.GetModPlayer<AAPlayer>().SagShield = true;
        }
    }
}