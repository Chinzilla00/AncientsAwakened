using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using AAMod.Items.Boss.Shen;

namespace AAMod.UI
{
    internal sealed class TerratoolSUI : TerratoolUI
    {
        public static int Pick = 0;

        public static int Hammer = 0;

        public static int Axe = 0;

        public override Texture2D ButtonImages() { return AAMod.instance.GetTexture("UI/Tools/ToolUIS"); }

        public override Texture2D ButtonOnImage() { return AAMod.instance.GetTexture("UI/Tools/ToolButtonS"); }

        public override Texture2D ButtonOffImage() { return AAMod.instance.GetTexture("UI/Tools/ToolButtonSOff"); }

        public override int HeldItemType() { return AAMod.instance.ItemType<ShenTerratool>(); }

        public override UserInterface Interface() { return AAMod.instance.TerratoolSInterface; }

        public override UIState State() { return AAMod.instance.TerratoolSState; }

        public override void ButtonClicked(int index)
        {
            base.ButtonClicked(index);

            switch (selectedButtons[0])
            {
                case 0:
                    Pick = 320;
                    if (selectedButtons[1] != -1)
                    {
                        Hammer = selectedButtons[1] == 1 ? 250 : 0;
                        Axe = selectedButtons[1] == 2 ? 70 : 0;
                    }
                    break;
                case 1:
                    Hammer = 250;
                    if (selectedButtons[1] != -1)
                    {
                        Pick = selectedButtons[1] == 0 ? 300 : 0;
                        Axe = selectedButtons[1] == 2 ? 70 : 0;
                    }
                    break;
                case 2:
                    Axe = 70;
                    if (selectedButtons[1] != -1)
                    {
                        Pick = selectedButtons[1] == 0 ? 300 : 0;
                        Hammer = selectedButtons[1] == 1 ? 250 : 0;
                    }
                    break;
            }
        }
    }
}
