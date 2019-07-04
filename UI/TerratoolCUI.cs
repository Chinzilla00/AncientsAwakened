using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using AAMod.Items.Tools;

namespace AAMod.UI
{
    internal sealed class TerratoolCUI : TerratoolUI
    {
        public static int Pick = 0;

        public static int Hammer = 0;

        public static int Axe = 0;

        public override Texture2D ButtonImages() { return AAMod.instance.GetTexture("UI/Tools/ToolUIC"); }

        public override Texture2D ButtonOnImage() { return AAMod.instance.GetTexture("UI/Tools/ToolButtonC"); }

        public override Texture2D ButtonOffImage() { return AAMod.instance.GetTexture("UI/Tools/ToolButtonCOff"); }

        public override int HeldItemType() { return AAMod.instance.ItemType<ChaosTerratool>(); }

        public override UserInterface Interface() { return AAMod.instance.TerratoolCInterface; }

        public override UIState State() { return AAMod.instance.TerratoolCState; }

        public override void ButtonClicked(int index)
        {
            base.ButtonClicked(index);
            Pick = selectedButtons.Contains(0) ? 215 : 0;
            Hammer = selectedButtons.Contains(1) ? 120 : 0;
            Axe = selectedButtons.Contains(2) ? 50 : 0;
        }
    }
}
