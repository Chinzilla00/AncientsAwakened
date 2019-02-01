using Terraria;
using System;
using Microsoft.Xna.Framework;

namespace AAMod.NPCs.Bosses.Infinity
{
    public class InfinityCore : Infinity
    {
		
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infinity Zero");
            Main.npcFrameCount[npc.type] = 5;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.width = 38;
            npc.height = 44;
        }

        public int varTime = 0;

        public int YvarOld = 0;

        public int XvarOld = 0;
        public NPC Body;
        public Infinity iz = null;
        public bool HoriSwitch = false;
        public int f = 1;
        public float TargetDirection = (float)Math.PI / 2;
        public float s = 1;
        private int CoreFrame;
        private int CoreCounter;

        public override void AI()
        {
            Body = Main.npc[(int)npc.ai[0]];
            npc.realLife = (int)npc.ai[0];

            npc.TargetClosest(true);
            Player player = Main.player[npc.target];
            if (iz == null)
            {
                NPC npcBody = Main.npc[(int)npc.ai[0]];
                if (npcBody.type == mod.NPCType<Infinity>())
                {
                    iz = (Infinity)npcBody.modNPC;
                }
            }

            if (!Body.active)
            {
                if (Main.netMode != 1) //force a kill to prevent 'ghost heads'
                {
                    npc.life = 0;
                    npc.checkDead();
                    npc.netUpdate = true;
                }
                return;
            }
            
            if (!player.active || player.dead || !Body.active)
            {
                npc.TargetClosest(false);
                player = Main.player[npc.target];
                if (!player.active || player.dead || !Body.active)
                {
                    if (npc.timeLeft > 10)
                    {
                        npc.timeLeft = 10;
                    }
                    return;
                }
            }
            
            Vector2 moveTo = Body.Center - npc.Center;
            npc.velocity = moveTo;
        }
        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if(npc.frameCounter > 5)
            {
                npc.frameCounter = 0;
                CoreCounter += 1;
            }
            if (CoreCounter > 4)
            {
                CoreCounter = 0;
            }
            npc.frame.Y = CoreCounter * frameHeight;
        }

        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            Player player = Main.player[npc.target];
            if (player.vortexStealthActive && projectile.ranged)
            {
                damage /= 2;
                crit = false;
            }
            if (projectile.penetrate == -1 && !projectile.minion)
            {
                projectile.damage *= (int).2;
            }
            else if (projectile.penetrate >= 1)
            {
                projectile.damage *= (int).2;
            }
        }

    }
}
