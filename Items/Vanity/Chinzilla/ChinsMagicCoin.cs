using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace AAMod.Items.Vanity.Chinzilla
{
    [AutoloadEquip(EquipType.Wings)]
    public class ChinsMagicCoin : ModItem
	{
        public int wing = 1;

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Chinzilla00's Coin Barrier");
			Tooltip.SetDefault("'Great for impersonating AA devs!'");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 8));
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(0, 128, 64);
                }
            }
        }
        public override void SetDefaults()
		{
            item.width = 22;
			item.height = 28;
			item.rare = 10;
            item.accessory = true;
            item.value = 500000;
        }

        public override void UpdateVanity(Player player, EquipType type)
        {
            if (player.HasItem(ItemID.PlatinumCoin))
            {
                Main.flyingCarpetTexture = mod.GetTexture("Items/Vanity/Chinzilla/Platinum_Wings");
            }
            else if (player.HasItem(ItemID.GoldCoin))
            {
                Main.flyingCarpetTexture = mod.GetTexture("Items/Vanity/Chinzilla/Gold_Wings");
            }
            else if (player.HasItem(ItemID.SilverCoin))
            {
                Main.flyingCarpetTexture = mod.GetTexture("Items/Vanity/Chinzilla/Silver_Wings");
            }
            else if (player.HasItem(ItemID.CopperCoin))
            {
                Main.flyingCarpetTexture = mod.GetTexture("Items/Vanity/Chinzilla/Copper_Wings");
            }
            else
            {
                Main.flyingCarpetTexture = mod.GetTexture("Items/Vanity/Chinzilla/ChinsMagicCoin_Wings");
            }
        }

        public override bool WingUpdate(Player player, bool inUse)
        {
            if (inUse)
            {
                player.wingFrame = -1;
            }
            if (player.wingFrame != 0 && player.wingFrame != 1)
            {
                if (wing == 1)
                {
                    player.carpetFrame = 1;
                    if (++player.carpetFrameCounter >= 5)
                    {
                        player.carpetFrameCounter = 0;
                        wing = 2;
                    }
                }
                if (wing == 2)
                {
                    player.carpetFrame = 2;
                    if (++player.carpetFrameCounter >= 5)
                    {
                        player.carpetFrameCounter = 0;
                        wing = 3;
                    }
                }
                if (wing == 3)
                {
                    player.carpetFrame = 3;
                    if (++player.carpetFrameCounter >= 5)
                    {
                        player.carpetFrameCounter = 0;
                        wing = 4;
                    }
                }
                if (wing == 4)
                {
                    player.carpetFrame = 0;
                    if (++player.carpetFrameCounter >= 5)
                    {
                        player.carpetFrameCounter = 0;
                        wing = 1;
                    }
                }
            }
            return false;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.HasItem(ItemID.PlatinumCoin))
            {
                player.wingTimeMax = 400;
            }
            else if (player.HasItem(ItemID.GoldCoin))
            {
                player.wingTimeMax = 300;
            }
            else if (player.HasItem(ItemID.SilverCoin))
            {
                player.wingTimeMax = 200;
            }
            else if (player.HasItem(ItemID.CopperCoin))
            {
                player.wingTimeMax = 100;
            }
            else
            {
                player.wingTimeMax = -1;
            }
        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
            ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            if (player.HasItem(ItemID.PlatinumCoin))
            {
                ascentWhenFalling = 0.90f;
                ascentWhenRising = 0.2f;
                maxCanAscendMultiplier = 1.5f;
                maxAscentMultiplier = 4f;
                constantAscend = 0.165f;
            }
            else if (player.HasItem(ItemID.GoldCoin))
            {
                ascentWhenFalling = 0.85f;
                ascentWhenRising = 0.15f;
                maxCanAscendMultiplier = 1f;
                maxAscentMultiplier = 3f;
                constantAscend = 0.135f;
            }
            else if (player.HasItem(ItemID.SilverCoin))
            {
                ascentWhenFalling = 0.80f;
                ascentWhenRising = 0.1f;
                maxCanAscendMultiplier = 0.5f;
                maxAscentMultiplier = 2f;
                constantAscend = 0.115f;
            }
            else if (player.HasItem(ItemID.CopperCoin))
            {
                ascentWhenFalling = 0.75f;
                ascentWhenRising = 0.05f;
                maxCanAscendMultiplier = 0.25f;
                maxAscentMultiplier = 1f;
                constantAscend = 0.1f;
            }
            else
            {
                ascentWhenFalling = 0f;
                ascentWhenRising = 0f;
                maxCanAscendMultiplier = 0f;
                maxAscentMultiplier = 0f;
                constantAscend = 0f;
            }
        }

        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            if (player.HasItem(ItemID.PlatinumCoin))
            {
                speed = 14f;
                acceleration *= 3f;
            }
            else if (player.HasItem(ItemID.GoldCoin))
            {
                speed = 13f;
                acceleration *= 2.5f;
            }
            else if (player.HasItem(ItemID.SilverCoin))
            {
                speed = 12f;
                acceleration *= 2f;
            }
            else if (player.HasItem(ItemID.CopperCoin))
            {
                speed = 11f;
                acceleration *= 1.5f;
            }
            else
            {
                speed = 10f;
                acceleration *= 1f;
            }
        }
    }
}