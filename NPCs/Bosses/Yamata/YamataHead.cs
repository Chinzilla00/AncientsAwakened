using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using BaseMod;
using AAMod.NPCs.Bosses.Yamata.Awakened;
using System.IO;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.NPCs.Bosses.Yamata
{
    [AutoloadBossHead]
    public class YamataHead : ModNPC
    {
		public bool isAwakened = false;
        public int projDamage = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Yamata");
            Main.npcFrameCount[npc.type] = 3;
        }

        public override void SetDefaults()
        {
			npc.lifeMax = 550000;
            npc.damage = 150;
            npc.defense = 100;
            npc.width = 78;
            npc.height = 60;
            npc.npcSlots = 0;
            npc.noTileCollide = true;
            npc.noGravity = true;
            npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/Sounds/YamataRoar");
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Yamata");
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
            if (AAWorld.downedShen)
            {
                npc.damage = 350;
            }
        }

        public int varTime = 0;

        public int YvarOld = 0;

        public int XvarOld = 0;
        public int numberOfAttacks = 0;
        public int endAttack = 0;
        public int damage = 0;
        public float moveSpeedBoost = .04f;
        public NPC Body;
        public Yamata yamata = null;
        public bool HoriSwitch = false;
        public int f = 1;
        public float TargetDirection = (float)Math.PI / 2;
        public float s = 1;
        public Projectile Breath;
        public static bool fireAttack;
        private int attackFrame;
        private int attackCounter;
        private int attackTimer;
        public int fireTimer = 0;
        public static bool EATTHELITTLEMAGGOT = false;
        public bool Quote1;
        public bool Quote2;
        public bool Quote3;
        public bool Quote4;
        public bool Quote5;
        public bool Quote6;
        public bool QuoteSaid;
        public static int HeadFrame = 0;

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
                writer.Write(EATTHELITTLEMAGGOT);
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
                EATTHELITTLEMAGGOT = reader.ReadBool();
            }
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = (int)(npc.damage * .8f);
            npc.lifeMax = (int)(npc.lifeMax * 0.5f * bossLifeScale);
        }

        public override void AI()
        {
            int attackpower = isAwakened ? 160 : 130;
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
			if(Body == null)
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

            int roarSound = mod.GetSoundSlot(SoundType.Item, "Sounds/Sounds/YamataRoar");

            Vector2 PlayerDistance = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
            float num433 = 6f;
            float PlayerPosX = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) - PlayerDistance.X;
            float PlayerPosY = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - PlayerDistance.Y;
            float PlayerPos = (float)Math.Sqrt(PlayerPosX * PlayerPosX + PlayerPosY * PlayerPosY);
            PlayerPos = num433 / PlayerPos;
            PlayerPosX *= PlayerPos;
            PlayerPosY *= PlayerPos;
            PlayerPosY += Main.rand.Next(-40, 41) * 0.01f;
            PlayerPosX += Main.rand.Next(-40, 41) * 0.01f;
            PlayerPosY += npc.velocity.Y * 0.5f;
            PlayerPosX += npc.velocity.X * 0.5f;
            PlayerDistance.X -= PlayerPosX * 1f;
            PlayerDistance.Y -= PlayerPosY * 1f;

            if (npc.alpha <= 0)
            {
                internalAI[2]++;
            }
            if (internalAI[2] == 399)
            {
                QuoteSaid = false;
                Main.PlaySound(roarSound, npc.Center);
                int AttackType = 2;
                int AwakenedAttackType = 4;
                if (!isAwakened && (NPC.AnyNPCs(mod.NPCType<YamataHeadF1>()) || NPC.AnyNPCs(mod.NPCType<YamataHeadF2>())))
                {
                    AttackType = 4;
                }
                if (isAwakened && (NPC.AnyNPCs(mod.NPCType<YamataAHeadF1>()) || NPC.AnyNPCs(mod.NPCType<YamataAHeadF2>())))
                {
                    AwakenedAttackType = 6;
                }
                internalAI[1] = isAwakened ? Main.rand.Next(AwakenedAttackType) : Main.rand.Next(AttackType);
            }

            if (internalAI[2] >= 400)
            {
                Attacks(internalAI[1]);
            }

            if (internalAI[2] >= 600)
            {
                EATTHELITTLEMAGGOT = false;
                internalAI[2] = 0;
            }

            if (npc.ai[3] == 1)
            {
                attackCounter++;
                if (attackCounter > 10)
                {
                    attackFrame++;
                    attackCounter = 0;
                }
                if (attackFrame >= 3)
                {
                    attackFrame = 2;
                }
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
            fireTimer++;
            if (fireTimer >= 240 && npc.ai[3] == 0)
            {
                Main.PlaySound(roarSound, npc.Center);
                npc.ai[3] = 1;
                fireTimer = 0;
            }
            projDamage = Main.expertMode ? (npc.damage / 2) : (npc.damage / 4);
            if (npc.ai[3] == 1)
            {
                attackTimer++;
                if (Main.rand.Next(3) == 0)
                {
                    if (attackTimer == 40)
                    {
                        Main.PlaySound(2, (int)npc.Center.X, (int)npc.Center.Y, 20);
                        int proj2 = Projectile.NewProjectile(npc.Center.X + Main.rand.Next(-20, 20), npc.Center.Y + Main.rand.Next(-20, 20), npc.velocity.X * 2f, npc.velocity.Y * 2f, mod.ProjectileType(isAwakened ? "YamataABomb" : "YamataBomb"), projDamage, 0, Main.myPlayer);
                        Main.projectile[proj2].damage = projDamage;
                        attackTimer = 0;
                        attackFrame = 0;
                        attackCounter = 0;
                    }
                    if (attackTimer >= 80)
                    {
                        npc.ai[3] = 0;
                    }
                }
                else
                {
                    if (attackTimer == 8 || attackTimer == 16 || attackTimer == 24 || attackTimer == 32 || attackTimer == 40 || attackTimer == 48 || attackTimer == 56 || attackTimer == 64 || attackTimer == 72 || attackTimer == 79)
                    {
                        Main.PlaySound(2, (int)npc.Center.X, (int)npc.Center.Y, 20);
                        for (int i = 0; i < 5; ++i)
                        {
                            if (Main.netMode != 1)
                            {
                                Projectile.NewProjectile(PlayerDistance.X, PlayerDistance.Y, PlayerPosX * 2f, PlayerPosY * 2f, mod.ProjectileType(isAwakened ? "YamataABreath" : "YamataBreath"), projDamage, 0f, Main.myPlayer);
                            }
                        }
                        
                    }
                    if (attackTimer >= 80)
                    {
                        npc.ai[3] = 0;
                        attackTimer = 0;
                        attackFrame = 0;
                        attackCounter = 0;
                    }
                }

            }

            Vector2 moveTo = new Vector2(Body.Center.X + npc.ai[1], Body.Center.Y - (130f + npc.ai[2])) - npc.Center;
            npc.velocity = moveTo * moveSpeedBoost;
            npc.rotation = 0;
            npc.position += Body.position - Body.oldPosition;
        }


        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }

        public void Attacks(float AttackType)
        {
            Player player = Main.player[npc.target];
            if (!isAwakened)
            {
                if (AttackType == 0f)
                {
                    if (!QuoteSaid)
                    {
                        if (Main.netMode != 1) BaseUtility.Chat((!Quote1) ? "TASTE ACID YOU UNBEARABLE MAGGOT!!!" : "STOP MOVING AND LET ME MELT YOU!!!", new Color(45, 46, 70));
                        QuoteSaid = true;
                        Quote1 = true;
                    }
                    BaseAI.ShootPeriodic(npc, new Vector2(player.position.X, player.position.Y - 1), player.width, player.height, mod.ProjectileType<YamataVenom>(), ref internalAI[3], 6, projDamage, 9f, true, new Vector2(20f, 15f));
                }
                if (AttackType == 1f)
                {
                    if (!QuoteSaid)
                    {
                        if (Main.netMode != 1) BaseUtility.Chat((!Quote3) ? "Down Down DOWN THE VENOM GOES!!! When it will it stop? WHO KNOWS?! NYEHEHEHEHEHEH!!!" : "DIEDIEDIEDIEDIEDIEDIEDIIIIIIIIIIIIIIIE!!!", new Color(45, 46, 70));
                        QuoteSaid = true;
                        Quote3 = true;
                    }
                    BaseAI.ShootPeriodic(npc, new Vector2(player.position.X, -4f), player.width, player.height, mod.ProjectileType<YamataStorm>(), ref internalAI[3], 40, projDamage, 10f, true, new Vector2(20f, 15f));
                }
                if (AttackType == 2f)
                {
                    if (!QuoteSaid)
                    {
                        if (Main.netMode != 1) BaseUtility.Chat((!Quote3) ? "BAM! BOOM! I'LL BLOW YOU INTO NEXT SUNDAY!!!" : "NGAAAAAAAAAAAAAAAAAH!!!", new Color(45, 46, 70));
                        QuoteSaid = true;
                        Quote3 = true;
                    }
                    BaseAI.ShootPeriodic(npc, new Vector2(player.position.X, player.position.Y - 1), player.width, player.height, mod.ProjectileType<YamataBlast>(), ref internalAI[3], 15, projDamage, 10f, true, new Vector2(20f, 15f));
                }
                if (AttackType == 3f)
                {
                    if (!QuoteSaid)
                    {
                        if (Main.netMode != 1) BaseUtility.Chat((!Quote4) ? ("GET THEM! EAT THEM! JUST GET " + (player.Male ? "HIM" : "HER") + " OUT OF MY FACE!!!") : "I’VE EATEN RABBITS MORE INTIMIDATING THAN YOU!", new Color(45, 46, 70));
                        QuoteSaid = true;
                        Quote4 = true;
                    }
                    EATTHELITTLEMAGGOT = true;
                }
            }
            else
            {
                if (AttackType == 0f)
                {
                    if (!QuoteSaid)
                    {
                        if (Main.netMode != 1) BaseUtility.Chat((!Quote1) ? "HOPE YOU BROUGHT YOUR UMBRELLA! BECAUSE IT’S RAINING PAIN!!! NYEHEHEHEHEHEHEHEH!!!" : "DOWN COMES THE VENOM!!!NYEHEHEHEHEHEHEHEH!", new Color(146, 30, 68));
                        QuoteSaid = true;
                        Quote1 = true;
                    }
                    BaseAI.ShootPeriodic(npc, new Vector2(Main.rand.Next(-2, 2), -1f), player.width, player.height, mod.ProjectileType<YamataStorm>(), ref internalAI[3], 30, projDamage, 10f, true, new Vector2(20f, 15f));
                }
                if (AttackType == 1f)
                {
                    if (!QuoteSaid)
                    {
                        if (Main.netMode != 1) BaseUtility.Chat((!Quote2) ? "EAT ECTOPLASM YOU LITTLE WRETCH" : "NYAAAAAAAAAAAH!!!", new Color(146, 30, 68));
                        QuoteSaid = true;
                        Quote2 = true;
                    }
                    BaseAI.ShootPeriodic(npc, new Vector2(player.position.X, player.position.Y - 1), player.width, player.height, mod.ProjectileType<HomingSoul>(), ref internalAI[3], 15, projDamage, 10f, true, new Vector2(20f, 15f));
                }
                if (AttackType == 2f)
                {
                    if (!QuoteSaid)
                    {
                        if (Main.netMode != 1) BaseUtility.Chat((!Quote3) ? "WHOOPS! DROPPED ACID! Hope you're not degradable..!" : "WHOOPS! DROPPED ACID AGAIN! NYEHEHEHEHEHEHEHEHEHEHEHEH", new Color(146, 30, 68));
                        QuoteSaid = true;
                        Quote3 = true;
                    }
                    BaseAI.ShootPeriodic(npc, new Vector2(player.position.X, player.position.Y - 1), player.width, player.height, mod.ProjectileType<YamataShot>(), ref internalAI[3], 10, projDamage, 10f, true, new Vector2(20f, 15f));
                }
                if (AttackType == 3f)
                {
                    if (!QuoteSaid)
                    {
                        if (Main.netMode != 1) BaseUtility.Chat((!Quote4) ? "NYAAAAAAAH! YOU WON’T LIVE THROUGH THIS ONE!" : "COME ON!!! STAND STILL SO I CAN BLOW YOU TO MARS!", new Color(146, 30, 68));
                        QuoteSaid = true;
                        Quote4 = true;
                    }
                    BaseAI.ShootPeriodic(npc, new Vector2(player.position.X, player.position.Y - 1), player.width, player.height, mod.ProjectileType<AbyssalThunder>(), ref internalAI[3], 20, projDamage, 10f, true, new Vector2(20f, 15f));
                }
                if (AttackType == 4f)
                {
                    if (!QuoteSaid)
                    {
                        if (Main.netMode != 1) BaseUtility.Chat((!Quote5) ? "NGAAAAAAAAAAAAAH STAAAAAAAHP MOOOOOOOOVIIIIIIING!!!!!" : "HAVE A HEALTHY TASTE OF ACID!", new Color(146, 30, 68));
                        QuoteSaid = true;
                        Quote5 = true;
                    }
                    BaseAI.ShootPeriodic(npc, new Vector2(player.position.X, player.position.Y - 1), player.width, player.height, mod.ProjectileType<YamataAVenom>(), ref internalAI[3], 6, projDamage, 10f, true, new Vector2(20f, 15f));
                }
                if (AttackType == 5f)
                {
                    if (!QuoteSaid)
                    {
                        if (Main.netMode != 1) BaseUtility.Chat((!Quote6) ? "I'M GONNA RIP YOU TO PIECES YOU LITTLE WRETCH!!!" : "REEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE!!!", new Color(146, 30, 68));
                        QuoteSaid = true;
                        Quote6 = true;
                    }
                    EATTHELITTLEMAGGOT = true;
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (isAwakened)
            {
                if (npc.frameCounter > 5)
                {
                    npc.frameCounter = 0;
                    npc.frame.Y += frameHeight;
                    if (npc.frame.Y > frameHeight * 2)
                    {
                        npc.frame.Y = 0;
                    }
                }
                if (npc.ai[3] == 1 || internalAI[2] > 400)
                {
                    if (npc.frameCounter < 5)
                    {
                        npc.frame.Y = frameHeight * 3;
                    }
                    if (npc.frameCounter > 10)
                    {
                        npc.frame.Y += frameHeight;
                        npc.frameCounter = 5;
                        if (npc.frame.Y > frameHeight * 6)
                        {
                            npc.frame.Y = frameHeight * 4;
                        }
                    }
                }
            }
            else
            {
                if (npc.ai[3] == 1 || npc.ai[2] >= 400)
                {
                    if (npc.frameCounter < 5)
                    {
                        npc.frame.Y = 1 * frameHeight;
                    }
                    else
                    {
                        npc.frame.Y = 2 * frameHeight;
                    }
                }
                else
                {

                    npc.frame.Y = 0 * frameHeight;
                    npc.frameCounter = 0;
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
            else if (projectile.type == ProjectileID.LastPrismLaser)
            {
                damage = (int)(damage * .05f);
            }
        }

        public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
        {
            if (projectile.type == ProjectileID.LastPrismLaser)
            {
                projectile.damage = (int)(projectile.damage * .01f);
            }
        }

        public override void BossHeadRotation(ref float rotation)
        {
            rotation = npc.rotation;
        }
        // We use this hook to prevent any loot from dropping. We do this because this is a multistage npc and it shouldn't drop anything until the final form is dead.
        public override bool PreNPCLoot()
        {
            return false;
        }

        public override bool CheckActive()
        {
            if (NPC.AnyNPCs(mod.NPCType<Yamata>()) || NPC.AnyNPCs(mod.NPCType<YamataA>()))
            {
                return false;
            }
            return true;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            return false;
        }
    }
}
