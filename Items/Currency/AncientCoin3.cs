using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.Localization;

namespace AAMod.Items.Currency
{
    public class AncientCoin3 : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ancient Medalion");
            Tooltip.SetDefault("A violet coin with an A engraved into it");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.rare = 9;
        }
    }

    public class ACoin3 : CustomCurrencySingleCoin
    {
        public static Color color = Color.Purple;

        public ACoin3(int coinItemID) : base(coinItemID, 999L)
        {
        }

        public override void GetPriceText(string[] lines, ref int currentLine, int price)
        {
            Color color2 = color * (Main.mouseTextColor / 255f);
            lines[currentLine++] = string.Format("[c/{0:X2}{1:X2}{2:X2}:{3} {4} {5}]", new object[]
            {
                color2.R,
                color2.G,
                color2.B,
                Language.GetTextValue("Mods.AAMod.Common.PlayerBuyPrice"),
                price,
                price == 1 ? "Ancient Coin" : "s"
            });
        }
    }
}