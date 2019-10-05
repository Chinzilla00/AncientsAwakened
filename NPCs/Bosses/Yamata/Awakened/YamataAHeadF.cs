using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using AAMod.NPCs.Bosses.Yamata.Awakened;
using BaseMod;
using System.IO;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.NPCs.Bosses.Yamata.Awakened
{
    [AutoloadBossHead]
    public class YamataAHeadF : ModNPC
    {
		public bool isAwakened = false;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Yamata");
            Main.npcFrameCount[npc.type] = 3;
            NPCID.Sets.TechnicallyABoss[npc.type] = true;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.lifeMax = 30000;
            npc.width = 64;
            npc.height = 48;
            npc.npcSlots = 0;
            npc.dontCountMe = true;
            npc.noTileCollide = true;
            npc.boss = false;
            npc.noGravity = true;
            npc.chaseable = false;
            npc.damage = 250;
            NPCID.Sets.TechnicallyABoss[npc.type] = true;
            npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/Sounds/YamataRoar");
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
            if (AAWorld.downedShen)
            {
                npc.lifeMax = 50000;
                npc.damage = 350;
            }
            
            npc.lifeMax = 35000;
            npc.damage = 210;
            npc.width = 46;
            npc.height = 46;
            isAwakened = true;
            npc.scale *= 2;
            npc.knockBackResist *= 0.1f;
        }

        public float[] internalAI = new float[4];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                writer.Write(internalAI[0]);
                writer.Write(internalAI[1]);
                writer.Write(internalAI[2]);
                writer.Write(internalAI[3]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                internalAI[0] = reader.ReadFloat();
                internalAI[1] = reader.ReadFloat();
                internalAI[2] = reader.ReadFloat();
                internalAI[3] = reader.ReadFloat();
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return 0f;
        }

		public Yamata Body = null;
        public Yamata Head = null;
        public bool killedbyplayer = true;	
		public bool leftHead = false;
        public bool fireAttack = false;
		public int distFromBodyX = 110; //how far from the body to centeralize the movement points. (X coord)
		public int distFromBodyY = 150; //how far from the body to centeralize the movement points. (Y coord)
		public int movementVariance = 60; //how far from the center point to move.

        public override void AI()
        {
            npc.defDamage = isAwakened ? 200 : 180;
            if (Body == null)
            {
                NPC npcBody = Main.npc[(int)npc.ai[0]];
                if (npcBody.type == mod.NPCType<Yamata>() || npcBody.type == mod.NPCType<YamataA>())
                {
                    Body = (Yamata)npcBody.modNPC;
                }
            }
            if (Body == null)
                return;			

            npc.alpha = Body.npc.alpha;

            if (npc.alpha > 0)
            {
                npc.damage = 0;
            }
            else
            {
                npc.damage = npc.defDamage;
            }
            npc.TargetClosest();
            Player targetPlayer = Main.player[npc.target];
            if (targetPlayer == null || !targetPlayer.active || targetPlayer.dead) targetPlayer = null; //deliberately set to null


            float playerDistance = targetPlayer == null ? 99999f : Vector2.Distance(targetPlayer.Center, npc.Center);
            if (!Body.npc.active)
            {
                if (Main.netMode != 1) //force a kill to prevent 'ghost hands'
                {
                    npc.life = 0;
                    npc.checkDead();
                    npc.netUpdate = true;
                    killedbyplayer = false;
                }
                return;
            }
            Vector2 nextTarget = Body.npc.Center + new Vector2(npc.ai[1], npc.ai[2]);
            
            switch ((int)internalAI[0])
            {
                case 0: //charge up

                    //insert charging dust here

                    if (++internalAI[1] > 180)
                    {
                        internalAI[0]++;
                        internalAI[1] = 0;
                        npc.netUpdate = true;
                        if (Main.netMode != 1)
                        {
                            float ai0 = (float)Math.PI * 2 / 300 * (npc.ai[3] == 2 ? 1 : -1) * Math.Sign(npc.ai[1]);
                            Projectile.NewProjectile(npc.Center, Vector2.UnitY, mod.ProjectileType("YamataWaveDeathray"), npc.damage / 4, 0f, Main.myPlayer, ai0, npc.whoAmI);
                        }
                    }
                    break;

                case 1: //idle while firing laser
                    if (++internalAI[1] > 300)
                    {
                        internalAI[0]++;
                        internalAI[1] = 0;
                        npc.netUpdate = true;
                    }
                    break;

                case 2: //shoot shit
                    internalAI[2] += npc.ai[3];
                    if (internalAI[2] > 180)
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
                    internalAI[2] += npc.ai[3];
                    if (++internalAI[2] > 60)
                    {
                        internalAI[2] = 0;
                        if (Main.netMode != 1)
                            Projectile.NewProjectile(npc.Center, npc.DirectionTo(Main.player[npc.target].Center) * 20f, mod.ProjectileType("YamataABreath"), npc.damage / 4, 0f, Main.myPlayer);
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
                            Projectile.NewProjectile(npc.Center, npc.DirectionTo(Main.player[npc.target].Center), mod.ProjectileType("YamataDeathray"), npc.damage / 4, 0f, Main.myPlayer, 0f, npc.whoAmI);
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
                    internalAI[2] += npc.ai[3];
                    if (internalAI[2] > 360)
                    {
                        internalAI[2] = 0;
                        if (Main.netMode != 1)
                            Projectile.NewProjectile(npc.Center, Vector2.UnitY * 5, mod.ProjectileType("YamataAShockBomb"), npc.damage / 4, 0f, Main.myPlayer, npc.target);
                    }
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
                    internalAI[2] += npc.ai[3];
                    if (internalAI[2] > 120)
                    {
                        internalAI[2] = 0;
                        if (Main.netMode != 1)
                        {
                            if (npc.ai[3] == 3)
                                Projectile.NewProjectile(npc.Center, Vector2.UnitY * 10, mod.ProjectileType("AbyssalThunder"), npc.damage / 4, 0f, Main.myPlayer);
                            else
                                Projectile.NewProjectile(npc.Center, npc.DirectionTo(Main.player[npc.target].Center) * 7f, mod.ProjectileType("YamataABomb"), npc.damage / 4, 0f, Main.myPlayer);
                        }
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

            float dist = Vector2.Distance(nextTarget, npc.Center);
            /*if (YamataHead.EATTHELITTLEMAGGOT && playerDistance < 300f)
            {
                BaseAI.AIFlier(npc, ref customAI, true, .5f, .8f, 5, 5, false, 300);
            }
            else*/
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
            //npc.position += Body.npc.position - Body.npc.oldPosition;
            npc.spriteDirection = -1;
            if (Body.TeleportMe1)
            {
                Body.TeleportMe1 = false;
                npc.Center = Body.npc.Center;
                return;
            }
            if (Body.TeleportMe2)
            {
                Body.TeleportMe2 = false;
                npc.Center = Body.npc.Center;
                return;
            }
            if (Body.TeleportMe3)
            {
                Body.TeleportMe3 = false;
                npc.Center = Body.npc.Center;
                return;
            }
            if (Body.TeleportMe4)
            {
                Body.TeleportMe4 = false;
                npc.Center = Body.npc.Center;
                return;
            }
            if (Body.TeleportMe5)
            {
                Body.TeleportMe5 = false;
                npc.Center = Body.npc.Center;
                return;
            }
            if (Body.TeleportMe6)
            {
                Body.TeleportMe6 = false;
                npc.Center = Body.npc.Center;
                return;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (fireAttack || YamataHead.EATTHELITTLEMAGGOT)
            {
                if (npc.frameCounter < 5)
                {
                    npc.frame.Y = 1 * frameHeight;
                }
                else if (npc.frameCounter < 10)
                {
                    npc.frame.Y = 2 * frameHeight;
                }
            }
            else
            {
                npc.frameCounter = 0;
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                CombatText.NewText(npc.getRect(), new Color(45, 46, 70), "OWIE!!!", true, false);
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            return false;
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
            else if (projectile.type == ProjectileID.LastPrismLaser)
            {
                damage = (int)(damage * .05f);
            }
        }

        public override void BossHeadRotation(ref float rotation)
        {
            rotation = npc.rotation;
        }
        
		

        public override bool CheckActive()
        {
            if (NPC.AnyNPCs(mod.NPCType<Yamata>()) || NPC.AnyNPCs(mod.NPCType<YamataA>()))
            {
                return false;
            }
            return true;
        }

        private int HomeOnTarget()
        {
            const float homingMaximumRangeInPixels = 400;

            int selectedTarget = -1;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC n = Main.npc[i];
                if (n.type == NPCID.Bunny)
                {
                    float distance = npc.Distance(n.Center);
                    if (distance <= homingMaximumRangeInPixels &&
                        (
                            selectedTarget == -1 || //there is no selected target
                            npc.Distance(Main.npc[selectedTarget].Center) > distance) 
                    )
                        selectedTarget = i;
                }
            }

            return selectedTarget;
        }
    }
}
