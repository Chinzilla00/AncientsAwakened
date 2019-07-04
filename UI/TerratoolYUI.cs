using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using AAMod.Items.Boss.Yamata;

namespace AAMod.UI
{
    internal sealed class TerratoolYUI : TerratoolUI
    {
        public static int Pick = 0;

        public static int Hammer = 0;

        public static int Axe = 0;

        public override Texture2D ButtonImages() { return AAMod.instance.GetTexture("UI/Tools/ToolUIY"); }

        public override Texture2D ButtonOnImage() { return AAMod.instance.GetTexture("UI/Tools/ToolButtonY"); }

        public override Texture2D ButtonOffImage() { return AAMod.instance.GetTexture("UI/Tools/ToolButtonYOff"); }

        public override int HeldItemType() { return AAMod.instance.ItemType<YamataTerratool>(); }

        public override UserInterface Interface() { return AAMod.instance.TerratoolYInterface; }

        public override UIState State() { return AAMod.instance.TerratoolYState; }

        public override void ButtonClicked(int index)
        {
            base.ButtonClicked(index);
            Pick = selectedButtons.Contains(0) ? 300 : 0;
            Hammer = selectedButtons.Contains(1) ? 200 : 0;
            Axe = selectedButtons.Contains(2) ? 60 : 0;
        }
    }
}
