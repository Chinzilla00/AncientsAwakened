using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;

namespace AAMod.NPCs.Bosses.Hydra
{

    [AutoloadBossHead]
    public class HydraHead3 : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hydra");
			Main.npcFrameCount[npc.type] = 2;
		}

		public override void SetDefaults()
		{
            base.SetDefaults();
            npc.width = 36;
            npc.height = 32;
            npc.damage = 35;
            npc.defense = 8;
            npc.boss = false;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.aiStyle = -1;
            animationType = -1;
            npc.noGravity = true;
            npc.dontTakeDamage = false;
            npc.noTileCollide = true;
            music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/Hydra");
			
			
		}

		public int attackDelay =600;
		public int attackCooldown =0;
		
		public int varTime=0;
		
		public int YvarOld =0;
		
		public int XvarOld =0;
		public int numberOfAttacks =0;
		public int endAttack =0;
		public int damage = 0;
		public bool attackFrame = false;
		public float moveSpeedBoost = .04f;
		public NPC Hydra;
		public bool HoriSwitch = false;
		public int f=1;
		public float TargetDirection = (float)Math.PI/2;
		public float s =1;
        public Projectile Bomb;

        public override void AI()
        {
            Hydra = Main.npc[(int)npc.ai[0]];
            npc.realLife = (int)npc.ai[0];

            if (Main.expertMode)
			{
				damage = npc.damage/4;
				attackDelay = 180;
			}
			else
			{
				damage = npc.damage/2;
			}
            
            if (!Hydra.active)
            {
                npc.life = 0;
            }

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
				TargetDirection = 0;
				varTime++;
				
				npc.ai[1] = 280;
				if(varTime == 30 && Main.netMode !=1)
				{
					Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 10f, 0f, mod.ProjectileType("HydraBreath"), (int)(damage*.8f), 3f, Main.myPlayer);
				}		
				if(varTime >= 60)
				{
                    if (Main.netMode != 1)
                    {
                        npc.ai[2] = Main.rand.Next(100, 500);
                        npc.netUpdate = true;
                    }
					endAttack++;
					varTime=0;
				
				}
				if(endAttack >= 10)
				{
					npc.ai[3] =0;
				}
			}
			else if(npc.ai[3] == 3)
			{
				
				attackFrame = true;
				TargetDirection = (float)Math.PI/2;
				
				
				
				varTime++;
				
				
				npc.ai[2] = 0;
				
				if(varTime >= 600)
				{
					npc.ai[3] =0;
                    if(Main.netMode !=1) Bomb.Kill();
				}
				else if( varTime >=300)
				{
					npc.ai[1]=100;
					
				}
				else if ( varTime >120)
				{
					npc.ai[1] -=  400/180;
					
				}
                else if (varTime == 120 && Main.netMode != 1)
                {
                    Bomb = Main.projectile[Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("HydraBeamT"), damage, 3f, Main.myPlayer, npc.whoAmI, 420)];
                }
                else
				{
					npc.ai[1] =500;
				}
				
				
			
			}
			else
			{
				attackFrame = false;
				moveSpeedBoost = .04f;
				varTime++;
				if(varTime > 100)
				{
                    if (Main.netMode != 1)
                    {
                        npc.ai[2] = Main.rand.Next(-50, 50);

                        npc.ai[1] = Main.rand.Next(0, 250);
                        npc.netUpdate = true;
                    }
					varTime=0;
				}
				attackCooldown++;
				TargetDirection = (float)Math.PI/2;
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

            Vector2 moveTo = new Vector2(Hydra.Center.X-(100+npc.ai[1]), Hydra.Center.Y-(90f - npc.ai[2]))- npc.Center;	
			npc.velocity = (moveTo)*moveSpeedBoost;			
			
		}
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {

            return false;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {

            if (Main.netMode != 0)
            {
                Hydra = Main.npc[(int)npc.ai[0]];
                Vector2 neckOrigin = new Vector2(Hydra.Center.X, Hydra.Center.Y - 50);
                Vector2 center = npc.Center;
                Vector2 distToProj = neckOrigin - npc.Center;
                float projRotation = distToProj.ToRotation() - 1.57f;
                float distance = distToProj.Length();
                spriteBatch.Draw(mod.GetTexture("NPCs/Boss/Hydra/HydraNeck"), neckOrigin - Main.screenPosition,
                            new Rectangle(0, 0, 14, 22), drawColor, projRotation,
                            new Vector2(14 * 0.5f, 22 * 0.5f), 1f, SpriteEffects.None, 0f);
                while (distance > 30f && !float.IsNaN(distance))
                {
                    distToProj.Normalize();                 //get unit vector
                    distToProj *= 30f;                      //speed = 30
                    center += distToProj;                   //update draw position
                    distToProj = neckOrigin - center;    //update distance
                    distance = distToProj.Length();


                    //Draw chain
                    spriteBatch.Draw(mod.GetTexture("NPCs/Boss/Hydra/HydraNeck"), new Vector2(center.X - Main.screenPosition.X, center.Y - Main.screenPosition.Y),
                        new Rectangle(0, 0, 14, 22), drawColor, projRotation,
                        new Vector2(14 * 0.5f, 22 * 0.5f), 1f, SpriteEffects.None, 0f);

                }
                spriteBatch.Draw(mod.GetTexture("NPCs/Boss/Hydra/HydraNeck"), neckOrigin - Main.screenPosition,
                            new Rectangle(0, 0, 14, 22), drawColor, projRotation,
                            new Vector2(14 * 0.5f, 22 * 0.5f), 1f, SpriteEffects.None, 0f);

                spriteBatch.Draw(mod.GetTexture("NPCs/Boss/Hydra/HydraHead3"), new Vector2(npc.Center.X - Main.screenPosition.X, npc.Center.Y - Main.screenPosition.Y),
                            new Rectangle(0, npc.frame.Y, 36, npc.frame.Y + 32), drawColor, npc.rotation,
                            new Vector2(36 * 0.5f, 32 * 0.5f), 1f, SpriteEffects.None, 0f);
                spriteBatch.Draw(mod.GetTexture("NPCs/Boss/Hydra/HydraHead3_Glow"), new Vector2(npc.Center.X - Main.screenPosition.X, npc.Center.Y - Main.screenPosition.Y),
                        new Rectangle(0, npc.frame.Y, 36, npc.frame.Y + 32), Color.White, npc.rotation,
                        new Vector2(36 * 0.5f, 32 * 0.5f), 1f, SpriteEffects.None, 0f);
            }
        }
        public override void FindFrame(int frameHeight)
		{
			
			
				if (attackFrame)
				{
					npc.frame.Y = 1 * frameHeight;
				}
				else
				{
					npc.frame.Y = 0 * frameHeight;
				}
				
		}
		public override void BossHeadRotation(ref float rotation)
		{
			
			rotation =npc.rotation;	
			
		}
		// We use this hook to prevent any loot from dropping. We do this because this is a multistage npc and it shouldn't drop anything until the final form is dead.
		public override bool PreNPCLoot()
		{
			return false;
		}
		
	}
}
