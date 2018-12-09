using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace AAMod.NPCs.Bosses.Infinity
{
    public class IZSpawn1 : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Death");
            Main.npcFrameCount[npc.type] = 8;
        }

        public override void SetDefaults()
        {
            npc.lifeMax = 1;
            npc.dontTakeDamage = true;
            npc.width = 1;
            npc.height = 1;
            npc.npcSlots = 100;
            npc.dontCountMe = true;
            npc.noTileCollide = true;
            npc.boss = false;
            npc.noGravity = true;
            npc.behindTiles = true;
            npc.aiStyle = 0;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/IZ");
            npc.scale *= 2;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
        }

        private bool Spawn1 = true;
        private bool Spawn2 = false;
        private bool Spawn3 = false;
        private bool Spawn4 = false;
        private bool Spawn5 = false;
        private bool Spawn6 = false;
        private int Frame2;
        private int Frame3;
        private int Frame4;
        private int Frame5;
        private int Frame6;
        private int Frame2Counter;
        private int Frame3Counter;
        private int Frame4Counter;
        private int Frame5Counter;
        private int Frame6Counter;
        private int HoldTimer = 5;
        private int HoldTimer2 = 5;
        private int HoldTimer3 = 5;
        private int HoldTimer4 = 5;
        private int HoldTimer5 = 5;
        private int HoldTimer6 = 5;

        public override void AI()
        {
            Player player = Main.player[npc.target]; // makes it so you can reference the player the npc is targetting
            if (Spawn1)
            {
                npc.frameCounter++;
                if (npc.frameCounter >= 10)
                {
                    npc.frameCounter = 0;
                    npc.frame.Y += 210;
                    if (npc.frame.Y >= 1470)
                    {
                        HoldTimer--;
                        npc.frame.Y = 1470;
                        if (HoldTimer == 0) 
                        {
                            Spawn1 = false;
                            Spawn2 = true;
                        }
                    }
                }
            }
            if (Spawn2)
            {
                Frame2Counter++;
                if (Frame2Counter > 10)
                {
                    Frame2++;
                    Frame2Counter = 0;
                }
                if (Frame2 >= 5)
                {
                    Frame2 = 4;
                    HoldTimer2--;
                    if (HoldTimer2 == 0)
                    {
                        Spawn2 = false;
                        Spawn3 = true;
                    }
                }
            }
            if (Spawn3)
            {
                Frame3Counter++;
                if (Frame3Counter > 10)
                {
                    Frame3++;
                    Frame3Counter = 0;
                }
                if (Frame3 >= 5)
                {
                    Frame3 = 4;
                    HoldTimer3--;
                    if (HoldTimer3 == 0)
                    {
                        Spawn3 = false;
                        Spawn4 = true;
                    }
                }
            }
            if (Spawn4)
            {
                Frame4Counter++;
                if (Frame4Counter > 10)
                {
                    Frame4++;
                    Frame4Counter = 0;
                }
                if (Frame4 >= 5)
                {
                    Frame4 = 4;
                    HoldTimer4--;
                    if (HoldTimer4 == 0)
                    {
                        Spawn4 = false;
                        Spawn5 = true;
                    }
                }
            }
            if (Spawn5)
            {
                Frame5Counter++;
                if (Frame5Counter > 10)
                {
                    Frame5++;
                    Frame5Counter = 0;
                }
                if (Frame5 >= 4)
                {
                    Frame5 = 3;
                    HoldTimer5--;
                    if (HoldTimer5 == 0)
                    {
                        Spawn5 = false;
                        Spawn6 = true;
                    }
                }
            }
            if (Spawn6)
            {
                Frame6Counter++;
                if (Frame2Counter > 10)
                {
                    Frame6++;
                    Frame6Counter = 0;
                }
                if (Frame6 >= 7)
                {
                    Frame6 = 6;
                    HoldTimer6--;
                    if (HoldTimer6 == 0)
                    {
                        npc.life = 0;
                    }
                }
            }
        }

        public override void NPCLoot()
        {
            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/IZRoar"), (int)npc.Center.X, (int)npc.Center.Y);
            NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("Infinity"));
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D SFrame1 = Main.npcTexture[npc.type];
            Texture2D SFrame2 = mod.GetTexture("NPCs/Bosses/Infinity/IZSpawn2");
            Texture2D SFrame3 = mod.GetTexture("NPCs/Bosses/Infinity/IZSpawn3");
            Texture2D SFrame4 = mod.GetTexture("NPCs/Bosses/Infinity/IZSpawn4");
            Texture2D SFrame5 = mod.GetTexture("NPCs/Bosses/Infinity/IZSpawn5");
            Texture2D SFrame6 = mod.GetTexture("NPCs/Bosses/Infinity/IZSpawn6");
            Texture2D SFrame7 = mod.GetTexture("NPCs/Bosses/Infinity/IZSpawn6");
            var effects = npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            if (Spawn1) // i think this is important for it to not do its usual walking cycle while its also doing those attacks
            {
                spriteBatch.Draw(SFrame1, npc.Center - Main.screenPosition, npc.frame, drawColor, npc.rotation, npc.frame.Size() / 2, npc.scale, npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            }
            if (Spawn2)
            {
                Vector2 drawCenter = new Vector2(npc.Center.X, npc.Center.Y);
                int num214 = SFrame2.Height / 5; // 3 is the number of frames in the sprite sheet
                int y6 = num214 * Frame2;
                Main.spriteBatch.Draw(SFrame2, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, SFrame2.Width, num214)), drawColor, npc.rotation, new Vector2((float)SFrame2.Width / 2f, (float)num214 / 2f), npc.scale, npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            }
            if (Spawn3)
            {
                Vector2 drawCenter = new Vector2(npc.Center.X, npc.Center.Y);
                int num214 = SFrame3.Height / 5; // 3 is the number of frames in the sprite sheet
                int y6 = num214 * Frame3;
                Main.spriteBatch.Draw(SFrame3, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, SFrame3.Width, num214)), drawColor, npc.rotation, new Vector2((float)SFrame3.Width / 2f, (float)num214 / 2f), npc.scale, npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            }
            if (Spawn4)
            {
                Vector2 drawCenter = new Vector2(npc.Center.X, npc.Center.Y);
                int num214 = SFrame4.Height / 5; // 3 is the number of frames in the sprite sheet
                int y6 = num214 * Frame4;
                Main.spriteBatch.Draw(SFrame4, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, SFrame4.Width, num214)), drawColor, npc.rotation, new Vector2((float)SFrame4.Width / 2f, (float)num214 / 2f), npc.scale, npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            }
            if (Spawn5)
            {
                Vector2 drawCenter = new Vector2(npc.Center.X, npc.Center.Y);
                int num214 = SFrame5.Height / 4; // 3 is the number of frames in the sprite sheet
                int y6 = num214 * Frame5;
                Main.spriteBatch.Draw(SFrame5, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, SFrame5.Width, num214)), drawColor, npc.rotation, new Vector2((float)SFrame5.Width / 2f, (float)num214 / 2f), npc.scale, npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            }
            if (Spawn6)
            {
                Vector2 drawCenter = new Vector2(npc.Center.X, npc.Center.Y);
                int num214 = SFrame6.Height / 7; // 3 is the number of frames in the sprite sheet
                int y6 = num214 * Frame6;
                Main.spriteBatch.Draw(SFrame6, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, SFrame6.Width, num214)), drawColor, npc.rotation, new Vector2((float)SFrame6.Width / 2f, (float)num214 / 2f), npc.scale, npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            }
            return false;
        }

    }
}
