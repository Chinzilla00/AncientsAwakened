﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Enemies.Void
{
    public class SagittariusMini : ModNPC
	{
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shadow Scout");
            Main.npcFrameCount[npc.type] = 12;
        }
		
		public override void SetDefaults()
		{
            npc.noGravity = true;
            npc.noTileCollide = true;
			npc.aiStyle = -1;
            npc.width = 24;
            npc.height = 40;
            npc.damage = 20;
            npc.defense = 10;
            npc.lifeMax = 100;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
            npc.alpha = 70;
            npc.value = 7000f;
            npc.knockBackResist = 0.7f;
            npc.noGravity = true;
            banner = npc.type;
			bannerItem = mod.ItemType("ShadowScoutBanner");
        }

		public int frameCount = 0;
		public int frameCounter = 0;
        public int IdleTimer = 0;

		public override void PostAI()
		{
			npc.spriteDirection = npc.velocity.X > 0 ? -1 : 1;
		}

        public override void AI()
        {
            BaseAI.AIElemental(npc, ref npc.ai, ref IdleTimer, null, 1, false, true, 800f, 600f, 180, 2f);
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (npc.frameCounter > 7)
            {
                if (npc.ai[0] == 2f)
                {
                    if (npc.frame.Y < 44 * 3)
                    {
                        npc.frame.Y = 44 * 3;
                    }
                    if (npc.frame.Y > 44 * 8)
                    {
                        npc.frame.Y = 44 * 6;
                    }
                }
                else
                {
                    if (npc.frame.Y >= 44 * 6)
                    {
                        npc.frame.Y = 44 * 9;
                    }
                    if (npc.frame.Y > 44 * 11 || npc.frame.Y == 44 * 3 )
                    {
                        npc.frame.Y = 0;
                    }
                }
            }
        }


        public override bool PreDraw(SpriteBatch sb, Color dColor)
        {
            BaseDrawing.DrawTexture(sb, Main.npcTexture[npc.type], 0, npc, dColor);
            BaseDrawing.DrawTexture(sb, mod.GetTexture("Glowmasks/SagittariusMini_Glow"), 0, npc, Globals.AAColor.ZeroShield);
            return false;
        }

        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("DeactivatedDoomite"), 1);
        }

        
    }
}