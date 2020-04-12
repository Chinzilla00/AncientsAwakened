using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Athena
{
    public class StormSphere : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Storm Sphere");
            Tooltip.SetDefault("A supercharged crystal sphere of Varian Energy");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(4, 5));
        }

        public override void SetDefaults()
        {
            item.width = 10;
            item.height = 10;
            item.maxStack = 999;
            item.value = 10000;
            item.rare = 9;
            AARarity = 12;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity12;
                }
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, Color.SkyBlue.ToVector3() * 0.55f * Main.essScale);
        }
    }
}