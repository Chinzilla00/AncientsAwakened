using System;
using System.Collections.Generic;
using AAMod.Items.Tools;
using AAMod.UI.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameInput;
using Terraria.UI;

namespace AAMod.UI
{
    internal sealed class TerratoolUI : ToggableUI
    {
        private static Vector2 circleCenter = Main.MouseScreen - new Vector2(20f, 20f);
        private static int[] selectedButtons = { -1, -1 };
        private static readonly int buttonAmount = 3;
        private static readonly float buttonPadding = 5f;
        private static readonly float circleRadius = 40f + buttonPadding;

        private List<UIColorImageButton> buttonList;
        private List<UIColorImage> buttonImageList;
        private Rectangle buttonFrontFrame;
        private Texture2D buttonImages = AAMod.instance.GetTexture("UI/Tools/ToolUI");
        private readonly Texture2D buttonHoverImage = AAMod.instance.GetTexture("UI/Tools/ToolButton");
        private readonly Texture2D buttonNoHoverImage = AAMod.instance.GetTexture("UI/Tools/ToolButtonOff");
        private Color hoverColor = new Color(150, 150, 150);
        private Color noHoverColor = new Color(80, 80, 80);

        public override void OnInitialize()
        {
            buttonList = new List<UIColorImageButton>();
            buttonImageList = new List<UIColorImage>();
            buttonFrontFrame = buttonImages.Frame(1, buttonAmount);

            for (int i = 0; i < buttonAmount; i++)
            {
                buttonList.Add(new UIColorImageButton(buttonNoHoverImage, noHoverColor));
                buttonImageList.Add(new UIColorImage(buttonImages, noHoverColor, buttonFrontFrame));

                double angle = (Math.PI * 2 * i) / buttonAmount;
                double x = circleCenter.X + (circleRadius * Math.Cos(angle));
                double y = circleCenter.Y + (circleRadius * Math.Sin(angle));

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

            if (Main.LocalPlayer.HeldItem.type != AAMod.instance.ItemType<ChaosTerratool>() || Main.LocalPlayer.dead || Main.LocalPlayer.ghost)
            {
                for (int i = 0; i < buttonList.Count; i++)
                {
                    buttonList[i].SetImage(buttonNoHoverImage);

                    if (Array.IndexOf(selectedButtons, i) < 0)
                    {
                        buttonList[i].SetColor(noHoverColor);
                        buttonImageList[i].SetColor(noHoverColor);
                    }
                }

                base.ToggleUI(AAMod.instance.TerratoolInterface, AAMod.instance.TerratoolState);
            }
        }

        public static int Pick = 0;
        public static int Hammer = 0;
        public static int Axe = 0;

        private void ButtonClicked(int index)
        {
            selectedButtons.SetValue(selectedButtons[0], selectedButtons[1]);
            selectedButtons.SetValue(index, selectedButtons[0]);

            switch (selectedButtons[0])
            {
                case 0:
                    Pick = 215;
                    Hammer = selectedButtons[1] == 1 ? 120 : 0;
                    Axe = selectedButtons[1] == 2 ? 50 : 0;
                    break;

                case 1:
                    Hammer = 120;
                    Pick = selectedButtons[1] == 0 ? 215 : 0;
                    Axe = selectedButtons[1] == 2 ? 50 : 0;
                    break;

                case 2:
                    Axe = 50;
                    Pick = selectedButtons[1] == 0 ? 215 : 0;
                    Hammer = selectedButtons[1] == 1 ? 120 : 0;
            }

            for (int i = 0; i < buttonList.Count; i++)
            {
                buttonList[i].SetColor(noHoverColor);
                buttonImageList[i].SetColor(noHoverColor);
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

        private void ButtonHover(int index)
        {
            buttonList[index].SetImage(buttonHoverImage);

            if (Array.IndexOf(selectedButtons, index) < 0)
            {
                buttonList[index].SetColor(hoverColor);
                buttonImageList[index].SetColor(hoverColor);
            }
        }

        private void ButtonLeave(int index)
        {
            buttonList[index].SetImage(buttonNoHoverImage);

            if (Array.IndexOf(selectedButtons, index) < 0)
            {
                buttonList[index].SetColor(noHoverColor);
                buttonImageList[index].SetColor(noHoverColor);
            }
        }

        private void UpdatePosition()
        {
            circleCenter = Main.MouseScreen - new Vector2(20f, 20f);

            for (int i = 0; i < buttonList.Count; i++)
            {
                double angle = (Math.PI * 2 * i) / buttonAmount;

                double x = circleCenter.X + (circleRadius * Math.Cos(angle));
                double y = circleCenter.Y + (circleRadius * Math.Sin(angle));

                buttonList[i].Left.Set((float)x, 0f);
                buttonList[i].Top.Set((float)y, 0f);
            }

            Recalculate();
        }
    }

    internal sealed class TerratoolCUI : ToggableUI
    {
        private static Vector2 circleCenter = Main.MouseScreen - new Vector2(20f, 20f);
        private static int[] selectedButtons = { -1, -1 };
        private static readonly int buttonAmount = 3;
        private static readonly float buttonPadding = 5f;
        private static readonly float circleRadius = 40f + buttonPadding;

        private List<UIColorImageButton> buttonList;
        private List<UIColorImage> buttonImageList;
        private Rectangle buttonFrontFrame;
        private Texture2D buttonImages = AAMod.instance.GetTexture("UI/Tools/ToolUIC");
        private readonly Texture2D buttonHoverImage = AAMod.instance.GetTexture("UI/Tools/ToolButtonC");
        private readonly Texture2D buttonNoHoverImage = AAMod.instance.GetTexture("UI/Tools/ToolButtonCOff");
        private Color hoverColor = new Color(150, 150, 150);
        private Color noHoverColor = new Color(80, 80, 80);

        public override void OnInitialize()
        {
            buttonList = new List<UIColorImageButton>();
            buttonImageList = new List<UIColorImage>();
            buttonFrontFrame = buttonImages.Frame(1, buttonAmount);

            for (int i = 0; i < buttonAmount; i++)
            {
                buttonList.Add(new UIColorImageButton(buttonNoHoverImage, noHoverColor));
                buttonImageList.Add(new UIColorImage(buttonImages, noHoverColor, buttonFrontFrame));

                double angle = (Math.PI * 2 * i) / buttonAmount;
                double x = circleCenter.X + (circleRadius * Math.Cos(angle));
                double y = circleCenter.Y + (circleRadius * Math.Sin(angle));

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

            if (Main.LocalPlayer.HeldItem.type != AAMod.instance.ItemType<ChaosTerratool>() || Main.LocalPlayer.dead || Main.LocalPlayer.ghost)
            {
                for (int i = 0; i < buttonList.Count; i++)
                {
                    buttonList[i].SetImage(buttonNoHoverImage);

                    if (Array.IndexOf(selectedButtons, i) < 0)
                    {
                        buttonList[i].SetColor(noHoverColor);
                        buttonImageList[i].SetColor(noHoverColor);
                    }
                }

                base.ToggleUI(AAMod.instance.TerratoolInterface, AAMod.instance.TerratoolState);
            }
        }

        public static int Pick = 0;
        public static int Hammer = 0;
        public static int Axe = 0;

        private void ButtonClicked(int index)
        {
            selectedButtons.SetValue(selectedButtons[0], selectedButtons[1]);
            selectedButtons.SetValue(index, selectedButtons[0]);

            switch (selectedButtons[0])
            {
                case 0:
                    Pick = 215;
                    Hammer = selectedButtons[1] == 1 ? 120 : 0;
                    Axe = selectedButtons[1] == 2 ? 50 : 0;
                    break;

                case 1:
                    Hammer = 120;
                    Pick = selectedButtons[1] == 0 ? 215 : 0;
                    Axe = selectedButtons[1] == 2 ? 50 : 0;
                    break;

                case 2:
                    Axe = 50;
                    Pick = selectedButtons[1] == 0 ? 215 : 0;
                    Hammer = selectedButtons[1] == 1 ? 120 : 0;
                    break;
            }

            for (int i = 0; i < buttonList.Count; i++)
            {
                buttonList[i].SetColor(noHoverColor);
                buttonImageList[i].SetColor(noHoverColor);
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

        private void ButtonHover(int index)
        {
            buttonList[index].SetImage(buttonHoverImage);

            if (Array.IndexOf(selectedButtons, index) < 0)
            {
                buttonList[index].SetColor(hoverColor);
                buttonImageList[index].SetColor(hoverColor);
            }
        }

        private void ButtonLeave(int index)
        {
            buttonList[index].SetImage(buttonNoHoverImage);

            if (Array.IndexOf(selectedButtons, index) < 0)
            {
                buttonList[index].SetColor(noHoverColor);
                buttonImageList[index].SetColor(noHoverColor);
            }
        }

        private void UpdatePosition()
        {
            circleCenter = Main.MouseScreen - new Vector2(20f, 20f);

            for (int i = 0; i < buttonList.Count; i++)
            {
                double angle = (Math.PI * 2 * i) / buttonAmount;

                double x = circleCenter.X + (circleRadius * Math.Cos(angle));
                double y = circleCenter.Y + (circleRadius * Math.Sin(angle));

                buttonList[i].Left.Set((float)x, 0f);
                buttonList[i].Top.Set((float)y, 0f);
            }

            Recalculate();
        }
    }

    internal sealed class TerratoolAUI : ToggableUI
    {
        private static Vector2 circleCenter = Main.MouseScreen - new Vector2(20f, 20f);
        private static int[] selectedButtons = { -1, -1 };
        private static readonly int buttonAmount = 3;
        private static readonly float buttonPadding = 5f;
        private static readonly float circleRadius = 40f + buttonPadding;

        private List<UIColorImageButton> buttonList;
        private List<UIColorImage> buttonImageList;
        private Rectangle buttonFrontFrame;
        private Texture2D buttonImages = AAMod.instance.GetTexture("UI/Tools/ToolUIA");
        private readonly Texture2D buttonHoverImage = AAMod.instance.GetTexture("UI/Tools/ToolButtonA");
        private readonly Texture2D buttonNoHoverImage = AAMod.instance.GetTexture("UI/Tools/ToolButtonAOff");
        private Color hoverColor = new Color(150, 150, 150);
        private Color noHoverColor = new Color(80, 80, 80);

        public override void OnInitialize()
        {
            buttonList = new List<UIColorImageButton>();
            buttonImageList = new List<UIColorImage>();
            buttonFrontFrame = buttonImages.Frame(1, buttonAmount);

            for (int i = 0; i < buttonAmount; i++)
            {
                buttonList.Add(new UIColorImageButton(buttonNoHoverImage, noHoverColor));
                buttonImageList.Add(new UIColorImage(buttonImages, noHoverColor, buttonFrontFrame));

                double angle = (Math.PI * 2 * i) / buttonAmount;
                double x = circleCenter.X + (circleRadius * Math.Cos(angle));
                double y = circleCenter.Y + (circleRadius * Math.Sin(angle));

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

            if (Main.LocalPlayer.HeldItem.type != AAMod.instance.ItemType<ChaosTerratool>() || Main.LocalPlayer.dead || Main.LocalPlayer.ghost)
            {
                for (int i = 0; i < buttonList.Count; i++)
                {
                    buttonList[i].SetImage(buttonNoHoverImage);

                    if (Array.IndexOf(selectedButtons, i) < 0)
                    {
                        buttonList[i].SetColor(noHoverColor);
                        buttonImageList[i].SetColor(noHoverColor);
                    }
                }

                base.ToggleUI(AAMod.instance.TerratoolInterface, AAMod.instance.TerratoolState);
            }
        }

        public static int Pick = 0;
        public static int Hammer = 0;
        public static int Axe = 0;

        private void ButtonClicked(int index)
        {
            selectedButtons.SetValue(selectedButtons[0], selectedButtons[1]);
            selectedButtons.SetValue(index, selectedButtons[0]);

            switch (selectedButtons[0])
            {
                case 0:
                    Pick = 300;
                    Hammer = selectedButtons[1] == 1 ? 260 : 0;
                    Axe = selectedButtons[1] == 2 ? 50 : 0;
                    break;

                case 1:
                    Hammer = 260;
                    Pick = selectedButtons[1] == 0 ? 300 : 0;
                    Axe = selectedButtons[1] == 2 ? 50 : 0;
                    break;

                case 2:
                    Axe = 50;
                    Pick = selectedButtons[1] == 0 ? 300 : 0;
                    Hammer = selectedButtons[1] == 1 ? 260 : 0;
                    break;
            }

            for (int i = 0; i < buttonList.Count; i++)
            {
                buttonList[i].SetColor(noHoverColor);
                buttonImageList[i].SetColor(noHoverColor);
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

        private void ButtonHover(int index)
        {
            buttonList[index].SetImage(buttonHoverImage);

            if (Array.IndexOf(selectedButtons, index) < 0)
            {
                buttonList[index].SetColor(hoverColor);
                buttonImageList[index].SetColor(hoverColor);
            }
        }

        private void ButtonLeave(int index)
        {
            buttonList[index].SetImage(buttonNoHoverImage);

            if (Array.IndexOf(selectedButtons, index) < 0)
            {
                buttonList[index].SetColor(noHoverColor);
                buttonImageList[index].SetColor(noHoverColor);
            }
        }

        private void UpdatePosition()
        {
            circleCenter = Main.MouseScreen - new Vector2(20f, 20f);

            for (int i = 0; i < buttonList.Count; i++)
            {
                double angle = (Math.PI * 2 * i) / buttonAmount;

                double x = circleCenter.X + (circleRadius * Math.Cos(angle));
                double y = circleCenter.Y + (circleRadius * Math.Sin(angle));

                buttonList[i].Left.Set((float)x, 0f);
                buttonList[i].Top.Set((float)y, 0f);
            }

            Recalculate();
        }
    }

    internal sealed class TerratoolYUI : ToggableUI
    {
        private static Vector2 circleCenter = Main.MouseScreen - new Vector2(20f, 20f);
        private static int[] selectedButtons = { -1, -1 };
        private static readonly int buttonAmount = 3;
        private static readonly float buttonPadding = 5f;
        private static readonly float circleRadius = 40f + buttonPadding;

        private List<UIColorImageButton> buttonList;
        private List<UIColorImage> buttonImageList;
        private Rectangle buttonFrontFrame;
        private Texture2D buttonImages = AAMod.instance.GetTexture("UI/Tools/ToolUIY");
        private readonly Texture2D buttonHoverImage = AAMod.instance.GetTexture("UI/Tools/ToolButtonY");
        private readonly Texture2D buttonNoHoverImage = AAMod.instance.GetTexture("UI/Tools/ToolButtonYOff");
        private Color hoverColor = new Color(150, 150, 150);
        private Color noHoverColor = new Color(80, 80, 80);

        public override void OnInitialize()
        {
            buttonList = new List<UIColorImageButton>();
            buttonImageList = new List<UIColorImage>();
            buttonFrontFrame = buttonImages.Frame(1, buttonAmount);

            for (int i = 0; i < buttonAmount; i++)
            {
                buttonList.Add(new UIColorImageButton(buttonNoHoverImage, noHoverColor));
                buttonImageList.Add(new UIColorImage(buttonImages, noHoverColor, buttonFrontFrame));

                double angle = (Math.PI * 2 * i) / buttonAmount;
                double x = circleCenter.X + (circleRadius * Math.Cos(angle));
                double y = circleCenter.Y + (circleRadius * Math.Sin(angle));

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

            if (Main.LocalPlayer.HeldItem.type != AAMod.instance.ItemType<ChaosTerratool>() || Main.LocalPlayer.dead || Main.LocalPlayer.ghost)
            {
                for (int i = 0; i < buttonList.Count; i++)
                {
                    buttonList[i].SetImage(buttonNoHoverImage);

                    if (Array.IndexOf(selectedButtons, i) < 0)
                    {
                        buttonList[i].SetColor(noHoverColor);
                        buttonImageList[i].SetColor(noHoverColor);
                    }
                }

                base.ToggleUI(AAMod.instance.TerratoolInterface, AAMod.instance.TerratoolState);
            }
        }

        public static int Pick = 0;
        public static int Hammer = 0;
        public static int Axe = 0;

        private void ButtonClicked(int index)
        {
            selectedButtons.SetValue(selectedButtons[0], selectedButtons[1]);
            selectedButtons.SetValue(index, selectedButtons[0]);

            switch (selectedButtons[0])
            {
                case 0:
                    Pick = 300;
                    Hammer = selectedButtons[1] == 1 ? 260 : 0;
                    Axe = selectedButtons[1] == 2 ? 50 : 0;
                    break;

                case 1:
                    Hammer = 260;
                    Pick = selectedButtons[1] == 0 ? 300 : 0;
                    Axe = selectedButtons[1] == 2 ? 50 : 0;
                    break;

                case 2:
                    Axe = 50;
                    Pick = selectedButtons[1] == 0 ? 300 : 0;
                    Hammer = selectedButtons[1] == 1 ? 260 : 0;
                    break;
            }

            for (int i = 0; i < buttonList.Count; i++)
            {
                buttonList[i].SetColor(noHoverColor);
                buttonImageList[i].SetColor(noHoverColor);
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

        private void ButtonHover(int index)
        {
            buttonList[index].SetImage(buttonHoverImage);

            if (Array.IndexOf(selectedButtons, index) < 0)
            {
                buttonList[index].SetColor(hoverColor);
                buttonImageList[index].SetColor(hoverColor);
            }
        }

        private void ButtonLeave(int index)
        {
            buttonList[index].SetImage(buttonNoHoverImage);

            if (Array.IndexOf(selectedButtons, index) < 0)
            {
                buttonList[index].SetColor(noHoverColor);
                buttonImageList[index].SetColor(noHoverColor);
            }
        }

        private void UpdatePosition()
        {
            circleCenter = Main.MouseScreen - new Vector2(20f, 20f);

            for (int i = 0; i < buttonList.Count; i++)
            {
                double angle = (Math.PI * 2 * i) / buttonAmount;

                double x = circleCenter.X + (circleRadius * Math.Cos(angle));
                double y = circleCenter.Y + (circleRadius * Math.Sin(angle));

                buttonList[i].Left.Set((float)x, 0f);
                buttonList[i].Top.Set((float)y, 0f);
            }

            Recalculate();
        }
    }

    internal sealed class TerratoolZUI : ToggableUI
    {
        private static Vector2 circleCenter = Main.MouseScreen - new Vector2(20f, 20f);
        private static int[] selectedButtons = { -1, -1 };
        private static readonly int buttonAmount = 3;
        private static readonly float buttonPadding = 5f;
        private static readonly float circleRadius = 40f + buttonPadding;

        private List<UIColorImageButton> buttonList;
        private List<UIColorImage> buttonImageList;
        private Rectangle buttonFrontFrame;
        private Texture2D buttonImages = AAMod.instance.GetTexture("UI/Tools/ToolUIZ");
        private readonly Texture2D buttonHoverImage = AAMod.instance.GetTexture("UI/Tools/ToolButtonZ");
        private readonly Texture2D buttonNoHoverImage = AAMod.instance.GetTexture("UI/Tools/ToolButtonZOff");
        private Color hoverColor = new Color(150, 150, 150);
        private Color noHoverColor = new Color(80, 80, 80);

        public override void OnInitialize()
        {
            buttonList = new List<UIColorImageButton>();
            buttonImageList = new List<UIColorImage>();
            buttonFrontFrame = buttonImages.Frame(1, buttonAmount);

            for (int i = 0; i < buttonAmount; i++)
            {
                buttonList.Add(new UIColorImageButton(buttonNoHoverImage, noHoverColor));
                buttonImageList.Add(new UIColorImage(buttonImages, noHoverColor, buttonFrontFrame));

                double angle = (Math.PI * 2 * i) / buttonAmount;
                double x = circleCenter.X + (circleRadius * Math.Cos(angle));
                double y = circleCenter.Y + (circleRadius * Math.Sin(angle));

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

            if (Main.LocalPlayer.HeldItem.type != AAMod.instance.ItemType<ChaosTerratool>() || Main.LocalPlayer.dead || Main.LocalPlayer.ghost)
            {
                for (int i = 0; i < buttonList.Count; i++)
                {
                    buttonList[i].SetImage(buttonNoHoverImage);

                    if (Array.IndexOf(selectedButtons, i) < 0)
                    {
                        buttonList[i].SetColor(noHoverColor);
                        buttonImageList[i].SetColor(noHoverColor);
                    }
                }

                base.ToggleUI(AAMod.instance.TerratoolInterface, AAMod.instance.TerratoolState);
            }
        }

        public static int Pick = 0;
        public static int Hammer = 0;
        public static int Axe = 0;

        private void ButtonClicked(int index)
        {
            selectedButtons.SetValue(selectedButtons[0], selectedButtons[1]);
            selectedButtons.SetValue(index, selectedButtons[0]);

            switch (selectedButtons[0])
            {
                case 0:
                    Pick = 300;
                    Hammer = selectedButtons[1] == 1 ? 260 : 0;
                    Axe = selectedButtons[1] == 2 ? 50 : 0;
                    break;

                case 1:
                    Hammer = 260;
                    Pick = selectedButtons[1] == 0 ? 300 : 0;
                    Axe = selectedButtons[1] == 2 ? 50 : 0;
                    break;

                case 2:
                    Axe = 50;
                    Pick = selectedButtons[1] == 0 ? 300 : 0;
                    Hammer = selectedButtons[1] == 1 ? 260 : 0;
                    break;
            }

            for (int i = 0; i < buttonList.Count; i++)
            {
                buttonList[i].SetColor(noHoverColor);
                buttonImageList[i].SetColor(noHoverColor);
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

        private void ButtonHover(int index)
        {
            buttonList[index].SetImage(buttonHoverImage);

            if (Array.IndexOf(selectedButtons, index) < 0)
            {
                buttonList[index].SetColor(hoverColor);
                buttonImageList[index].SetColor(hoverColor);
            }
        }

        private void ButtonLeave(int index)
        {
            buttonList[index].SetImage(buttonNoHoverImage);

            if (Array.IndexOf(selectedButtons, index) < 0)
            {
                buttonList[index].SetColor(noHoverColor);
                buttonImageList[index].SetColor(noHoverColor);
            }
        }

        private void UpdatePosition()
        {
            circleCenter = Main.MouseScreen - new Vector2(20f, 20f);

            for (int i = 0; i < buttonList.Count; i++)
            {
                double angle = (Math.PI * 2 * i) / buttonAmount;

                double x = circleCenter.X + (circleRadius * Math.Cos(angle));
                double y = circleCenter.Y + (circleRadius * Math.Sin(angle));

                buttonList[i].Left.Set((float)x, 0f);
                buttonList[i].Top.Set((float)y, 0f);
            }

            Recalculate();
        }
    }

    internal sealed class TerratoolSUI : ToggableUI
    {
        private static Vector2 circleCenter = Main.MouseScreen - new Vector2(20f, 20f);
        private static int[] selectedButtons = { -1, -1 };
        private static readonly int buttonAmount = 3;
        private static readonly float buttonPadding = 5f;
        private static readonly float circleRadius = 40f + buttonPadding;

        private List<UIColorImageButton> buttonList;
        private List<UIColorImage> buttonImageList;
        private Rectangle buttonFrontFrame;
        private Texture2D buttonImages = AAMod.instance.GetTexture("UI/Tools/ToolUIS");
        private readonly Texture2D buttonHoverImage = AAMod.instance.GetTexture("UI/Tools/ToolButtonS");
        private readonly Texture2D buttonNoHoverImage = AAMod.instance.GetTexture("UI/Tools/ToolButtonSOff");
        private Color hoverColor = new Color(150, 150, 150);
        private Color noHoverColor = new Color(80, 80, 80);

        public override void OnInitialize()
        {
            buttonList = new List<UIColorImageButton>();
            buttonImageList = new List<UIColorImage>();
            buttonFrontFrame = buttonImages.Frame(1, buttonAmount);

            for (int i = 0; i < buttonAmount; i++)
            {
                buttonList.Add(new UIColorImageButton(buttonNoHoverImage, noHoverColor));
                buttonImageList.Add(new UIColorImage(buttonImages, noHoverColor, buttonFrontFrame));

                double angle = (Math.PI * 2 * i) / buttonAmount;
                double x = circleCenter.X + (circleRadius * Math.Cos(angle));
                double y = circleCenter.Y + (circleRadius * Math.Sin(angle));

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

            if (Main.LocalPlayer.HeldItem.type != AAMod.instance.ItemType<ChaosTerratool>() || Main.LocalPlayer.dead || Main.LocalPlayer.ghost)
            {
                for (int i = 0; i < buttonList.Count; i++)
                {
                    buttonList[i].SetImage(buttonNoHoverImage);

                    if (Array.IndexOf(selectedButtons, i) < 0)
                    {
                        buttonList[i].SetColor(noHoverColor);
                        buttonImageList[i].SetColor(noHoverColor);
                    }
                }

                base.ToggleUI(AAMod.instance.TerratoolInterface, AAMod.instance.TerratoolState);
            }
        }

        public static int Pick = 0;
        public static int Hammer = 0;
        public static int Axe = 0;

        private void ButtonClicked(int index)
        {
            selectedButtons.SetValue(selectedButtons[0], selectedButtons[1]);
            selectedButtons.SetValue(index, selectedButtons[0]);

            switch (selectedButtons[0])
            {
                case 0:
                    Pick = 320;
                    Hammer = selectedButtons[1] == 1 ? 270 : 0;
                    Axe = selectedButtons[1] == 2 ? 70 : 0;
                    break;

                case 1:
                    Hammer = 270;
                    Pick = selectedButtons[1] == 0 ? 320 : 0;
                    Axe = selectedButtons[1] == 2 ? 70 : 0;
                    break;

                case 2:
                    Axe = 70;
                    Pick = selectedButtons[1] == 0 ? 320 : 0;
                    Hammer = selectedButtons[1] == 1 ? 270 : 0;
                    break;
            }

            for (int i = 0; i < buttonList.Count; i++)
            {
                buttonList[i].SetColor(noHoverColor);
                buttonImageList[i].SetColor(noHoverColor);
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

        private void ButtonHover(int index)
        {
            buttonList[index].SetImage(buttonHoverImage);

            if (Array.IndexOf(selectedButtons, index) < 0)
            {
                buttonList[index].SetColor(hoverColor);
                buttonImageList[index].SetColor(hoverColor);
            }
        }

        private void ButtonLeave(int index)
        {
            buttonList[index].SetImage(buttonNoHoverImage);

            if (Array.IndexOf(selectedButtons, index) < 0)
            {
                buttonList[index].SetColor(noHoverColor);
                buttonImageList[index].SetColor(noHoverColor);
            }
        }

        private void UpdatePosition()
        {
            circleCenter = Main.MouseScreen - new Vector2(20f, 20f);

            for (int i = 0; i < buttonList.Count; i++)
            {
                double angle = (Math.PI * 2 * i) / buttonAmount;

                double x = circleCenter.X + (circleRadius * Math.Cos(angle));
                double y = circleCenter.Y + (circleRadius * Math.Sin(angle));

                buttonList[i].Left.Set((float)x, 0f);
                buttonList[i].Top.Set((float)y, 0f);
            }

            Recalculate();
        }
    }
}
