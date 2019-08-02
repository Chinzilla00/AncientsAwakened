using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;
using AAMod.NPCs.Bosses.Athena;

namespace AAMod.NPCs.Enemies.Sky
{
	public class Seraph : ModNPC
	{
        public override void SetStaticDefaults()
		{
            Main.npcFrameCount[npc.type] = 4;		
		}			
		
        public override void SetDefaults()
        {
            npc.width = 60;
            npc.height = 40;
            npc.value = BaseUtility.CalcValue(0, 0, 10, 0);
            npc.npcSlots = 1;
			npc.aiStyle = -1;
            npc.lifeMax = 500;
            npc.defense = 20;
            npc.damage = 55;
            npc.knockBackResist = 0.3f;
			npc.noGravity = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.noTileCollide = true;
            if (npc.type == mod.NPCType<SeraphA>())
            {
                npc.alpha = 255;
            }
        }

        public override bool PreAI()
        {
            if (npc.type == mod.NPCType<SeraphA>() && !(NPC.AnyNPCs(mod.NPCType<Athena>()) || NPC.AnyNPCs(mod.NPCType<AthenaA>())))
            {
                npc.velocity.Y -= .2f;
                npc.velocity.X *= .95f;
                if (npc.position.Y + npc.velocity.Y <= 0f && Main.netMode != 1) { BaseAI.KillNPC(npc); npc.netUpdate = true; }
                return false;
            }
            return true;
        }

		public override void AI()
		{
			BaseAI.AIFlier(npc, ref npc.ai, true, 0.15f, 0.08f, 8f, 7f, false, 300);

            if (npc.alpha > 0)
            {
                npc.alpha -= 4;
            }
            else
            {
                npc.alpha = 0;
            }

            Player player = Main.player[npc.target];

            if (npc.ai[3]++ > 30 && Main.netMode != 1)
            {
                int projType = mod.ProjectileType<SeraphFeather>();
                float spread = 30f * 0.0174f;
                Vector2 dir = Vector2.Normalize(player.Center - npc.Center);
                dir *= 14f;
                float baseSpeed = (float)Math.Sqrt((dir.X * dir.X) + (dir.Y * dir.Y));
                double startAngle = Math.Atan2(dir.X, dir.Y) - .1d;
                double deltaAngle = spread / 6f;
                for (int i = 0; i < 3; i++)
                {
                    double offsetAngle = startAngle + (deltaAngle * i);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), projType, npc.damage / 4, 2, Main.myPlayer);
                }
                npc.ai[3] = 0;
                npc.netUpdate = true;
            }

            npc.spriteDirection = npc.direction;
			npc.rotation = (npc.velocity.X * 0.05f);
		}

		public override void FindFrame(int frameHeight)
		{
            if (npc.velocity.X > 0f)
            {
                npc.spriteDirection = 1;
            }
            if (npc.velocity.X < 0f)
            {
                npc.spriteDirection = -1;
            }
            npc.rotation = npc.velocity.X * 0.1f;
            if (npc.type == 210 || npc.type == 211)
            {
                npc.frameCounter += 1.0;
                npc.rotation = npc.velocity.X * 0.2f;
            }
            npc.frameCounter += 1.0;
            if (npc.frameCounter >= 6.0)
            {
                npc.frame.Y = npc.frame.Y + frameHeight;
                npc.frameCounter = 0.0;
            }
            if (npc.frame.Y >= frameHeight * Main.npcFrameCount[npc.type])
            {
                npc.frame.Y = 0;
            }
        }

        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SeraphFeather"));
        }
    }
}