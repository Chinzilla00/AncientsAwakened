using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BaseMod;

namespace AAMod.NPCs.Bosses.Yamata
{
    [AutoloadBossHead]
    public class YamataHead : ModNPC
    {
		public bool isAwakened = false;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Yamata; Dread Nightmare");
            Main.npcFrameCount[npc.type] = 3;
        }

        public override void SetDefaults()
        {
            npc.life = npc.lifeMax;
            if (!Main.expertMode && !AAWorld.downedYamata)
            {
                npc.damage = 130;
                npc.defense = 40;
            }
            if (!Main.expertMode && AAWorld.downedYamata)
            {
                npc.damage = 140;
                npc.defense = 40;
            }
            if (Main.expertMode && !AAWorld.downedYamataA)
            {
                npc.damage = 140;
                npc.defense = 50;
            }
            if (Main.expertMode && AAWorld.downedYamataA)
            {
                npc.damage = 150;
                npc.defense = 60;
            }
            npc.width = 64;
            npc.height = 80;
            npc.npcSlots = 0;
            npc.dontCountMe = true;
            npc.noTileCollide = false;
            npc.noGravity = true;
            npc.boss = true;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
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
        public bool HoriSwitch = false;
        public int f = 1;
        public float TargetDirection = (float)Math.PI / 2;
        public float s = 1;
        public Projectile Breath;
        private int MouthFrame;
        private int MouthCounter;
        private bool fireAttack;
        private int attackFrame;
        private int attackCounter;
        private int attackTimer;
        public int fireTimer = 0;

        public override void AI()
        {
            if (Main.expertMode)
            {
                damage = npc.damage / 4;
            }
            else
            {
                damage = npc.damage / 2;
            }
            Body = Main.npc[(int)npc.ai[0]];
            npc.realLife = (int)npc.ai[0];

            npc.TargetClosest(true);
            Player player = Main.player[npc.target];

            if (!Body.active)
            {
                if (npc.timeLeft > 10)
                {
                    npc.timeLeft = 10;
                }
            }

            int num429 = 1;
            if (npc.position.X + (npc.width / 2) < Main.player[npc.target].position.X + Main.player[npc.target].width)
            {
                num429 = -1;
            }
            Vector2 PlayerDistance = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
            float PlayerPosX = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) + (num429 * 180) - PlayerDistance.X;
            float PlayerPosY = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - PlayerDistance.Y;
            float PlayerPos = (float)Math.Sqrt((PlayerPosX * PlayerPosX) + (PlayerPosY * PlayerPosY));
            float num433 = 6f;
            PlayerDistance = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
            PlayerPosX = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) - PlayerDistance.X;
            PlayerPosY = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - PlayerDistance.Y;
            PlayerPos = (float)Math.Sqrt((PlayerPosX * PlayerPosX + PlayerPosY * PlayerPosY));
            PlayerPos = num433 / PlayerPos;
            PlayerPosX *= PlayerPos;
            PlayerPosY *= PlayerPos;
            PlayerPosY += Main.rand.Next(-40, 41) * 0.01f;
            PlayerPosX += Main.rand.Next(-40, 41) * 0.01f;
            PlayerPosY += npc.velocity.Y * 0.5f;
            PlayerPosX += npc.velocity.X * 0.5f;
            PlayerDistance.X -= PlayerPosX * 1f;
            PlayerDistance.Y -= PlayerPosY * 1f;


            if (fireAttack == true)
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

            if (!player.active || player.dead)
            {
                npc.TargetClosest(false);
                player = Main.player[npc.target];
                if (!player.active || player.dead)
                {
                    npc.velocity = new Vector2(0f, 10f);
                    if (npc.timeLeft > 10)
                    {
                        npc.timeLeft = 10;
                    }
                    return;
                }
            }
            fireTimer++;
            if (fireTimer >= 240 && fireAttack == false)
            {
                fireAttack = true;

                fireTimer = 0;
            }
            if (fireAttack == true)
            {
                attackTimer++;
                if (Main.rand.Next(3) == 0)
                {
                    if (attackTimer == 40)
                    {
                        Main.PlaySound(SoundID.Item34, npc.position);
                        int proj2 = Projectile.NewProjectile(npc.Center.X + Main.rand.Next(-20, 20), npc.Center.Y + Main.rand.Next(-20, 20), npc.velocity.X * 1.6f, npc.velocity.Y * 1.6f, mod.ProjectileType(isAwakened ? "YamataABomb" : "YamataBomb"), 20, 0, Main.myPlayer);
                        Main.projectile[proj2].damage = npc.damage / 3;
                        attackTimer = 0;
                        attackFrame = 0;
                        attackCounter = 0;
                    }
                    if (attackTimer >= 80)
                    {
                        fireAttack = false;
                    }
                }
                else
                {
                    if (attackTimer == 8 || attackTimer == 16 || attackTimer == 24 || attackTimer == 32 || attackTimer == 40 || attackTimer == 48 || attackTimer == 56 || attackTimer == 64 || attackTimer == 72 || attackTimer == 79)
                    {
                        Main.PlaySound(SoundID.Item34, npc.position);
                        for (int i = 0; i < 5; ++i)
                        {
                            if (Main.netMode != 1)
                            {
								Projectile.NewProjectile(PlayerDistance.X, PlayerDistance.Y, PlayerPosX, PlayerPosY, mod.ProjectileType(isAwakened ? "YamataABreath" : "YamataBreath"), (int)(damage * .8f), 0f, Main.myPlayer);
                            }
                        }

                    }
                    if (attackTimer >= 80)
                    {
                        fireAttack = false;
                        attackTimer = 0;
                        attackFrame = 0;
                        attackCounter = 0;
                    }
                }

            }

