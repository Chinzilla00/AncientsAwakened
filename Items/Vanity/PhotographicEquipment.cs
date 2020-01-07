using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity
{

    [AutoloadEquip(EquipType.Wings)]
    public class PhotographicEquipment : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Photographic Equipment");
            Tooltip.SetDefault(@"Are you a photographer?");
        }

        public override void SetDefaults()
        {
            item.width = 42;
            item.height = 42;
            item.value = 500000;
            item.rare = 11;
            item.accessory = true;
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

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.wingTimeMax = 300;
        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
            ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 0.85f;
            ascentWhenRising = 0.15f;
            maxCanAscendMultiplier = 1f;
            maxAscentMultiplier = 1f;
            constantAscend = 1f;
        }

        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            if (player.controlDown && player.controlJump && player.wingTime > 0f)
            {
                speed = 15f;
                acceleration *= 10f;
            }
            else
            {
                speed = 10f;
                acceleration *= 6.25f;
            }
        }

        public override bool WingUpdate(Player player, bool inUse)
        {
            if (player.controlDown && player.controlJump && player.wingTime > 0f && !player.merman)
            {
                player.velocity.Y *= 0.01f;
                if (player.velocity.Y > -2f && player.velocity.Y < 1f)
                {
                    player.velocity.Y = 1E-05f;
                }
            }

            if(player.wingTime <= 1)
            {
                player.wingTime ++;
            }

            if (inUse)
            {
                if (player.controlJump && player.wingTime <= 0)
                {
                    player.wingFrame = 2;
                }
                player.wingFrameCounter++;
                if (player.wingFrameCounter > 6)
                {
                    player.wingFrame++;
                    player.wingFrameCounter = 0;
                    if (player.wingFrame >= 3)
                    {
                        player.wingFrame = 1;
                    }
                }
            }
            else
            {
                player.wingFrame = 0;
            }
            return true;
        }
    }
}
