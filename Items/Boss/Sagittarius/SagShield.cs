using Terraria;
using Microsoft.Xna.Framework; 
using Microsoft.Xna.Framework.Graphics; 
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Sagittarius
{
    public class SagShield : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sagittarius Shield");
            Tooltip.SetDefault(@"Pressing the ability hotkey puts up a barrier around you to protect you from damage
While shielded, you cannot use items
While shielded, your health regeneration is increated dramatically
Shield lasts for 5 seconds
Shield has a 5 minute cooldown");
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
            p.GetModPlayer<AAPlayer>(mod).SagShield = true;
        }
    }
}