using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.Audio;
using System.Collections.Generic;

namespace AAMod.Items.Boss.EFish
{
    [AutoloadEquip(EquipType.Wings)]
    public class EFishWings : ModItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Emperor Fishron Wings");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.FishronWings);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.wingTimeMax = 600;
        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
            ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 0.85f;
            ascentWhenRising = 0.15f;
            maxCanAscendMultiplier = 1f;
            maxAscentMultiplier = 3f;
            constantAscend = 0.135f;
        }

        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            speed = 16f;
            acceleration *= 4.5f;
        }
    }
}