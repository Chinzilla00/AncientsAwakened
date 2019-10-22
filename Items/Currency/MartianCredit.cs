using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.Localization;

namespace AAMod.Items.Currency
{
    public class MartianCredit : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Martian Credit");
            Tooltip.SetDefault("A card that has some sort of monetary value to the martians");
        }


        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, Color.Cyan.ToVector3() * 0.55f * Main.essScale);
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.value = 1000;
            item.rare = 9;
        }
    }
    public class MCredit : CustomCurrencySingleCoin
    {
        public static Color color = Color.Cyan;

        public MCredit(int coinItemID) : base(coinItemID, 999L)
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
                price == 1 ? Language.GetTextValue("Mods.AAMod.Common.MartianCredit") : Language.GetTextValue("Mods.AAMod.Common.MartianCredits")
            });
        }
    }
}