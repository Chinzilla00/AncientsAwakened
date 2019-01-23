using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework; using Microsoft.Xna.Framework.Graphics; using Terraria.ModLoader;
using BaseMod;

namespace AAMod.Items.Boss
{
    public class RiftSphere : ModItem
    {
        internal static int type;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Reality Sphere");
            Tooltip.SetDefault("It's not of this world...");
            // ticksperframe, frameCount
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(4, 6));
            ItemID.Sets.ItemNoGravity[item.type] = true;

        }

        // TODO -- Velocity Y smaller, post NewItem?
        public override void SetDefaults()
        {
            Item refItem = new Item();
            refItem.SetDefaults(ItemID.SoulofSight);
            item.width = refItem.width;
            item.height = refItem.height;
            item.maxStack = 999;
            item.value = 1000000;
            item.rare = 11;
            item.expert = true;
            
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White; //GConstants.COLOR_RARITYN1;
        }


        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, Color.LightSeaGreen.ToVector3() * 0.55f * Main.essScale);
        }
    }
}