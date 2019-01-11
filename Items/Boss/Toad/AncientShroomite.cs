using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Toad
{
    public class AncientShroomite : ModItem
    {
        public override void SetDefaults()
        {

            item.width = 32;
            item.height = 30;
            item.maxStack = 99;
			
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ancient Shroomite");
            Tooltip.SetDefault("Like regular shroomite but ooooooooooold");
        }
    }
}
