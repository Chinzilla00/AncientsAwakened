using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using BaseMod;

namespace AAMod.NPCs.Bosses.Infinity
{
    public class IZSpawn1 : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Death");
            Main.npcFrameCount[npc.type] = 2;
        }

        public override void SetDefaults()
        {
            npc.lifeMax = 1;
            npc.dontTakeDamage = true;
            npc.width = 342;
            npc.height = 420; 
            npc.npcSlots = 100;
            npc.dontCountMe = true;
            npc.noTileCollide = true;
            npc.boss = false;
            npc.noGravity = true;
            //npc.behindTiles = true;
            npc.aiStyle = -1;
            npc.scale *= 1.4f;
            npc.behindTiles = true;
            music = mod.GetSoundSlot(SoundType.Music, "Lulspooky");
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
            npc.alpha = 255;
        }

        private int Frame = 0;
        private int FrameCounter = 0;
        private int HoldTimer = 90;
		public int spawnState = 0;
        public int StartTimer = 200;

        public override void AI()
        {
            StartTimer--;
            if (StartTimer <= 0)
            {
                music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/IZ");
                npc.alpha = 0;
                int endFrame = (spawnState == 0 ? 7 : spawnState == 1 ? 4 : spawnState == 2 ? 4 : spawnState == 3 ? 4 : spawnState == 4 ? 3 : 6);
                if (Frame >= endFrame)
                {
                    Frame = endFrame;
                    HoldTimer--;
                    if (HoldTimer == 0)
                    {
                        Frame = 0;
                        FrameCounter = 0;
                        HoldTimer = (spawnState >= 3 ? 50 : 60);
                        spawnState++;
                        if (spawnState >= 5 && Main.netMode != 1)
                        {
                            SummonInfinity();

                            npc.life = 0;
                            npc.checkDead();
                            npc.netUpdate = true;
                        }
                    }
                }
                else
                {
                    FrameCounter++;
                    if (FrameCounter > 10)
                    {
                        Frame++;
                        FrameCounter = 0;
                    }
                }
            }
			
        }

		public void SummonInfinity()
		{
			//roar is now handled when infinity spawns so his mouth opens
            if(Main.netMode != 1)
			{
				int npcID = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("Infinity"));
				Main.npc[npcID].Center = npc.Center;
				Main.npc[npcID].netUpdate = true;
			}
		}

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D SFrame1 = Main.npcTexture[npc.type];
            Texture2D SFrame2 = mod.GetTexture("NPCs/Bosses/Infinity/IZSpawn2");
            Texture2D SFrame3 = mod.GetTexture("NPCs/Bosses/Infinity/IZSpawn3");
            Texture2D SFrame4 = mod.GetTexture("NPCs/Bosses/Infinity/IZSpawn4");
            Texture2D SFrame5 = mod.GetTexture("NPCs/Bosses/Infinity/IZSpawn5");
            Texture2D SFrame6 = mod.GetTexture("NPCs/Bosses/Infinity/IZSpawn6");
            

            npc.frame = BaseDrawing.GetFrame(Frame, 171, 210, 0, 0);
			Rectangle darkFrame = BaseDrawing.GetFrame(0, 171, 210, 0, 0);
			Texture2D drawTexture = (spawnState == 0 ? SFrame1 : spawnState == 1 ? SFrame2 : spawnState == 2 ? SFrame3 : spawnState == 3 ? SFrame4 : spawnState == 4 ? SFrame5 : SFrame6);
			Texture2D infinityTex = mod.GetTexture("NPCs/Bosses/Infinity/IZShadow");		
			npc.position.Y += 72;
            if (StartTimer <= 0)
            {
                BaseDrawing.DrawTexture(spriteBatch, infinityTex, 0, npc.position + new Vector2(0f, npc.gfxOffY), npc.width, npc.height, 3f, npc.rotation, npc.spriteDirection, 7, darkFrame, Color.Black);
                BaseDrawing.DrawTexture(spriteBatch, drawTexture, 0, npc.position + new Vector2(0f, npc.gfxOffY), npc.width, npc.height, 3f, npc.rotation, npc.spriteDirection, 7, npc.frame, Infinity.GetGlowAlpha(true));
            }
            npc.position.Y -= 72;
			return false;
        }
    }
}
