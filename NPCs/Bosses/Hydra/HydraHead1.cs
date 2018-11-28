using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace AAMod.NPCs.Bosses.Hydra
{
    [AutoloadBossHead]
    public class HydraHead1 : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hydra");
            Main.npcFrameCount[npc.type] = 2;
        }

        public override void SetDefaults()
        {
            npc.life = npc.lifeMax = 100;
            npc.damage = 30;
            npc.defense = 10;
            npc.width = 36;
            npc.height = 32;
            npc.npcSlots = 0;
            npc.dontCountMe = true;
            npc.noTileCollide = false;
            npc.noGravity = true;
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
                if (npc.timeLeft > 10) npc.timeLeft = 10;
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

            if (!Body.active)
            {
                if (npc.timeLeft > 10) npc.timeLeft = 10;
            }

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
                        int proj2 = Projectile.NewProjectile(npc.Center.X + Main.rand.Next(-20, 20), npc.Center.Y + Main.rand.Next(-20, 20), npc.velocity.X * 1.6f, npc.velocity.Y * 1.6f, mod.ProjectileType("AcidProj"), 20, 0, Main.myPlayer);
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
                                Projectile.NewProjectile(PlayerDistance.X, PlayerDistance.Y, PlayerPosX, PlayerPosY, mod.ProjectileType("HydraBreath"), (int)(damage * .8f), 0f, Main.myPlayer);
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
            Vector2 moveTo = new Vector2(Body.Center.X + npc.ai[1], Body.Center.Y - (70f + npc.ai[2])) - npc.Center;
            npc.velocity = (moveTo) * moveSpeedBoost;
            npc.spriteDirection = -1;
        }
        public override void FindFrame(int frameHeight)
        {
            if (fireAttack)
            {
                MouthCounter++;
                if (MouthCounter > 10)
                {
                    MouthFrame++;
                    MouthCounter = 0;
                }
                if (MouthFrame >= 3)
                {
                    MouthFrame = 2;
                }
            }
            else
            {
                npc.frame.Y = 0 * frameHeight;
            }
        }

        public override void PostDraw(SpriteBatch sb, Color lightColor)
        {
            if (Body != null && Body.modNPC is Hydra)
            {
                string headTex = ("NPCs/Bosses/Hydra/HydraHead1");
                ((Hydra)Body.modNPC).DrawHead(sb, headTex, headTex + "_Glow", npc, lightColor);
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

    }
}
