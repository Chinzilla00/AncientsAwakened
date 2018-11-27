using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace AAMod.NPCs.Bosses.Yamata
{
    public class YamataHeadF1 : ModNPC
    {

        public int PoisonTimer = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Yamata");
            Main.npcFrameCount[npc.type] = 3;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            if (!Main.expertMode && !AAWorld.downedYamata)
            {
                npc.lifeMax = 20000;
            }
            if (!Main.expertMode && AAWorld.downedYamata)
            {
                npc.lifeMax = 30000;
            }
            if (Main.expertMode && !AAWorld.downedYamataA)
            {
                npc.lifeMax = 25000;
            }
            if (Main.expertMode && AAWorld.downedYamataA)
            {
                npc.lifeMax = 35000;
            }
            npc.width = 64;
            npc.height = 48;
            npc.npcSlots = 0;
            npc.dontCountMe = true;
            npc.noTileCollide = false;
            npc.boss = false;
            npc.noGravity = true;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }

        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return 0f;
        }
        public int attackDelay = 600;
        public int attackCooldown = 0;
        
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
        public Projectile laser;
        private int MouthFrame;
        private int MouthCounter;
        private bool killedbyplayer = true;

        public override void AI()
        {


            Body = Main.npc[(int)npc.ai[0]];
            //npc.realLife = (int)npc.ai[0];
            if (!Body.active)
            {
                killedbyplayer = false;
                npc.life = 0;
            }

            if (Main.expertMode)
            {
                damage = npc.damage / 4;
                attackDelay = 180;
            }
            else
            {
                damage = npc.damage / 2;
            }






            Player player = Main.player[npc.target];
            npc.TargetClosest(true);


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


            if (attackCooldown > attackDelay)
            {
                endAttack = 0;
                if (Main.netMode != 1)
                {
                    npc.ai[3] = Main.rand.Next(2, 4);
                    npc.netUpdate = true;
                }
                attackCooldown = 0;

            }

            if (npc.ai[3] == 2)
            {
                attackFrame = true;
                TargetDirection = (float)Math.PI;
                varTime++;
                
                npc.ai[1] = 280;
                if (varTime == 30)
                {
                    for (int i = 0; i < 5; ++i)
                    {
                        if (Main.netMode != 1)
                        {
                            Projectile.NewProjectile(PlayerDistance.X, PlayerDistance.Y, PlayerPosX, PlayerPosY, mod.ProjectileType("YamataBreath"), (int)(damage * .8f), 0f, Main.myPlayer);
                        }
                    }
                }
                if (varTime >= 60)
                {
                   
                    if (Main.netMode != 1)
                    {
                        npc.ai[2] = Main.rand.Next(100, 130);
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
                TargetDirection = (float)Math.PI / 2;



                varTime++;
                
               
                npc.ai[2] = 0;

                if (varTime >= 600)
                {
                    npc.ai[3] = 0;
                    if(Main.netMode !=1) laser.Kill();
                }
                else if (varTime >= 300)
                {
                    npc.ai[1] = 100;
                    
                }
                else if (varTime > 120)
                {
                    npc.ai[1] -= 400 / 180;
                    
                }
                else if (varTime == 120 && Main.netMode !=1)
                {
                    laser = Main.projectile[Projectile.NewProjectile(npc.Center.X, npc.Center.Y, PlayerPosX, PlayerPosY, mod.ProjectileType("YamataBomb"), damage, 1f, Main.myPlayer, npc.whoAmI, 420)];
                }
                else
                {
                    npc.ai[1] = 500;
                }



            }
            else
            {
                attackFrame = false;
                moveSpeedBoost = .04f;
                varTime++;
                if (varTime > 100)
                {
                    

                    varTime = 0;
                    if (Main.netMode != 1)
                    {
                        npc.ai[2] = Main.rand.Next(0, 50);
                        npc.ai[1] = Main.rand.Next(0, 250);
                        npc.netUpdate = true;
                    }
                }
                attackCooldown++;
                TargetDirection = (float)Math.PI/2;
            }

			npc.spriteDirection = 1;
			npc.rotation = 1.57f;


            Vector2 moveTo = new Vector2(Body.Center.X + 100 + npc.ai[1], Body.Center.Y - (130f - npc.ai[2])) - npc.Center;
                npc.velocity = (moveTo) * moveSpeedBoost;

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
                spriteBatch.Draw(mod.GetTexture("NPCs/Bosses/Yamata/YamataNeck"), neckOrigin - Main.screenPosition,
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
                    spriteBatch.Draw(mod.GetTexture("NPCs/Bosses/Yamata/YamataNeck"), new Vector2(center.X - Main.screenPosition.X, center.Y - Main.screenPosition.Y),
                        new Rectangle(0, 0, 26, 40), drawColor, projRotation,
                        new Vector2(26 * 0.5f, 40 * 0.5f), 1f, SpriteEffects.None, 0f);

                }
                spriteBatch.Draw(mod.GetTexture("NPCs/Bosses/Yamata/YamataNeck"), neckOrigin - Main.screenPosition,
                            new Rectangle(0, 0, 26, 40), drawColor, projRotation,
                            new Vector2(26 * 0.5f, 40 * 0.5f), 1f, SpriteEffects.None, 0f);
                
                spriteBatch.Draw(mod.GetTexture("NPCs/Bosses/Yamata/YamataHeadF1"), new Vector2(npc.Center.X - Main.screenPosition.X, npc.Center.Y - Main.screenPosition.Y),
                            new Rectangle(0, npc.frame.Y, 64, npc.frame.Y + 48), drawColor, npc.rotation,
                            new Vector2(64 * 0.5f, 48 * 0.5f), 1f, SpriteEffects.None, 0f);
                spriteBatch.Draw(mod.GetTexture("NPCs/Bosses/Yamata/YamataHeadF1_Glow"), new Vector2(npc.Center.X - Main.screenPosition.X, npc.Center.Y - Main.screenPosition.Y),
                        new Rectangle(0, npc.frame.Y, 64, npc.frame.Y + 48), Color.White, npc.rotation,
                        new Vector2(64 * 0.5f, 48 * 0.5f), 1f, SpriteEffects.None, 0f);
            }
        }

        public override void NPCLoot()
        {
            Main.NewText("OWIE!!!", new Color(45, 46, 70));
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
