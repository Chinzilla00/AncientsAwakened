using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
                /*item.color = Color.White;
                if (WorldGen.crimson)
                {
                    item.color = Color.DarkRed;
                }
                else
                {
                    item.color = Color.Violet;
                }*/
            }
        }
    }
}
