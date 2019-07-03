using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameInput;
using Terraria.UI;

namespace GadgetGalore.UI.Core
{
    internal abstract class RadialUI : ToggableUI
    {
        private static Vector2 circleCenter = Main.MouseScreen - new Vector2(20f, 20f);
        private static int selectedButton = -1;

        private List<UIColorImageButton> buttonList;
        private List<UIColorImage> buttonImageList;
        private Rectangle buttonFrontFrame;

        public abstract UserInterface Interface { get; }

        public abstract UIState State { get; }

        public abstract Texture2D ButtonImages { get; }

        public abstract int ButtonAmount { get; }

        public abstract int HeldItemType { get; }

        public virtual Color HoverColor => new Color(150, 150, 150);

        public virtual Color NoHoverColor => new Color(80, 80, 80);

        public virtual float ButtonPadding => 5f;

        public virtual float CircleRadius => 40f + ButtonPadding;

        public override void OnInitialize()
        {
            buttonList = new List<UIColorImageButton>();
            buttonImageList = new List<UIColorImage>();
            buttonFrontFrame = ButtonImages.Frame(1, ButtonAmount);

            for (int i = 0; i < ButtonAmount; i++)
            {
                buttonList.Add(new UIColorImageButton(Main.wireUITexture[0], NoHoverColor));
                buttonImageList.Add(new UIColorImage(ButtonImages, NoHoverColor, buttonFrontFrame));

                double angle = (Math.PI * 2 * i) / ButtonAmount;
                double x = circleCenter.X + (CircleRadius * Math.Cos(angle));
                double y = circleCenter.Y + (CircleRadius * Math.Sin(angle));

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

            if (Main.LocalPlayer.HeldItem.type != HeldItemType || Main.LocalPlayer.dead || Main.LocalPlayer.ghost)
            {
                for (int i = 0; i < buttonList.Count; i++)
                {
                    buttonList[i].SetImage(Main.wireUITexture[0]);

                    if (i != selectedButton)
                    {
                        buttonList[i].SetColor(NoHoverColor);
                        buttonImageList[i].SetColor(NoHoverColor);
                    }
                }

                base.ToggleUI(Interface, State);
            }
        }

        public virtual void ButtonClicked(int index)
        {
            selectedButton = index;

            for (int i = 0; i < buttonList.Count; i++)
            {
                buttonList[i].SetColor(NoHoverColor);
                buttonImageList[i].SetColor(NoHoverColor);
            }

            buttonList[index].SetColor(Color.White);
            buttonImageList[index].SetColor(Color.White);
        }

        public virtual void ButtonHover(int index)
        {
            buttonList[index].SetImage(Main.wireUITexture[1]);

            if (index != selectedButton)
            {
                buttonList[index].SetColor(HoverColor);
                buttonImageList[index].SetColor(HoverColor);
            }
        }

        public virtual void ButtonLeave(int index)
        {
            buttonList[index].SetImage(Main.wireUITexture[0]);

            if (index != selectedButton)
            {
                buttonList[index].SetColor(NoHoverColor);
                buttonImageList[index].SetColor(NoHoverColor);
            }
        }

        public virtual void UpdatePosition()
        {
            circleCenter = Main.MouseScreen - new Vector2(20f, 20f);

            for (int i = 0; i < buttonList.Count; i++)
            {
                double angle = (Math.PI * 2 * i) / ButtonAmount;

                double x = circleCenter.X + (CircleRadius * Math.Cos(angle));
                double y = circleCenter.Y + (CircleRadius * Math.Sin(angle));

                buttonList[i].Left.Set((float)x, 0f);
                buttonList[i].Top.Set((float)y, 0f);
            }

            Recalculate();
        }
    }
}
