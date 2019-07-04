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

            switch (selectedButtons[0])
            {
                case 0:
                    Pick = 215;
                    if (selectedButtons[1] != -1)
                    {
                        Hammer = selectedButtons[1] == 1 ? 120 : 0;
                        Axe = selectedButtons[1] == 2 ? 60 : 0;
                    }
                    break;
                case 1:
                    Hammer = 120;
                    if (selectedButtons[1] != -1)
                    {
                        Pick = selectedButtons[1] == 0 ? 215 : 0;
                        Axe = selectedButtons[1] == 2 ? 60 : 0;
                    }
                    break;
                case 2:
                    Axe = 50;
                    if (selectedButtons[1] != -1)
                    {
                        Pick = selectedButtons[1] == 0 ? 215 : 0;
                        Hammer = selectedButtons[1] == 1 ? 120 : 0;
                    }
                    break;
            }
        }
    }
}
