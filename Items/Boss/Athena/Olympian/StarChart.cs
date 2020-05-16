using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace AAMod.Items.Boss.Athena.Olympian
{
    public class StarChart : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Star Chart");
            Tooltip.SetDefault("A map of the solar system up to this planet.");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(4, 8));
        }

        public override void SetDefaults()
        {
            item.width = 10;
            item.height = 10;
            item.maxStack = 999;
            item.value = 10000;
            item.rare = ItemRarityID.Purple;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, Color.White.ToVector3() * 0.55f * Main.essScale);
        }
    }
}