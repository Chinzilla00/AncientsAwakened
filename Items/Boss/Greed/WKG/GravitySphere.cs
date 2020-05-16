using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace AAMod.Items.Boss.Greed.WKG
{
    public class GravitySphere : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gravity Sphere");
            Tooltip.SetDefault("A stone model of the planet, complete with an orbitting moon!");
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
    }
}