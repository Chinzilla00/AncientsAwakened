using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BaseMod;
using Terraria.ID;
using Terraria.Audio;
using System.IO;

namespace AAMod.NPCs.Bosses.Hydra
{
    [AutoloadBossHead]
    public class HydraHead1 : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hydra");
            Main.npcFrameCount[npc.type] = 2;
            NPCID.Sets.TechnicallyABoss[npc.type] = true;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.lifeMax = 1300;
            npc.width = 42;
            npc.height = 54;
            npc.damage = 40;
            npc.npcSlots = 0;
            npc.dontCountMe = true;
            npc.noTileCollide = true;
            npc.boss = false;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = new LegacySoundStyle(2, 88, Terraria.Audio.SoundType.Sound);
            npc.noGravity = true;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
            Head = 0;
        }

        public bool SetLife = false;
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                writer.Write(SetLife);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                SetLife = reader.ReadBool();
            }
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.6f * bossLifeScale);
            npc.damage = (int)(npc.damage * 0.6f);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return 0f;
        }

        public override void NPCLoot()
        {
            Item.NewItem(npc.Hitbox, ModContent.ItemType<Items.Blocks.Abyssium>(), Main.rand.Next(16, 26));
            if (!Main.expertMode)
            {
                Item.NewItem(npc.Hitbox, ModContent.ItemType<Items.Boss.Hydra.HydraHide>(), Main.rand.Next(3, 7));
            }
            else
            {
                Item.NewItem(npc.Hitbox, ModContent.ItemType<Items.Boss.Hydra.HydraHide>(), Main.rand.Next(7, 17));
            }
        }

        public int Head = 0;
        public Hydra Body => (bodyNPC != null && bodyNPC.modNPC is Hydra) ? (Hydra)bodyNPC.modNPC : null;
        public NPC bodyNPC = null;
        public int damage = 0;

        public int movementVariance = 40;
        public bool fireAttack = false;

        public override void AI()
        {
            bool middleHead = Head == 0 || Head == 4 || Head == 5; 
            bool leftHead = Head == 2 || Head == 7 || Head == 8;
            if (bodyNPC == null)
            {
                NPC npcBody = Main.npc[(int)npc.ai[0]];
                if (npcBody.type == ModContent.NPCType<Hydra>())
                {
                    bodyNPC = npcBody;
                }
            }
			if(bodyNPC == null)
				return;
            if (!SetLife && Main.netMode != 1)
            {
                npc.lifeMax = bodyNPC.life / 3;
                npc.life = bodyNPC.life / 3;
                SetLife = true;
                npc.netUpdate = true;
            }
            if (!bodyNPC.active)
            {
                if (Main.netMode != 1) //force a kill to prevent 'ghosting'
                {
                    npc.life = 0;
                    npc.checkDead();
                    npc.netUpdate = true;
                }
                return;
            }

            npc.timeLeft = 100;

            npc.TargetClosest();
            
            Player targetPlayer = Main.player[npc.target];

            if (targetPlayer == null || !targetPlayer.active || targetPlayer.dead) targetPlayer = null; //deliberately set to null
            
            if (!targetPlayer.GetModPlayer<AAPlayer>().ZoneMire)
            {
                npc.damage = 80;
                npc.defense = 100;
            }
            else
            {
                npc.damage = 40;
                npc.defense = 0;
            }

            if (Main.expertMode)
            {
                damage = npc.damage / 4;
            }
            else
            {
                damage = npc.damage / 2;
            }

            if (Main.netMode != 1)
            {
                npc.ai[1]++;
                int aiTimerFire = npc.whoAmI % 3 == 0 ? 50 : npc.whoAmI % 2 == 0 ? 150 : 100; //aiTimerFire is different per head by using whoAmI (which is usually different) 
                if (leftHead) aiTimerFire += 30;
                else
                {
                    aiTimerFire = 100;
                }

                if (targetPlayer != null)
                {
                    Vector2 dir = Vector2.Normalize(targetPlayer.Center - npc.Center);
                    if (!middleHead && npc.ai[1] == aiTimerFire)
                    {
                        dir *= 9f;
                        for (int i = 0; i < 7; ++i)
                        {
                            int projID = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, dir.X, dir.Y, mod.ProjectileType("AcidProj"), damage, 0f, Main.myPlayer);
                            Main.projectile[projID].netUpdate = true;
                        }
                    }
                    else
                    if (middleHead && npc.ai[1] >= 100 && npc.ai[1] % 10 == 0)
                    {
                        dir *= 6f;
                        dir.X += Main.rand.Next(-1, 1) * 0.5f;
                        dir.Y += Main.rand.Next(-1, 1) * 0.5f;
                        int projID = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, dir.X, dir.Y, mod.ProjectileType("HydraBreath"), damage, 0f, Main.myPlayer);
                        Main.projectile[projID].netUpdate = true;
                    }
                }
                if ((!middleHead && npc.ai[1] >= aiTimerFire) || (middleHead && npc.ai[1] >= 100 && npc.ai[1] % 10 == 0))
                {
                    npc.frame.Y = 54;
                }
                else
                {
                    npc.frame.Y = 0;
                }
                if (npc.ai[1] >= 200) //pick random spot to move head to
                {
                    npc.ai[1] = 0;
                    npc.ai[2] = Main.rand.Next(-movementVariance, movementVariance);
                    npc.ai[3] = Main.rand.Next(-movementVariance, movementVariance);
                    npc.netUpdate = true;
                }
            }

            Vector2 nextTarget = Body.npc.Center + HeadPos() + new Vector2(npc.ai[2], npc.ai[3]);

			float dist = Vector2.Distance(nextTarget, npc.Center);
            if (dist < 40f)
            {
                npc.velocity *= 0.9f;
                if (Math.Abs(npc.velocity.X) < 0.05f) npc.velocity.X = 0f;
                if (Math.Abs(npc.velocity.Y) < 0.05f) npc.velocity.Y = 0f;
            }else
            if (dist > 200f) //teleport to keep up with body
            {
                npc.Center = Body.npc.Center;
				npc.netUpdate = true;
            }	
            else
            {
                npc.velocity = Vector2.Normalize(nextTarget - npc.Center);
                npc.velocity *= 5f;
            }
            npc.position += Body.npc.position - Body.npc.oldPosition;
            npc.spriteDirection = -1;
        }

        public Vector2 HeadPos()
        {
            switch (Head)
            {
                default:
                    return new Vector2(0, -110);
                case 1:
                    return new Vector2(80, -100);
                case 2:
                    return new Vector2(-80, -100);
                case 3:
                    return new Vector2(-30, -110);
                case 4:
                    return new Vector2(30, -110);
                case 5:
                    return new Vector2(70, -100);
                case 6:
                    return new Vector2(90, -90);
                case 7:
                    return new Vector2(-70, -100);
                case 8:
                    return new Vector2(-90, -90);
            }
        }

        public float moveSpeed = 16f; 
        public void MoveToPoint(Vector2 point)
        {
            float velMultiplier = 1f;
            Vector2 dist = point - npc.Center;
            float length = dist == Vector2.Zero ? 0f : dist.Length();
            if (length < moveSpeed)
            {
                velMultiplier = MathHelper.Lerp(0f, 1f, length / moveSpeed);
            }
            npc.velocity = length == 0f ? Vector2.Zero : Vector2.Normalize(dist);
            npc.velocity *= moveSpeed;
            npc.velocity *= velMultiplier;
        }

        public override void BossHeadRotation(ref float rotation)
        {
            rotation = npc.rotation;
        }

        public override bool PreNPCLoot()
        {
            if (npc.type == mod.NPCType("HydraHead1"))
            {
                int a = NPC.NewNPC((int)bodyNPC.Center.X, (int)bodyNPC.Center.Y, ModContent.NPCType<HydraHead4>(), 0, bodyNPC.whoAmI);
                Body.Head4 = Main.npc[a];
                int b = NPC.NewNPC((int)bodyNPC.Center.X, (int)bodyNPC.Center.Y, ModContent.NPCType<HydraHead5>(), 0, bodyNPC.whoAmI);
                Body.Head5 = Main.npc[b];
                return false;
            }
            if (npc.type == mod.NPCType("HydraHead2"))
            {
                int a = NPC.NewNPC((int)bodyNPC.Center.X, (int)bodyNPC.Center.Y, ModContent.NPCType<HydraHead6>(), 0, bodyNPC.whoAmI);
                Body.Head6 = Main.npc[a];
                int b = NPC.NewNPC((int)bodyNPC.Center.X, (int)bodyNPC.Center.Y, ModContent.NPCType<HydraHead7>(), 0, bodyNPC.whoAmI);
                Body.Head7 = Main.npc[b];
                return false;
            }
            if (npc.type == mod.NPCType("HydraHead3"))
            {
                int a = NPC.NewNPC((int)bodyNPC.Center.X, (int)bodyNPC.Center.Y, ModContent.NPCType<HydraHead8>(), 0, bodyNPC.whoAmI);
                Body.Head8 = Main.npc[a];
                int b = NPC.NewNPC((int)bodyNPC.Center.X, (int)bodyNPC.Center.Y, ModContent.NPCType<HydraHead9>(), 0, bodyNPC.whoAmI);
                Body.Head9 = Main.npc[b];
                return false;
            }
            else 
            {
                return true;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            return false;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.type == mod.NPCType("HydraHead2"))
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/HydraHeadGoreB"), 1f);
            }
            else
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/HydraHeadGoreR"), 1f);
            }
            Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/HydraHeadGore1"), 1f);
            Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/HydraHeadGore2"), 1f);
            Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/HydraHeadGore3"), 1f);
            Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/HydraHeadGore4"), 1f);
        }
    }
}
