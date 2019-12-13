using Terraria;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Items.FishingItem
{
    public class SharpeningLavaFish : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sharpening Lava Fish");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.rare = 9;
            AARarity = 13;
            item.useStyle = 1;
            item.consumable = true;
            item.value = 128000;
            item.createTile = mod.TileType("SharpeningLavaFishTile");
        }
    }
}
