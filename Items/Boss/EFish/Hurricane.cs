using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.Audio;
using System.Collections.Generic;

namespace AAMod.Items.Boss.EFish
{
    public class Hurricane : ModItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hurricane");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.Tsunami);
            item.damage = 130;
            item.rare = 11;
        }
    }
}