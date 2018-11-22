using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics;
using Microsoft.Xna.Framework.Graphics;
namespace AAMod.NPCs.Bosses.Yamata.Awakened
{
    public class YamataAHead : Yamata
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Yamata Awakened");
            Main.npcFrameCount[npc.type] = 3;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            if (!AAWorld.downedYamataA)
            {
                npc.lifeMax = 140000;
                if (npc.life > npc.lifeMax / 5)
                {
                    npc.damage = 120;
                    npc.defense = 60;
                }
                if (npc.life <= npc.lifeMax / 5)
                {
                    npc.damage = 100;
                    npc.defense = 70;
                }
            }
            if (AAWorld.downedYamataA)
            {
                npc.lifeMax = 150000;
                if (npc.life > npc.lifeMax / 5)
                {
                    npc.damage = 90;
                    npc.defense = 60;
                }
                if (npc.life <= npc.lifeMax / 5)
                {
                    npc.damage = 110;
                    npc.defense = 80;
                }
            }
            npc.width = 64;
            npc.height = 80;
            npc.npcSlots = 0;
            npc.dontCountMe = true;

        }

        public int varTime = 0;

        public int YvarOld = 0;

        public int XvarOld = 0;
        public int numberOfAttacks = 0;
        public int endAttack = 0;
        public int damage = 0;
        public bool attackFrame = false;
        public float moveSpeedBoost = .04f;
        public NPC Body;
        public bool HoriSwitch = false;
        public int f = 1;
        public float TargetDirection = (float)Math.PI / 2;
        public float s = 1;
        public Projectile Breath;
        private int MouthFrame;
        private int MouthCounter;

        public override void AI()
        {

            Body = Main.npc[(int)npc.ai[0]];

            if (!Body.active)
            {
                npc.life = 0;
            }

            if (Main.expertMode)
            {
                damage = npc.damage / 4;
            }
            else
            {
                damage = npc.damage / 2;
            }
            npc.realLife = (int)npc.ai[0];
            Player player = Main.player[npc.target];
            npc.TargetClosest(true);
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
            if (npc.ai[3] == 2)
            {
                attackFrame = true;
                TargetDirection = (float)Math.PI / 2;
                varTime++;
                npc.ai[2] = 100;
                if (varTime == 30 && Main.netMode !=1)
                {
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 10f, mod.ProjectileType("YamataABreath"), (int)(damage * .8f), 0f, Main.myPlayer);
                }
                if (varTime >= 60)
                {
                    if (Main.netMode != 1)
                    {
                        npc.ai[1] = Main.rand.Next(-300, 300);
                        npc.netUpdate = true;
                    }
                    endAttack++;
                    varTime = 0;

                }
                if (endAttack >= 10)
                {
                    npc.ai[3] = 0;
                }
            }
            else if (npc.ai[3] == 3)
            {
                attackFrame = true;
                varTime++;
                if (varTime < 120)
                {
                    npc.ai[2] = 100;
                    npc.ai[1] = 0;
                    TargetDirection = (float)Math.PI / 2;
                }
                else if (varTime == 180 && Main.netMode !=1)
                {
                    Breath = Main.projectile[Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("YamataABomb"), damage, 1f, Main.myPlayer, npc.whoAmI, 420)];
                }
                else if (varTime < 180)
                {
                    npc.ai[2] = 100;
                    npc.ai[1] = 0;
                    TargetDirection = (float)Math.PI / 2;
                }
                else if (varTime < 240)
                {
                    npc.ai[2] = -300;
                    npc.ai[1] = 0;
                    TargetDirection = (float)Math.PI / 2;
                }
                else if (varTime < 600)
                {
                    npc.ai[2] = -300;
                    npc.ai[1] = 0;
                    s = .5f;
                    TargetDirection = (float)(player.Center - npc.Center).ToRotation();
                }
                else
                {
                    if(Main.netMode !=1) Breath.Kill();
                    s = 1;
                    npc.ai[3] = 0;
                }
            }

            else
            {
                attackFrame = false;
                moveSpeedBoost = .04f;
                varTime++;
                if (varTime > 100)
                {
                    if (Main.netMode != 1)
                    {

                        npc.ai[2] = Main.rand.Next(0, 100);

                        npc.ai[1] = Main.rand.Next(-125, 125);
                        npc.netUpdate = true;
                    }
                    varTime = 0;
                }
                TargetDirection = (float)Math.PI / 2;
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
        }
        public override void FindFrame(int frameHeight)
        {
            if (attackFrame)
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
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            
            return false;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (Main.netMode != 0)
            {
                Body = Main.npc[(int)npc.ai[0]];
                Vector2 neckOrigin = new Vector2(Body.Center.X, Body.Center.Y - 50);
                Vector2 center = npc.Center;
                Vector2 distToProj = neckOrigin - npc.Center;
                float projRotation = distToProj.ToRotation() - 1.57f;
                float distance = distToProj.Length();
                spriteBatch.Draw(mod.GetTexture("NPCs/Bosses/Yamata/Awakened/YamataANeck"), neckOrigin - Main.screenPosition,
                            new Rectangle(0, 0, 26, 40), drawColor, projRotation,
                            new Vector2(26 * 0.5f, 40 * 0.5f), 1f, SpriteEffects.None, 0f);
                while (distance > 30f && !float.IsNaN(distance))
                {
                    distToProj.Normalize();                 //get unit vector
                    distToProj *= 30f;                      //speed = 30
                    center += distToProj;                   //update draw position
                    distToProj = neckOrigin - center;    //update distance
                    distance = distToProj.Length();


                    //Draw chain
                    spriteBatch.Draw(mod.GetTexture("NPCs/Bosses/Yamata/Awakened/YamataANeck"), new Vector2(center.X - Main.screenPosition.X, center.Y - Main.screenPosition.Y),
                        new Rectangle(0, 0, 26, 40), drawColor, projRotation,
                        new Vector2(26 * 0.5f, 40 * 0.5f), 1f, SpriteEffects.None, 0f);

                }
                spriteBatch.Draw(mod.GetTexture("NPCs/Bosses/Yamata/Awakened/YamataANeck"), neckOrigin - Main.screenPosition,
                            new Rectangle(0, 0, 26, 40), drawColor, projRotation,
                            new Vector2(26 * 0.5f, 40 * 0.5f), 1f, SpriteEffects.None, 0f);
                
                spriteBatch.Draw(mod.GetTexture("NPCs/Bosses/Yamata/Awakened/YamataAHead"), new Vector2(npc.Center.X - Main.screenPosition.X, npc.Center.Y - Main.screenPosition.Y),
                            new Rectangle(0, npc.frame.Y, 106, npc.frame.Y + 72), drawColor, npc.rotation,
                            new Vector2(106 * 0.5f, 72 * 0.5f), 1f, SpriteEffects.None, 0f);
                spriteBatch.Draw(mod.GetTexture("NPCs/Bosses/Yamata/Awakened/YamataAHead_Glow"), new Vector2(npc.Center.X - Main.screenPosition.X, npc.Center.Y - Main.screenPosition.Y),
                        new Rectangle(0, npc.frame.Y, 106, npc.frame.Y + 72), Color.White, npc.rotation,
                        new Vector2(106 * 0.5f, 72 * 0.5f), 1f, SpriteEffects.None, 0f);
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
