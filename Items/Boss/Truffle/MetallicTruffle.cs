using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Truffle
{
    public class MetallicTruffle : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Metallic Truffle");
            Tooltip.SetDefault(@"Don't bite it.");
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