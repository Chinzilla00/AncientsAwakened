using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.Audio;
using System.Collections.Generic;

namespace AAMod.Items.Boss.EFish
{
    public class EFlairon : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Emperor Flairon");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.Flairon);
            item.damage = 160;
            item.rare = 11;
            item.shoot = mod.ProjectileType("EFlairon");
        }
    }
}