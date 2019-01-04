using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Enemies.Void
{
    public class Null : ModNPC
	{
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Null");
            Main.npcFrameCount[npc.type] = 4;
        }
		
		public override void SetDefaults()
		{
            npc.CloneDefaults(NPCID.Poltergeist);
            npc.noGravity = true;
            npc.noTileCollide = true;
			npc.aiStyle = -1;
            npc.width = 24;
            npc.height = 40;
            npc.damage = 50;
            npc.defense = 9999999;
            npc.lifeMax = 100;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath6;
            npc.alpha = 70;
            npc.value = 7000f;
            npc.knockBackResist = 0.7f;
            npc.noGravity = true;
        }

		public int frameCount = 0;
		public int frameCounter = 0;
		public override void PostAI()
		{
			frameCounter++;
			if(frameCounter > 5)
			{
				frameCounter = 0;				
				frameCount++;
				if(frameCount > 3) frameCount = 0;
			}
			npc.frame = new Rectangle(0, frameCount * 40, 36, 38);
			npc.spriteDirection = (npc.velocity.X > 0 ? -1 : 1);
			npc.rotation = npc.velocity.X * 0.25f;
		}

        public override void AI()
        {
            bool flag19 = false;
            if (npc.justHit)
            {
                npc.ai[2] = 0f;
            }
            if (npc.ai[2] >= 0f)
            {
                int num282 = 16;
                bool flag21 = false;
                bool flag22 = false;
                if (npc.position.X > npc.ai[0] - (float)num282 && npc.position.X < npc.ai[0] + (float)num282)
                {
                    flag21 = true;
                }
                else if ((npc.velocity.X < 0f && npc.direction > 0) || (npc.velocity.X > 0f && npc.direction < 0))
                {
                    flag21 = true;
                }
                num282 += 24;
                if (npc.position.Y > npc.ai[1] - (float)num282 && npc.position.Y < npc.ai[1] + (float)num282)
                {
                    flag22 = true;
                }
                if (flag21 && flag22)
                {
                    npc.ai[2] += 1f;
                    if (npc.ai[2] >= 30f && num282 == 16)
                    {
                        flag19 = true;
                    }
                    if (npc.ai[2] >= 60f)
                    {
                        npc.ai[2] = -200f;
                        npc.direction *= -1;
                        npc.velocity.X = npc.velocity.X * -1f;
                        npc.collideX = false;
                    }
                }
                else
                {
                    npc.ai[0] = npc.position.X;
                    npc.ai[1] = npc.position.Y;
                    npc.ai[2] = 0f;
                }
                npc.TargetClosest(true);
            }
            
            else
            {
                npc.ai[2] += 1f;
                if (Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) > npc.position.X + (float)(npc.width / 2))
                {
                    npc.direction = -1;
                }
                else
                {
                    npc.direction = 1;
                }
            }
            int num283 = (int)((npc.position.X + (float)(npc.width / 2)) / 16f) + npc.direction * 2;
            int num284 = (int)((npc.position.Y + (float)npc.height) / 16f);
            bool flag23 = false;
            int num285 = 3;
            /*for (int num308 = num284; num308 < num284 + num285; num308++)
            {
                if (Main.tile[num283, num308] == null)
                {
                    Main.tile[num283, num308] = new Tile();
                }
                if ((Main.tile[num283, num308].nactive() && Main.tileSolid[(int)Main.tile[num283, num308].type]) || Main.tile[num283, num308].liquid > 0)
                {
                    flag23 = false;
                    break;
                }
            }
            if (Main.player[npc.target].npcTypeNoAggro[npc.type])
            {
                bool flag25 = false;
                for (int num309 = num284; num309 < num284 + num285 - 2; num309++)
                {
                    if (Main.tile[num283, num309] == null)
                    {
                        Main.tile[num283, num309] = new Tile();
                    }
                    if ((Main.tile[num283, num309].nactive() && Main.tileSolid[(int)Main.tile[num283, num309].type]) || Main.tile[num283, num309].liquid > 0)
                    {
                        flag25 = true;
                        break;
                    }
                }
                npc.directionY = (!flag25).ToDirectionInt();
            }*/
			npc.directionY = (Main.player[npc.target].Center.Y < npc.Center.Y ? -1 : 1);
            
            //if (flag19)
            //{
            //    flag23 = true;
            //}
            if (flag23)
            {
                npc.velocity.Y = npc.velocity.Y + 0.1f;
                if (npc.velocity.Y > 3f)
                {
                    npc.velocity.Y = 3f;
                }
            }
            else
            {
                if (npc.directionY < 0 && npc.velocity.Y > 0f)
                {
                    npc.velocity.Y = npc.velocity.Y - 0.1f;
                }
                if (npc.directionY > 0 && npc.velocity.Y < 0f)
                {
                    npc.velocity.Y = npc.velocity.Y + 0.1f;
                }				
                if (npc.velocity.Y < -4f)
                {
                    npc.velocity.Y = -4f;
                }
            }
            if (npc.collideX)
            {
                npc.velocity.X = npc.oldVelocity.X * -0.4f;
                if (npc.direction == -1 && npc.velocity.X > 0f && npc.velocity.X < 1f)
                {
                    npc.velocity.X = 1f;
                }
                if (npc.direction == 1 && npc.velocity.X < 0f && npc.velocity.X > -1f)
                {
                    npc.velocity.X = -1f;
                }
            }
            if (npc.collideY)
            {
                npc.velocity.Y = npc.oldVelocity.Y * -0.25f;
                if (npc.velocity.Y > 0f && npc.velocity.Y < 1f)
                {
                    npc.velocity.Y = 1f;
                }
                if (npc.velocity.Y < 0f && npc.velocity.Y > -1f)
                {
                    npc.velocity.Y = -1f;
                }
            }
            float num311 = 2f;
            
            if (npc.direction == -1 && npc.velocity.X > -num311)
            {
                npc.velocity.X = npc.velocity.X - 0.1f;
                if (npc.velocity.X > num311)
                {
                    npc.velocity.X = npc.velocity.X - 0.1f;
                }
                else if (npc.velocity.X > 0f)
                {
                    npc.velocity.X = npc.velocity.X + 0.05f;
                }
                if (npc.velocity.X < -num311)
                {
                    npc.velocity.X = -num311;
                }
            }
            else if (npc.direction == 1 && npc.velocity.X < num311)
            {
                npc.velocity.X = npc.velocity.X + 0.1f;
                if (npc.velocity.X < -num311)
                {
                    npc.velocity.X = npc.velocity.X + 0.1f;
                }
                else if (npc.velocity.X < 0f)
                {
                    npc.velocity.X = npc.velocity.X - 0.05f;
                }
                if (npc.velocity.X > num311)
                {
                    npc.velocity.X = num311;
                }
            }
            num311 = 1.5f;
            if (npc.directionY == -1 && npc.velocity.Y > -num311)
            {
                npc.velocity.Y = npc.velocity.Y - 0.04f;
                if (npc.velocity.Y > num311)
                {
                    npc.velocity.Y = npc.velocity.Y - 0.05f;
                }
                else if (npc.velocity.Y > 0f)
                {
                    npc.velocity.Y = npc.velocity.Y + 0.03f;
                }
                if (npc.velocity.Y < -num311)
                {
                    npc.velocity.Y = -num311;
                }
            }
            else if (npc.directionY == 1 && npc.velocity.Y < num311)
            {
                npc.velocity.Y = npc.velocity.Y + 0.04f;
                if (npc.velocity.Y < -num311)
                {
                    npc.velocity.Y = npc.velocity.Y + 0.05f;
                }
                else if (npc.velocity.Y < 0f)
                {
                    npc.velocity.Y = npc.velocity.Y - 0.03f;
                }
                if (npc.velocity.Y > num311)
                {
                    npc.velocity.Y = num311;
                }
            }
        }

        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("UnstableSingularity"), 1);
        }
    }
}