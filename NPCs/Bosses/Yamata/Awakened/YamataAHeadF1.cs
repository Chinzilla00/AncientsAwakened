using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace AAMod.NPCs.Bosses.Yamata.Awakened
{
    [AutoloadBossHead]
    public class YamataAHeadF1 : YamataHeadF1
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Yamata no Orochi");
            Main.npcFrameCount[npc.type] = 9;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.lifeMax = 35000;
            npc.damage = 210;
            npc.width = 46;
            npc.height = 46;
			isAwakened = true;
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            
            if (npc.frameCounter > 5)
            {
                npc.frameCounter = 0;
                npc.frame.Y += frameHeight;
                if (npc.frame.Y > frameHeight * 3)
                {
                    npc.frame.Y = 0;
                }
            }
            if (fireAttack || YamataHead.EATTHELITTLEMAGGOT)
            {
                if (npc.frameCounter < 5)
                {
                    npc.frame.Y = frameHeight * 4;
                }
                if (npc.frameCounter > 10)
                {
                    npc.frame.Y += frameHeight;
                    npc.frameCounter = 5;
                    if (npc.frame.Y > frameHeight * 8)
                    {
                        npc.frame.Y = frameHeight * 5;
                    }
                }
            }
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
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                CombatText.NewText(npc.getRect(), new Color(146, 30, 68), Lang.BossChat("YamataAHead"), true, false);
            }
        }
        public override bool PreNPCLoot()
        {
            if (!Main.player[npc.target].dead || Math.Abs(npc.position.X - Main.player[npc.target].position.X) < 6000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) < 6000f)
            {
                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType<YamataSoul>());
            }
            return false;
        }
    }
}
