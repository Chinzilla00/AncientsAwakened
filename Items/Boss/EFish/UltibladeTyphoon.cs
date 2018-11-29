using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.Audio;
using System.Collections.Generic;

namespace AAMod.Items.Boss.EFish
{
    public class UltibladeTyphoon : ModItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ultiblade Typhoon");
        }
        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.RazorbladeTyphoon);
            item.damage = 170;
            item.rare = 11;
            item.shoot = mod.ProjectileType("Typhoon");
        }
    }
}