            if (player != null)
            {
                float dist = npc.Distance(player.Center);
                if (dist > 1000)
                {
                    npc.noTileCollide = true;
                }
                else
                {
                    npc.noTileCollide = false;
                }
            }


            npc.rotation = new Vector2((float)Math.Cos(npc.rotation), (float)Math.Sin(npc.rotation)).ToRotation();
            if (Math.Abs(npc.rotation - TargetDirection) > Math.PI)
            {
                f = -1;
            }
            else
            {
                f = 1;
            }
            if (npc.rotation <= TargetDirection + MathHelper.ToRadians(4 * s) && npc.rotation >= TargetDirection - MathHelper.ToRadians(4 * s))
            {
                npc.rotation = TargetDirection;
            }
            else if (npc.rotation <= TargetDirection)
            {
                npc.rotation += MathHelper.ToRadians(2 * s) * f;
            }
            else if (npc.rotation >= TargetDirection)
            {
                npc.rotation -= MathHelper.ToRadians(2 * s) * f;
            }
            Vector2 moveTo = new Vector2(Body.Center.X + npc.ai[1], Body.Center.Y - (130f + npc.ai[2])) - npc.Center;
            npc.velocity = (moveTo) * moveSpeedBoost;
            npc.spriteDirection = -1;
        }
        public override void FindFrame(int frameHeight)
        {
            if (fireAttack)
            {
                if (npc.frameCounter < 4)
                {
                    npc.frame.Y = 1 * frameHeight;
                }
                if (npc.frameCounter < 8)
                {
                    npc.frame.Y = 2 * frameHeight;
                }
            }
            else
            {
                npc.frame.Y = 0 * frameHeight;
            }
        }

        private bool tenthHealth = false;
        private bool quarterHealth = false;
        private bool threeQuarterHealth = false;
        private bool HalfHealth = false;
        bool Panic = false;

        public override void HitEffect(int hitDirection, double damage)
        {
            if (!isAwakened)
            {
                int dust1 = mod.DustType<Dusts.YamataDust>();
                int dust2 = mod.DustType<Dusts.YamataDust>();
                if (npc.life <= 0)
                {
                    Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust1, 0f, 0f, 0, default(Color), 1f);
                    Main.dust[dust1].velocity *= 0.5f;
                    Main.dust[dust1].scale *= 1.3f;
                    Main.dust[dust1].fadeIn = 1f;
                    Main.dust[dust1].noGravity = false;
                    Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust2, 0f, 0f, 0, default(Color), 1f);
                    Main.dust[dust2].velocity *= 0.5f;
                    Main.dust[dust2].scale *= 1.3f;
                    Main.dust[dust2].fadeIn = 1f;
                    Main.dust[dust2].noGravity = true;

                }
                if (!AAWorld.downedYamata)
                {
                    if (npc.life <= ((npc.lifeMax / 4) * 3) && threeQuarterHealth == false)
                    {
                        BaseUtility.Chat("Resistance isn't gonna save you here! Now stop being a little brat and let me destroy you!", new Color(45, 46, 70));
                        threeQuarterHealth = true;
                    }
                    if (npc.life <= npc.lifeMax / 2 && HalfHealth == false)
                    {
                        BaseUtility.Chat("STOP SQUIRMING AND LET ME SQUASH YOU!!!", new Color(45, 46, 70));
                        HalfHealth = true;
                    }
                    if (npc.life <= npc.lifeMax / 4 && quarterHealth == false)
                    {
                        BaseUtility.Chat("NYAAAAAAAAAAAAAH..! YOU'RE REALLY ANNOYING YOU KNOW..!", new Color(45, 46, 70));
                        quarterHealth = true;
                    }
                    if (npc.life <= npc.lifeMax / 5 && tenthHealth == false)
                    {
                        BaseUtility.Chat("I'VE SQUASHED LIZARDS BIGGER THAN YOU! WHY WON'T YOU JUST FLATTEN?!", new Color(45, 46, 70));
                        tenthHealth = true;
                    }

                }
                if (AAWorld.downedYamata)
                {
                    if (npc.life <= ((npc.lifeMax / 4) * 3) && threeQuarterHealth == false)
                    {
                        Main.NewText("I don't understand why you keep fighting me! I'm superior to you in every single way..!", new Color(45, 46, 70));
                        threeQuarterHealth = true;
                    }
                    if (npc.life <= npc.lifeMax / 2 && HalfHealth == false)
                    {
                        Main.NewText("I'M GETTING FRUSTRATED AGAIN..!", new Color(45, 46, 70));
                        HalfHealth = true;
                    }
                    if (npc.life <= npc.lifeMax / 4 && quarterHealth == false)
                    {
                        Main.NewText("I HATE FIGHTING YOU! I HATE IT I HATE IT I HATE IT!!!", new Color(45, 46, 70));
                        quarterHealth = true;
                    }
                    if (npc.life <= npc.lifeMax / 5 && tenthHealth == false)
                    {
                        Main.NewText("I'M GONNA GRIND YOU INTO TOOTHPASTE YOU LITTLE WRETCH!!!", new Color(45, 46, 70));
                        tenthHealth = true;
                    }
                }
            }
            if (isAwakened)
            {
                int dust1 = mod.DustType<Dusts.YamataADust>();
                int dust2 = mod.DustType<Dusts.YamataADust>();
                if (npc.life <= 0)
                {
                    Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust1, 0f, 0f, 0, default(Color), 1f);
                    Main.dust[dust1].velocity *= 0.5f;
                    Main.dust[dust1].scale *= 1.3f;
                    Main.dust[dust1].fadeIn = 1f;
                    Main.dust[dust1].noGravity = false;
                    Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust2, 0f, 0f, 0, default(Color), 1f);
                    Main.dust[dust2].velocity *= 0.5f;
                    Main.dust[dust2].scale *= 1.3f;
                    Main.dust[dust2].fadeIn = 1f;
                    Main.dust[dust2].noGravity = true;

                }
                if (!AAWorld.downedYamataA)
                {
                    if (npc.life <= ((npc.lifeMax / 4) * 3) && threeQuarterHealth == false)
                    {
                        Main.NewText("YOU'RE STILL FIGHTING?! WHAT THE HELL IS WRONG WITH YOU?!", new Color(146, 30, 68));
                        threeQuarterHealth = true;
                    }
                    if (npc.life <= npc.lifeMax / 2 && HalfHealth == false)
                    {
                        Main.NewText("I'VE HAD IT UP TO HERE WITH YOUR SHENANIGANS!!! EAT VENOM YOU LITTLE HELLSPAWN!!!", new Color(146, 30, 68));
                        HalfHealth = true;
                    }
                    if (npc.life <= npc.lifeMax / 10 && tenthHealth == false)
                    {
                        Main.NewText("STOP IT STOP IT STOP IT!!! I'M NOT GONNA LET YOU WIN!!!", new Color(146, 30, 68));
                        tenthHealth = true;
                    }
                }
                if (AAWorld.downedYamataA)
                {
                    if (npc.life <= ((npc.lifeMax / 4) * 3) && threeQuarterHealth == false)
                    {
                        Main.NewText("You're an annoying little bugger, you know!", new Color(146, 30, 68));
                        threeQuarterHealth = true;
                    }
                    if (npc.life <= npc.lifeMax / 2 && HalfHealth == false)
                    {
                        Main.NewText("DIE! WHY WON'T YOU JUST DIE ALREADY?!", new Color(146, 30, 68));
                        HalfHealth = true;
                    }
                    if (npc.life <= npc.lifeMax / 10 && tenthHealth == false)
                    {
                        Main.NewText("STOP IT!!! I'M NOT GONNA LOSE AGAIN!!!", new Color(146, 30, 68));
                        tenthHealth = true;
                    }
                }
                if (npc.life > npc.lifeMax / 3)
                {
                    Panic = false;
                }
                if (npc.life <= npc.lifeMax / 3 && Panic == false && !AAWorld.downedYamataA && Main.expertMode && npc.type == mod.NPCType<Awakened.YamataAHead>())
                {
                    Panic = true;
                    Main.NewText("Wh-WHA?! DIE! DIE YOU LITTLE TWERP! DIEDIEDIEDIEDIEDIEDIE!!!!", new Color(146, 30, 68));
                }
                if (npc.life <= npc.lifeMax / 3 && Panic == false && AAWorld.downedYamataA && Main.expertMode && npc.type == mod.NPCType<Awakened.YamataAHead>())
                {
                    Panic = true;
                    Main.NewText("NO NO NO!!! NOT AGAIN!!! THIS TIME IMMA STOMP YOU RIGHT INTO THE GROUND!!!", new Color(146, 30, 68));
                }
            }
            
        }

        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            if (Body != null && Body.modNPC is Yamata)
            {
                string headTex = (isAwakened ? "NPCs/Bosses/Yamata/Awakened/YamataAHead" : "NPCs/Bosses/Yamata/YamataHead");
                ((Yamata)Body.modNPC).DrawHead(sb, headTex, headTex + "_Glow", npc, lightColor);
            }
            return true;
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

    }
}
