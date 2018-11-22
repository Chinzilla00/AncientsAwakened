using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Broodmother
{
    public class BroodScale : ModItem
    {
        public override void SetDefaults()
        {

            item.width = 22;
            item.height = 24;
            item.maxStack = 99;
            item.rare = 1;
			
        }

        public static short customGlowMask = 0;
        public override void SetStaticDefaults()
        {
            if (Main.netMode != 2)
            {
                Microsoft.Xna.Framework.Graphics.Texture2D[] glowMasks = new Microsoft.Xna.Framework.Graphics.Texture2D[Main.glowMaskTexture.Length + 1];
                for (int i = 0; i < Main.glowMaskTexture.Length; i++)
                {
                    glowMasks[i] = Main.glowMaskTexture[i];
                }
                glowMasks[glowMasks.Length - 1] = mod.GetTexture("Items/Boss/Broodmother/" + GetType().Name + "_Glow");
                customGlowMask = (short)(glowMasks.Length - 1);
                Main.glowMaskTexture = glowMasks;
            }
            item.glowMask = customGlowMask;
            DisplayName.SetDefault("Dragon Scale");
            Tooltip.SetDefault("The scale of a formidable foe");
        }
    }
}
