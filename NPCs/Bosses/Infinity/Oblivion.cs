using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Infinity
{
    public class Oblivion : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Oblivion");
            Main.npcFrameCount[npc.type] = 16;
        }
        public override void SetDefaults()
        {
            npc.width = 1;
            npc.height = 1;
            npc.friendly = false;
        }

        public int OblivionSpeech = 0;


        public override void AI()
        {
            float Eggroll = Math.Abs(Main.GameUpdateCount) / 0.5f;
            float Pie = 1f * (float)Math.Sin(Eggroll);
            Color color1 = Color.Lerp(Color.Red, Color.Black, Pie);


            npc.velocity.X = 0;

            npc.velocity.Y = 0;

            Player player = Main.player[Main.myPlayer];
            OblivionSpeech++;
            if (OblivionSpeech == 120)
            {
                Main.NewText("...impressive.", color1);
            }
            if (OblivionSpeech == 300)
            {
                Main.NewText("Defeating my mechanical body...", color1);
            }
            if (OblivionSpeech == 360)
            {
                Main.NewText("...Is not a small feat...", color1);
            }
            if (OblivionSpeech == 540)
            {
                Main.NewText("I applaud you, terrarian.", color1);
            }
            if (OblivionSpeech == 660)
            {
                Main.NewText("Although...next time we meet...when you're stronger...", color1);
            }
            if (OblivionSpeech == 720)
            {
                Main.NewText("..." + player.name + "...", color1);
            }
            if (OblivionSpeech == 820)
            {
                Main.NewText("I won't be so forgiving.", color1);
            }
            if (OblivionSpeech >= 820)
            {
                npc.alpha += 5;
            }
            if (npc.alpha >= 255)
            {
                npc.life = 0;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (npc.frameCounter < 5)
            {
                if (Main.rand.Next(4) == 0)
                {
                    npc.frame.Y = 12 * frameHeight;
                }
                else
                {
                    npc.frame.Y = 0 * frameHeight;
                }
            }
            else if (npc.frameCounter < 10)
            {
                if (Main.rand.Next(4) == 0)
                {
                    npc.frame.Y = 12 * frameHeight;
                }
                else
                {
                    npc.frame.Y = 1 * frameHeight;
                }
            }
            else if (npc.frameCounter < 15)
            {
                if (Main.rand.Next(4) == 0)
                {
                    npc.frame.Y = 13 * frameHeight;
                }
                else
                {
                    npc.frame.Y = 2 * frameHeight;
                }
            }
            else if (npc.frameCounter < 20)
            {
                if (Main.rand.Next(4) == 0)
                {
                    npc.frame.Y = 13 * frameHeight;
                }
                else
                {
                    npc.frame.Y = 3 * frameHeight;
                }
            }
            else if (npc.frameCounter < 25)
            {
                if (Main.rand.Next(4) == 0)
                {
                    npc.frame.Y = 14 * frameHeight;
                }
                else
                {
                    npc.frame.Y = 4 * frameHeight;
                }
            }
            else if (npc.frameCounter < 30)
            {
                if (Main.rand.Next(4) == 0)
                {
                    npc.frame.Y = 14 * frameHeight;
                }
                else
                {
                    npc.frame.Y = 5 * frameHeight;
                }
            }
            else if (npc.frameCounter < 35)
            {
                if (Main.rand.Next(4) == 0)
                {
                    npc.frame.Y = 15 * frameHeight;
                }
                else
                {
                    npc.frame.Y = 6 * frameHeight;
                }
            }
            else if (npc.frameCounter < 35)
            {
                if (Main.rand.Next(4) == 0)
                {
                    npc.frame.Y = 15 * frameHeight;
                }
                else
                {
                    npc.frame.Y = 7 * frameHeight;
                }
            }
            else if (npc.frameCounter < 35)
            {
                if (Main.rand.Next(4) == 0)
                {
                    npc.frame.Y = 14 * frameHeight;
                }
                else
                {
                    npc.frame.Y = 8 * frameHeight;
                }
            }
            else if (npc.frameCounter < 35)
            {
                if (Main.rand.Next(4) == 0)
                {
                    npc.frame.Y = 14 * frameHeight;
                }
                else
                {
                    npc.frame.Y = 9 * frameHeight;
                }
            }
            else if (npc.frameCounter < 35)
            {
                if (Main.rand.Next(4) == 0)
                {
                    npc.frame.Y = 13 * frameHeight;
                }
                else
                {
                    npc.frame.Y = 10 * frameHeight;
                }
            }
            else if (npc.frameCounter < 35)
            {
                if (Main.rand.Next(4) == 0)
                {
                    npc.frame.Y = 13 * frameHeight;
                }
                else
                {
                    npc.frame.Y = 11 * frameHeight;
                }
            }
            else
            {
                npc.frameCounter = 0;
            }
        }	
    }
}