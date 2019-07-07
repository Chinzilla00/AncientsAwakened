using BaseMod;
using Microsoft.Xna.Framework;
using Terraria;

namespace AAMod.Items.Boss.MushroomMonarch
{
    public class GlowingMushium : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.rare = 1;
            item.value = Terraria.Item.sellPrice(0, 0, 3, 0);
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Glowing Mushium");
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return BaseUtility.MultiLerpColor((Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.White, lightColor, lightColor, Color.White);
        }
    }
}
