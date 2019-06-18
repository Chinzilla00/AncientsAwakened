using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Materials
{
    public class OceanWhisper : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ocean Whisper");
            Tooltip.SetDefault("Sounds of the soft ocean waves eminate from this enchanted aura");
            // ticksperframe, frameCount
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(7, 5));
            ItemID.Sets.ItemIconPulse[item.type] = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        // TODO -- Velocity Y smaller, post NewItem?
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.value = 1000;
            item.rare = 7;
            item.alpha = 80;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, AAColor.Ocean.ToVector3() * 0.55f * Main.essScale);
        }
    }
}