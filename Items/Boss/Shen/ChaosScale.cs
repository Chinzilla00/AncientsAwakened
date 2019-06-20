using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Shen
{
    public class ChaosScale : BaseAAItem
    {
        public short customGlowMask = 0;
        public override void SetStaticDefaults()
        {
            if (Main.netMode != 2)
            {
                Texture2D[] glowMasks = new Texture2D[Main.glowMaskTexture.Length + 1];
                for (int i = 0; i < Main.glowMaskTexture.Length; i++)
                {
                    glowMasks[i] = Main.glowMaskTexture[i];
                }
                glowMasks[glowMasks.Length - 1] = mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
                customGlowMask = (short)(glowMasks.Length - 1);
                Main.glowMaskTexture = glowMasks;
            }
            DisplayName.SetDefault("Chaos Scale");
            Tooltip.SetDefault("Chaos radiates from this blazing scale");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(4, 6));
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity14;
                }
            }
        }

        // TODO -- Velocity Y smaller, post NewItem?
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 42;
            item.maxStack = 999;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 11;
            item.glowMask = customGlowMask;
        }

        // The following 2 methods are purely to show off these 2 hooks. Don't use them in your own code.


        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, Color.Indigo.ToVector3() * 0.55f * Main.essScale);
        }
    }
}