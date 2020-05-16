using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;

namespace AAMod.Items.Vanity.Cerberus
{
    [AutoloadEquip(EquipType.Body)]
    public class InvokerRobe : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Invoker Robe");
            Tooltip.SetDefault(@"The enchanted robe of Aleister the 'Mega Therion'
Great for impersonating Awakened Devs!");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = Color.Gold;
                }
            }
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 20;
            item.rare = ItemRarityID.Cyan;
            item.vanity = true;
        }
    }
}