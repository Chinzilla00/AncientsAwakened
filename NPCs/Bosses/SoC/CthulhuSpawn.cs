using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.SoC
{
    public class CthulhuSpawn : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Quantum Portal");

            Main.npcFrameCount[npc.type] = 9;
        }
        public override void SetDefaults()
        {
            npc.width = 100;
            npc.height = 100;
            npc.alpha = 255;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/SoC");
            npc.lifeMax = 1;
            npc.dontTakeDamage = true;
            npc.noGravity = true;
            npc.aiStyle = -1;
            npc.timeLeft = 10;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
        }

        public int Spawntimer = 0;
        public int Eyetimer = 0;
        public int HoldTimer = 0;
        public int CFrame = 0;

        public override void AI()
        {
            if (npc.timeLeft <= 10)
            {
                npc.timeLeft = 10;
            }
            Spawntimer++;
            if (Spawntimer > 300)
            {
                npc.alpha -= 6;
            }
            if (npc.alpha >= 255)
            {
                npc.alpha = 255;
                npc.frameCounter++;
                Eyetimer++;
            }

            if (Eyetimer >= 80 )
            {
                HoldTimer++;
            }

            
            if (npc.frameCounter < 10)
            {
                npc.frameCounter = 0;
                CFrame += 1;
                npc.frame.Y = 132 * CFrame;
            }
            if (npc.frame.Y >= 132 * 8)
            {
                npc.frame.Y = 132 * 8;
            }

            if (HoldTimer >= 180)
            {
                SummonSoul();
            }
        }

        public void SummonSoul()
        {
            if (Main.netMode != 1)
            {
                Main.NewText("The Soul of Cthulhu shreds through reality into this world", Color.DarkCyan);
                int npcID = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("SoC"));
                Main.npc[npcID].Center = npc.Center;
                Main.npc[npcID].netUpdate = true;
            }

            npc.active = false;
        }

        public Color GetColorAlpha()
        {
            return new Color(16, 16, 36) * (Main.mouseTextColor / 255f);
        }
        
        public float auraPercent = 0f;
        public bool auraDirection = true;
        public float Rotation1;

        public override bool PreDraw(SpriteBatch sb, Color dColor)
        {
            Texture2D Portal1 = mod.GetTexture("NPCs/Bosses/SoC/CthulhuPortal");
            Texture2D EyeTex = mod.GetTexture("NPCs/Bosses/SoC/CthulhuSpawnEyes");
            Vector2 vector38 = npc.position + new Vector2(npc.width, npc.height) / 2f + Vector2.UnitY * npc.gfxOffY - Main.screenPosition;
            int num214 = Main.npcTexture[npc.type].Height / Main.projFrames[npc.type];
            int y6 = 0;
            Color color25 = Lighting.GetColor((int)(npc.position.X + npc.width * 0.5) / 16, (int)((npc.position.Y + npc.height * 0.5) / 16.0));
            Color? alpha4 = GetAlpha(color25);
            Rectangle Frame = BaseDrawing.GetFrame(0, 150, 169, 0, 2);
            Rectangle npcFrame = BaseDrawing.GetFrame(CFrame, 130, 130, 0, 2);
            Vector2 drawCenter = new Vector2(npc.Center.X, npc.Center.Y);

            Rotation1 += .005f;

            if (auraDirection) { auraPercent += 0.1f; auraDirection = auraPercent < 1f; }
            else { auraPercent -= 0.1f; auraDirection = auraPercent <= 0f; }
            Main.spriteBatch.Draw(Portal1, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, Portal1.Width, Portal1.Height)), Color.White, Rotation1, new Vector2(Portal1.Width / 2f, Portal1.Height / 2f), npc.scale, SpriteEffects.None, 0f);
            BaseDrawing.DrawTexture(sb, Main.npcTexture[npc.type], 0, drawCenter - Main.screenPosition, 130, 132, 1, 0, 0, 9, npcFrame, dColor, false);
            BaseDrawing.DrawTexture(sb, EyeTex, 0, drawCenter - Main.screenPosition, 130, 132, 1, 0, 0, 9, npcFrame, Color.White, false);
            return false;
        }
    }
}