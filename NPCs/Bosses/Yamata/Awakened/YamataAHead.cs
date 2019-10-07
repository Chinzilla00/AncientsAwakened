using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace AAMod.NPCs.Bosses.Yamata.Awakened
{
    [AutoloadBossHead]
    public class YamataAHead : YamataHead
    {
        public override void SetStaticDefaults()
        {
			base.SetStaticDefaults();
            DisplayName.SetDefault("Yamata no Orochi");
            Main.npcFrameCount[npc.type] = 3;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.5f * bossLifeScale);
            npc.damage = (int)(npc.damage * .8f);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Yamata2");

            npc.damage = 350;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
            npc.scale *= 2;
            npc.knockBackResist *= 0.1f;
        }

        public override void AI()
        {
            int attackpower = 160;
            if (Main.expertMode)
            {
                damage = npc.damage / 4;
            }
            else
            {
                damage = npc.damage / 2;
            }
            if (Body == null)
            {
                NPC npcBody = Main.npc[(int)npc.ai[0]];
                if (npcBody.type == mod.NPCType<Yamata>() || npcBody.type == mod.NPCType<YamataA>())
                {
                    Body = npcBody;
                    yamata = (Yamata)npcBody.modNPC;
                }
            }
            if (Body == null)
                return;
            if (!Body.active)
            {
                if (Main.netMode != 1) //force a kill to prevent 'ghost hands'
                {
                    npc.life = 0;
                    npc.checkDead();
                    npc.netUpdate = true;
                }
                return;
            }

            npc.realLife = Body.whoAmI;
            npc.timeLeft = 100;
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];

            if (Yamata.TeleportMeBitch)
            {
                Yamata.TeleportMeBitch = false;
                npc.Center = yamata.npc.Center;
                return;
            }

            npc.alpha = Body.alpha;
            if (npc.alpha > 0)
            {
                npc.damage = 0;
            }
            else
            {
                npc.damage = attackpower;
            }

            //int roarSound = mod.GetSoundSlot(SoundType.Item, "Sounds/Sounds/YamataRoar");

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

            npc.rotation = 0;
            Vector2 nextTarget = new Vector2(Body.Center.X + npc.ai[1], Body.Center.Y + npc.ai[2]);
            float dist = Vector2.Distance(nextTarget, npc.Center);
            if (dist < 100)
            {
                npc.velocity *= 0.9f;
                if (Math.Abs(npc.velocity.X) < 0.05f) npc.velocity.X = 0f;
                if (Math.Abs(npc.velocity.Y) < 0.05f) npc.velocity.Y = 0f;
            }
            else
            {
                npc.velocity = Vector2.Normalize(nextTarget - npc.Center);
                npc.velocity *= 10f;
            }
            //npc.position += Body.position - Body.oldPosition;

            switch ((int)internalAI[0])
            {
                case 0: //while other heads are charging
                    if (++internalAI[2] > 60)
                    {
                        internalAI[2] = 0;
                        if (Main.netMode != 1)
                            Projectile.NewProjectile(npc.Center, Vector2.UnitY * 5, mod.ProjectileType("YamataAShockBomb"), npc.damage / 4, 0f, Main.myPlayer, npc.target);
                    }
                    if (++internalAI[1] > 180)
                    {
                        internalAI[0]++;
                        internalAI[1] = 0;
                        internalAI[2] = 0;
                        npc.netUpdate = true;
                    }
                    break;

                case 1: //while other heads are shooting waveray
                    /*if (++internalAI[2] > 60)
                    {
                        internalAI[2] = 0;
                        if (Main.netMode != 1)
                            Projectile.NewProjectile(npc.Center, npc.DirectionTo(Main.player[npc.target].Center) * 10f, mod.ProjectileType("AbyssalThunder"), npc.damage / 4, 0f, Main.myPlayer);
                    }*/
                    if (++internalAI[1] > 300)
                    {
                        internalAI[0]++;
                        internalAI[1] = 0;
                        npc.netUpdate = true;
                    }
                    break;

                case 2: //shoot shit
                    if (++internalAI[2] > 20)
                    {
                        internalAI[2] = 0;
                        if (Main.netMode != 1)
                            Projectile.NewProjectile(npc.Center, npc.DirectionTo(Main.player[npc.target].Center) * 5f, mod.ProjectileType("YamataAVenom2"), npc.damage / 4, 0f, Main.myPlayer);
                    }
                    if (++internalAI[1] > 240)
                    {
                        internalAI[0]++;
                        internalAI[1] = 0;
                        internalAI[2] = 0;
                        npc.netUpdate = true;
                    }
                    break;

                case 3: //breathe lingering flame
                    if (++internalAI[2] > 60)
                    {
                        internalAI[2] = 0;
                        if (Main.netMode != 1)
                            Projectile.NewProjectile(npc.Center, npc.DirectionTo(Main.player[npc.target].Center) * 7f, mod.ProjectileType("YamataABomb"), npc.damage / 4, 0f, Main.myPlayer);
                    }
                    if (++internalAI[1] > 180)
                    {
                        internalAI[0]++;
                        internalAI[1] = 0;
                        internalAI[2] = 0;
                        npc.netUpdate = true;
                    }
                    break;

                case 4: //shoot direct aim deathrays
                    if (internalAI[1] == npc.ai[3] * 60)
                    {
                        if (Main.netMode != 1)
                            Projectile.NewProjectile(npc.Center, npc.DirectionTo(Main.player[npc.target].Center), mod.ProjectileType("YamataDeathray"), npc.damage, 0f, Main.myPlayer, 0f, npc.whoAmI);
                    }
                    if (++internalAI[1] > 360)
                    {
                        internalAI[0]++;
                        internalAI[1] = 0;
                        npc.netUpdate = true;
                    }
                    break;

                case 5: //shoot the shit again
                    goto case 2;

                case 6: //drop meteor that creates ripples across ground
                    if (++internalAI[1] > 420)
                    {
                        internalAI[0]++;
                        internalAI[1] = 0;
                        internalAI[2] = 0;
                        npc.netUpdate = true;
                    }
                    break;

                case 7: //pause, let previous waves disperse
                    if (++internalAI[1] > 120)
                    {
                        internalAI[0]++;
                        internalAI[1] = 0;
                        npc.netUpdate = true;
                    }
                    break;

                case 8: //breathe the lingering shit
                    goto case 3;

                case 9: //some mix of 2 attacks he already does, something homing + something directly aimed
                    if (++internalAI[2] > 60)
                    {
                        internalAI[2] = 0;
                        Projectile.NewProjectile(npc.Center, Vector2.UnitY * 10, mod.ProjectileType("AbyssalThunder"), npc.damage / 4, 0f, Main.myPlayer);
                    }
                    if (++internalAI[1] > 360)
                    {
                        internalAI[0]++;
                        internalAI[1] = 0;
                        internalAI[2] = 0;
                        npc.netUpdate = true;
                    }
                    break;

                case 10: //shoot the shit again
                    goto case 2;

                default:
                    internalAI[0] = 0;
                    npc.netUpdate = true;
                    goto case 0;
            }
        }
    }
}
