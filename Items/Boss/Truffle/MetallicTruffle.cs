using Terraria;
using Terraria.DataStructures;

namespace AAMod.Items.Boss.Truffle
{
    public class MetallicTruffle : BaseAAItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Metallic Truffle");
            Tooltip.SetDefault(@"Don't bite it.");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 4));
        }


        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 26;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = 1;
            item.accessory = true;
            item.expert = true;
            item.defense = 8;
        }
    }
}