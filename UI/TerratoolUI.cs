using AAMod.Items.Tools;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.UI;

namespace AAMod.UI
{
    class TerratoolUI : UIState
    {
        public static bool visible = false;

        public static TerraToolUIAxe TerraToolUIToolAxe;
        public static TerraToolUIPick TerraToolUIToolPick;
        public static TerraToolUIHammer TerraToolUIToolHammer;

        public override void OnInitialize()
		{
            TerraToolUIToolAxe = new TerraToolUIAxe();
            TerraToolUIToolAxe.SetPadding(0);
            TerraToolUIToolAxe.Left.Set(40f, 0f);
            TerraToolUIToolAxe.Top.Set(280f, 0f);
            TerraToolUIToolAxe.Width.Set(40f, 0f);
            TerraToolUIToolAxe.Height.Set(40f, 0f);
            Append(TerraToolUIToolAxe);

            TerraToolUIToolPick = new TerraToolUIPick();
            TerraToolUIToolPick.SetPadding(0);
            TerraToolUIToolPick.Left.Set(80f, 0f);
            TerraToolUIToolPick.Top.Set(320f, 0f);
            TerraToolUIToolPick.Width.Set(40f, 0f);
            TerraToolUIToolPick.Height.Set(40f, 0f);
            Append(TerraToolUIToolPick);

            TerraToolUIToolHammer = new TerraToolUIHammer();
            TerraToolUIToolHammer.SetPadding(0);
            TerraToolUIToolHammer.Left.Set(120f, 0f);
            TerraToolUIToolHammer.Top.Set(280f, 0f);
            TerraToolUIToolHammer.Width.Set(40f, 0f);
            TerraToolUIToolHammer.Height.Set(40f, 0f);
            Append(TerraToolUIToolHammer);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
        }
    }

    internal class TerraToolUIAxe : UIElement
    {
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
            spriteBatch.Draw(ModLoader.GetTexture("AAMod/UI/Axe"), new Vector2(40f, 280f), null, Color.White, 0f, new Vector2(0.3f), 1f, SpriteEffects.None, 0f);
            if (TerratoolUI.TerraToolUIToolAxe.IsMouseHovering && Main.mouseLeft)
            {
                if (!Terratool.AxeBool)
                {
                    Terratool.AxeBool = true;
                }
                else
                {
                    Terratool.AxeBool = false;
                }
            }
            Utils.DrawBorderStringFourWay(spriteBatch, Main.fontItemStack, "", Main.mouseX - 40, Main.mouseY - 20, Color.White, Color.Black, new Vector2(0.3f));
        }
    }

    internal class TerraToolUIPick : UIElement
    {
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
            spriteBatch.Draw(ModLoader.GetTexture("AAMod/UI/Pick"), new Vector2(40f, 280f), null, Color.White, 0f, new Vector2(0.3f), 1f, SpriteEffects.None, 0f);
            if (TerratoolUI.TerraToolUIToolPick.IsMouseHovering && Main.mouseLeft)
            {
                if (!Terratool.PickBool)
                {
                    Terratool.PickBool = true;
                }
                else
                {
                    Terratool.PickBool = false;
                }
            }
            Utils.DrawBorderStringFourWay(spriteBatch, Main.fontItemStack, "", Main.mouseX, Main.mouseY + 20, Color.White, Color.Black, new Vector2(0.3f));
        }
    }

    internal class TerraToolUIHammer : UIElement
    {
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
            spriteBatch.Draw(ModLoader.GetTexture("AAMod/UI/Hammer"), new Vector2(40f, 280f), null, Color.White, 0f, new Vector2(0.3f), 1f, SpriteEffects.None, 0f);
            if (TerratoolUI.TerraToolUIToolHammer.IsMouseHovering && Main.mouseLeft)
            {
                if (!Terratool.HammerBool)
                {
                    Terratool.HammerBool = true;
                }
                else
                {
                    Terratool.HammerBool = false;
                }
            }
            Utils.DrawBorderStringFourWay(spriteBatch, Main.fontItemStack, "", Main.mouseX + 40, Main.mouseY - 20, Color.White, Color.Black, new Vector2(0.3f));
        }
    }
}
