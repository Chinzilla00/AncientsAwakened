using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.Items.Vanity.Grox
{
	[AutoloadEquip(EquipType.Body)]
	public class AncientGroviteCofferplate : ModItem
    {
        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawShadow = true;
			player.armorEffectDrawOutlines = true;
			player.armorEffectDrawShadowSubtle = true;
        }

		public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ancient Grovite Cofferplate");
            BaseUtility.AddTooltips(item, new string[] { "'A chestpiece made of a plantlike material not of this world'" });
        }		

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.rare = 2;
            item.vanity = true;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(31, 77, 37);
                }
            }
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return BasePlayer.IsInSet(mod, "Ancient Grovite Grovite", head, body, legs);
        }

        public override void UpdateArmorSet(Player p)
        {
            BaseMod.BaseDrawing.AddLight(p.Center, AAPlayer.groviteColor, 0.5f);
            p.statLifeMax2 += 100;
            AAPlayer.groviteGlow[p.whoAmI] = true;
            if (Main.netMode != 2 && Main.rand.Next(4) == 0)
            {
                DustHandler.SpawnSpecialDust(p.position, p.width, p.height, new Vector2(Main.rand.Next(-10, 10), Main.rand.Next(-10, 10)), 1f, mod.GetTexture("dust_Grovite"), AAPlayer.groviteColor, 6);
            }
		}
	}
}