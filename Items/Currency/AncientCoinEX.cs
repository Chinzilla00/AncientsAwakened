using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.UI;
using Terraria.Localization;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.Items.Currency
{
    public class AncientCoinEX : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ancient Medallion EX");
            Tooltip.SetDefault("A red and blue coin with an A engraved into it");
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, Main.DiscoColor.ToVector3() * 0.55f * Main.essScale);
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.rare = 11;
            item.expert = true;
            item.expertOnly = true;
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Texture2D itemTex = mod.GetTexture("Items/Currency/AncientCoinEX_Glow");

            Rectangle iframe = BaseMod.BaseDrawing.GetFrame(0, itemTex.Width, itemTex.Height, 0, 0);

            BaseMod.BaseDrawing.DrawTexture(spriteBatch, Main.itemTexture[item.type], 0, item.position, item.width, item.height, scale, rotation, item.direction, 1, iframe, lightColor, true);
            BaseMod.BaseDrawing.DrawTexture(spriteBatch, itemTex, 0, item.position, item.width, item.height, scale, rotation, item.direction, 1, iframe, lightColor, true);
            return false;
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D itemTex = mod.GetTexture("Items/Currency/AncientCoinEX_Glow");

            Rectangle iframe = BaseMod.BaseDrawing.GetFrame(0, itemTex.Width, itemTex.Height, 0, 0);

            BaseMod.BaseDrawing.DrawTexture(spriteBatch, Main.itemTexture[item.type], 0, item.position, item.width, item.height, scale, 0, item.direction, 1, iframe, drawColor, true);
            BaseMod.BaseDrawing.DrawTexture(spriteBatch, itemTex, 0, item.position, item.width, item.height, scale, 0, item.direction, 1, iframe, drawColor, true);
            return false;
        }
    }

    public class ACoinEX : CustomCurrencySingleCoin
    {
        public static Color color = Main.DiscoColor;

        public ACoinEX(int coinItemID) : base(coinItemID, 999L)
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
                price == 1 ? Language.GetTextValue("Mods.AAMod.Common.AncientCoinEX") : Language.GetTextValue("Mods.AAMod.Common.AncientCoinEXs")
            });
        }
    }
}