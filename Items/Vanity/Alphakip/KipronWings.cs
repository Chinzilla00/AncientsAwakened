using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Alphakip
{

    [AutoloadEquip(EquipType.Wings)]
    public class KipronWings : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Kipron Wings");
            Tooltip.SetDefault(@"Allows flight and slow fall
Hold down and jump to hover for an extended period of time
'Great for impersonating Ancients Awakened Devs!'");
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
			maxAscentMultiplier = 3f;
			constantAscend = 0.135f;
        }

		public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
		{
            if (player.controlDown && player.controlJump && player.wingTime > 0f)
            {
                speed = 20f;
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
            if (BaseMod.BasePlayer.HasAccessory(player, mod.ItemType<KipronWings>(), true, false))
            {
                if (player.controlDown && player.controlJump && player.wingTime > 0f && !player.merman)
                {
                    player.velocity.Y *= 0.7f;
                    if (player.velocity.Y > -2f && player.velocity.Y < 1f)
                    {
                        player.velocity.Y = 1E-05f;
                    }
                    player.armorEffectDrawShadowEOCShield = true;
                }
                else
                {
                    player.armorEffectDrawShadowEOCShield = false;
                }
            }
            return false;
        }
    }
}
