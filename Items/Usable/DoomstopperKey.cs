
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Usable
{
    public class DoomstopperKey : ModItem
    {
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
                glowMasks[glowMasks.Length - 1] = mod.GetTexture("Items/Usable/" + GetType().Name + "_Glow");
                customGlowMask = (short)(glowMasks.Length - 1);
                Main.glowMaskTexture = glowMasks;
            }
            item.glowMask = customGlowMask;
            DisplayName.SetDefault("Doomstopper Chip");
			Tooltip.SetDefault("'Unlocks Doomsday Chests'");
		}


        public override void SetDefaults()
        {
            item.width = item.height = 16;
            item.rare = 6;
            item.maxStack = 99;
			item.value = 800000;
           // item.useStyle = 4;
           // item.useTime = item.useAnimation = 20;

            item.noMelee = true;
            //item.consumable = true;
            //item.autoReuse = false;

       //     item.UseSound = SoundID.Item43;
        }

       
    }
}
