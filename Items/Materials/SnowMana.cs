using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Materials
{
    public class SnowMana : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Snow Mana");
            // ticksperframe, frameCount
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 3));
            ItemID.Sets.ItemIconPulse[item.type] = true;
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
            item.value = 1000;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, Color.Cyan.ToVector3() * 0.55f * Main.essScale);
        }
    }
}