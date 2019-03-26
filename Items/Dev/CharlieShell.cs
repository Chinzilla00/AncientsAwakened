using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Dev
{
    [AutoloadEquip(EquipType.Shield)]
    public class CharlieShell : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spiked Turtle Shell");
            Tooltip.SetDefault(
@"Allows you to dash into enemies, damaging them
Enemies that hit you take full damage
'I. Will. Smite. You.'
-Charlie");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 30;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.accessory = true;
            item.defense = 15;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(18, 89, 24);
                }
            }
        }
        
		public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.thorns = 1f;
            player.turtleThorns = true;
            player.dash = 3;
        }
    }
}