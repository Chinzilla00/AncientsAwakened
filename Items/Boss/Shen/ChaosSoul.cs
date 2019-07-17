using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace AAMod.Items.Boss.Shen
{
    public class ChaosSoul : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chaos Soul");
            Tooltip.SetDefault("Solid discord, symbolizing unrest and Anarchy itself");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(4, 4));
            ItemID.Sets.ItemNoGravity[item.type] = true;

        }

        // TODO -- Velocity Y smaller, post NewItem?
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 24;
            item.maxStack = 999;
            item.value = Item.sellPrice(0, 30, 0, 0);
            item.rare = 9;
            item.expert = true; item.expertOnly = true;
            item.alpha = 25;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
    }
}