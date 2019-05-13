using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework; using Microsoft.Xna.Framework.Graphics; using Terraria.ModLoader;

namespace AAMod.Items.Materials
{
    public class Prism : ModItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Prism");
            Tooltip.SetDefault("Shines with the colors of all the gems");
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 22;
			item.maxStack = 99;
            item.rare = 3;
            item.value = 1000;
        }
    }
}
