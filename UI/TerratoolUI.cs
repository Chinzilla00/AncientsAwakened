using AAMod.UI.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameInput;
using Terraria.UI;

namespace AAMod.UI
{
    internal abstract class TerratoolUI : ToggableUI
    {
        private static Vector2 circleCenter = Main.MouseScreen - new Vector2(20f, 20f);
        public static int[] selectedButtons = { -1, -1 };

        private List<UIColorImageButton> buttonList;
        private List<UIColorImage> buttonImageList;
        private Rectangle buttonFrontFrame;

        public abstract UserInterface Interface();
        public abstract UIState State();
        public abstract Texture2D ButtonImages();
        public abstract Texture2D ButtonOnImage();
        public abstract Texture2D ButtonOffImage();
        public abstract int HeldItemType();

        public virtual Color HoverColor()
        {
            return new Color(150, 150, 150);
        }

        public virtual Color NoHoverColor()
        {
            return new Color(80, 80, 80);
        }
        public virtual int ButtonAmount()
        {
            return 3;
        }
        public virtual float ButtonPadding()
        {
            return 5f;
        }
        public virtual float CircleRadius()
        {
            return 40f + ButtonPadding();
        }

        public override void OnInitialize()
        {
            buttonList = new List<UIColorImageButton>();
            buttonImageList = new List<UIColorImage>();
            buttonFrontFrame = ButtonImages().Frame(1, ButtonAmount());

            for (int i = 0; i < ButtonAmount(); i++)
            {
                buttonList.Add(new UIColorImageButton(ButtonOffImage(), NoHoverColor()));
                buttonImageList.Add(new UIColorImage(ButtonImages(), NoHoverColor(), buttonFrontFrame));

                double angle = (Math.PI * 2 * i) / ButtonAmount();
                double x = circleCenter.X + (CircleRadius() * Math.Cos(angle));
                double y = circleCenter.Y + (CircleRadius() * Math.Sin(angle));

                buttonList[i].Left.Set((float)x, 0f);
                buttonList[i].Top.Set((float)y, 0f);

                buttonFrontFrame.Y += buttonFrontFrame.Height;

                int index = i;
                buttonList[i].OnClick += (evt, element) => ButtonClicked(index);
                buttonList[i].OnMouseOver += (evt, element) => ButtonHover(index);
                buttonList[i].OnMouseOut += (evt, element) => ButtonLeave(index);

                buttonList[i].Append(buttonImageList[i]);
                Append(buttonList[i]);
            }
        }

        public override void ToggleUI(UserInterface userInterface, UIState state = null)
        {
            base.ToggleUI(userInterface, state);

            if (Visible)
            {
                PlayerInput.SetZoom_UI();
                UpdatePosition();
                PlayerInput.SetZoom_Unscaled();
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            for (int i = 0; i < buttonList.Count; i++)
            {
                if (buttonList[i].ContainsPoint(Main.MouseScreen))
                {
                    Main.LocalPlayer.mouseInterface = true;
                }
            }

            if (Main.LocalPlayer.HeldItem.type != HeldItemType() || Main.LocalPlayer.dead || Main.LocalPlayer.ghost)
            {
                for (int i = 0; i < buttonList.Count; i++)
                {
                    buttonList[i].SetImage(ButtonOffImage());

                    if (Array.IndexOf(selectedButtons, i) < 0)
                    {
                        buttonList[i].SetColor(NoHoverColor());
                        buttonImageList[i].SetColor(NoHoverColor());
                    }
                }

                base.ToggleUI(Interface(), State());
            }
        }

        public virtual void ButtonClicked(int index)
        {
            selectedButtons.SetValue(selectedButtons[0], selectedButtons[1]);
            selectedButtons.SetValue(index, selectedButtons[0]);

            for (int i = 0; i < buttonList.Count; i++)
            {
                buttonList[i].SetColor(NoHoverColor());
                buttonImageList[i].SetColor(NoHoverColor());
            }

            foreach (int value in selectedButtons)
            {
                if (value != -1)
                {
                    buttonList[index].SetColor(Color.White);
                    buttonImageList[index].SetColor(Color.White);
                }
            }
        }

        public virtual void ButtonHover(int index)
        {
            buttonList[index].SetImage(ButtonOnImage());

            if (Array.IndexOf(selectedButtons, index) < 0)
            {
                buttonList[index].SetColor(HoverColor());
                buttonImageList[index].SetColor(HoverColor());
            }
        }

        public virtual void ButtonLeave(int index)
        {
            buttonList[index].SetImage(ButtonOffImage());

            if (Array.IndexOf(selectedButtons, index) < 0)
            {
                buttonList[index].SetColor(NoHoverColor());
                buttonImageList[index].SetColor(NoHoverColor());
            }
        }

        public virtual void UpdatePosition()
        {
            circleCenter = Main.MouseScreen - new Vector2(20f, 20f);

            for (int i = 0; i < buttonList.Count; i++)
            {
                double angle = (Math.PI * 2 * i) / ButtonAmount();

                double x = circleCenter.X + (CircleRadius() * Math.Cos(angle));
                double y = circleCenter.Y + (CircleRadius() * Math.Sin(angle));

                buttonList[i].Left.Set((float)x, 0f);
                buttonList[i].Top.Set((float)y, 0f);
            }

            Recalculate();
        }
    }
}
