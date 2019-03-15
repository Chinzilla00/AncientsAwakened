using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Tails
{
    [AutoloadEquip(EquipType.Legs)]
    public class FishDiverBoots : ModItem
    {
        public override void SetStaticDefaults()

        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Tails Leggings");
            Tooltip.SetDefault(@"Leggings made to resemble a certain two-tailed fox.
'Great for impersonating Ancients Awakened Devs!'");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(39, 115, 189);
                }
            }
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 18;
            item.rare = 9;
            item.vanity = true;
        }
    }
