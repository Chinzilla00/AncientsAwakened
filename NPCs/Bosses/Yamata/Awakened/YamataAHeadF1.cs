using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AAMod.NPCs.Bosses.Yamata.Awakened;
using BaseMod;
using System.IO;

namespace AAMod.NPCs.Bosses.Yamata.Awakened
{
    [AutoloadBossHead]
    public class YamataAHeadF1 : YamataHeadF1
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Yamata Awakened");
            Main.npcFrameCount[npc.type] = 9;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.lifeMax = 35000;
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

		public override bool PreNPCLoot()
        {
            NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType<YamataSoul>());
            BaseUtility.Chat("OWIE!!!", new Color(146, 30, 68));
            return false;
        }
    }
}
