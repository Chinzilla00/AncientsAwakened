using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace AAMod.Items.Materials
{
    public class DragonFire : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon's Fire");
            Tooltip.SetDefault("It's really really hot.");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 4));
        }
        public override void SetDefaults()
        {
            Item refItem = new Item();
            refItem.SetDefaults(ItemID.Ichor);
            item.width = refItem.width;
            item.height = refItem.height;
            item.maxStack = refItem.maxStack;
            item.value = refItem.value;
            item.rare = refItem.rare;
            item.alpha = 40;
        }
    }
}