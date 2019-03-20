using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod
{
    public class AAModGlobalItem : GlobalItem
	{
        public override void SetDefaults(Item item)
        {
            if (item.type == ItemID.SoulofNight)
            {
                if (WorldGen.crimson)
                {
                    item.color = Color.Firebrick;
                }
                else
                {
                    item.color = Color.Violet;
                }
            }
            if (item.type == ItemID.LunarOre)
            {
                item.createTile = mod.TileType("LuminiteOre");
            }
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (item.type == ItemID.AnkhShield)
            {
                player.meleeSpeed += 0.07f;
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.type == ItemID.AnkhShield)
            {
                int index = 1;
                for (int m = 0; m < tooltips.Count; m++)
                {
                    TooltipLine line = tooltips[m];
                    if (line.mod == "Terraria" && line.text == "Grants immunity to most debuffs")
                    {
                        index = m;
                        break;
                    }
                }
                tooltips.Insert(index + 1, new TooltipLine(mod, "AnkhShield", "7% melee speed"));
            }
        }
    }
}
