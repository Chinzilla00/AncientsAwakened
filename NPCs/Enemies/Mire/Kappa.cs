using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Enemies.Mire
{
    // Party Zombie is a pretty basic clone of a vanilla NPC. To learn how to further adapt vanilla NPC behaviors, see https://github.com/blushiemagic/tModLoader/wiki/Advanced-Vanilla-Code-Adaption#example-npc-npc-clone-with-modified-projectile-hoplite
    public class Kappa : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Kappa");
			Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.CreatureFromTheDeep];
		}

		public override void SetDefaults()
		{
			npc.width = 18;
			npc.height = 40;
			npc.damage = 90;
			npc.defense = 16;
			npc.lifeMax = 300;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath2;
			npc.value = 450f;
			npc.aiStyle = 0;
			animationType = NPCID.CreatureFromTheDeep;
		}

        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("HydraToxin"));
        }

        public override void AI()
        {
            if (npc.wet)
            {
                npc.knockBackResist = 0f;
                npc.ai[3] = -0.10101f;
                npc.noGravity = true;
                Vector2 center = npc.Center;
                npc.width = 34;
                npc.height = 24;
                npc.position.X = center.X - (float)(npc.width / 2);
                npc.position.Y = center.Y - (float)(npc.height / 2);
                npc.TargetClosest(true);
                if (npc.collideX)
                {
                    npc.velocity.X = -npc.oldVelocity.X;
                }
                if (npc.velocity.X < 0f)
                {
                    npc.direction = -1;
                }
                if (npc.velocity.X > 0f)
                {
                    npc.direction = 1;
                }
                if (Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].Center, 1, 1))
                {
                    Vector2 value = Main.player[npc.target].Center - npc.Center;
                    value.Normalize();
                    value *= 5f;
                    npc.velocity = (npc.velocity * 19f + value) / 20f;
                    return;
                }
                float num2 = 5f;
                if (npc.velocity.Y > 0f)
                {
                    num2 = 3f;
                }
                if (npc.velocity.Y < 0f)
                {
                    num2 = 8f;
                }
                Vector2 value2 = new Vector2((float)npc.direction, -1f);
                value2.Normalize();
                value2 *= num2;
                if (num2 < 5f)
                {
                    npc.velocity = (npc.velocity * 24f + value2) / 25f;
                        return;
                }
                npc.velocity = (npc.velocity * 9f + value2) / 10f;
                return;
            }
            else
            {
                npc.knockBackResist = 0.4f * Main.knockBackMultiplier;
                npc.noGravity = false;
                Vector2 center2 = npc.Center;
                npc.width = 18;
                npc.height = 40;
                npc.position.X = center2.X - (float)(npc.width / 2);
                npc.position.Y = center2.Y - (float)(npc.height / 2);
                if (npc.ai[3] == -0.10101f)
                {
                    npc.ai[3] = 0f;
                    float num3 = npc.velocity.Length();
                    num3 *= 2f;
                    if (num3 > 10f)
                    {
                        num3 = 10f;
                    }
                    npc.velocity.Normalize();
                    npc.velocity *= num3;
                    if (npc.velocity.X < 0f)
                    {
                        npc.direction = -1;
                    }
                    if (npc.velocity.X > 0f)
                    {
                            npc.direction = 1;
                    }
                    npc.spriteDirection = npc.direction;
                }
            }
            
            bool flag4 = false;
            if (npc.velocity.X == 0f)
            {
                flag4 = true;
            }
            if (npc.justHit)
            {
                flag4 = false;
            }
            int num36 = 60;
            
            bool flag5 = false;
            bool flag6 = true;
            bool flag7 = false;
            int num42 = npc.type;
            bool flag8 = true;
            num42 = npc.type;
            if (num42 != 466)
            {
                switch (num42)
                {
                    default:
                        if (num42 != 520)
                        {
                            goto IL_2E98;
                        }
                        break;
                }
            }
            if (npc.ai[2] > 0f)
            {
                flag8 = false;
            }
            IL_2E98:
            if (!flag7 && flag8)
            {
                if (npc.velocity.Y == 0f && ((npc.velocity.X > 0f && npc.direction < 0) || (npc.velocity.X < 0f && npc.direction > 0)))
                {
                    flag5 = true;
                }
                if (npc.position.X == npc.oldPosition.X || npc.ai[3] >= (float)num36 || flag5)
                {
                    npc.ai[3] += 1f;
                }
                else if ((double)Math.Abs(npc.velocity.X) > 0.9 && npc.ai[3] > 0f)
                {
                    npc.ai[3] -= 1f;
                }
                if (npc.ai[3] > (float)(num36 * 10))
                {
                    npc.ai[3] = 0f;
                }
                if (npc.justHit)
                {
                    npc.ai[3] = 0f;
                }
                if (npc.ai[3] == (float)num36)
                {
                    npc.netUpdate = true;
                }
            }
            if (npc.ai[3] < (float)num36 && (!Main.dayTime || (double)npc.position.Y > Main.worldSurface * 16.0))
            {
                
                npc.TargetClosest(true);
            }
            else if (npc.ai[2] <= 0f)
            {
                if (Main.dayTime && (double)(npc.position.Y / 16f) < Main.worldSurface && npc.timeLeft > 10)
                {
                    npc.timeLeft = 10;
                }
                if (npc.velocity.X == 0f)
                {
                    if (npc.velocity.Y == 0f)
                    {
                        npc.ai[0] += 1f;
                        if (npc.ai[0] >= 2f)
                        {
                            npc.direction *= -1;
                            npc.spriteDirection = npc.direction;
                            npc.ai[0] = 0f;
                        }
                    }
                }
                else
                {
                    npc.ai[0] = 0f;
                }
                if (npc.direction == 0)
                {
                    npc.direction = 1;
                }
            }
            if (npc.velocity.X < -2f || npc.velocity.X > 2f)
            {
                if (npc.velocity.Y == 0f)
                {
                    npc.velocity *= 0.8f;
                }
            }
                else if (npc.velocity.X < 2f && npc.direction == 1)
                {
                    npc.velocity.X = npc.velocity.X + 0.07f;
                    if (npc.velocity.X > 2f)
                    {
                        npc.velocity.X = 2f;
                    }
                }
                else if (npc.velocity.X > -2f && npc.direction == -1)
                {
                    npc.velocity.X = npc.velocity.X - 0.07f;
                    if (npc.velocity.X < -2f)
                    {
                        npc.velocity.X = -2f;
                    }
                }
            
            float num79 = 1f;
                if (npc.type == 186)
                {
                    num79 = 1.1f;
                }
                if (npc.type == 187)
                {
                    num79 = 0.9f;
                }
                if (npc.type == 188)
                {
                    num79 = 1.2f;
                }
                if (npc.type == 189)
                {
                    num79 = 0.8f;
                }
                if (npc.type == 132)
                {
                    num79 = 0.95f;
                }
                if (npc.type == 200)
                {
                    num79 = 0.87f;
                }
                if (npc.type == 223)
                {
                    num79 = 1.05f;
                }
                if (npc.type == 489)
                {
                    float num80 = (Main.player[npc.target].Center - npc.Center).Length();
                    num80 *= 0.0025f;
                    if ((double)num80 > 1.5)
                    {
                        num80 = 1.5f;
                    }
                    if (Main.expertMode)
                    {
                        num79 = 3f - num80;
                    }
                    else
                    {
                        num79 = 2.5f - num80;
                    }
                    num79 *= 0.8f;
                }
                if (npc.type == 489 || npc.type == 3 || npc.type == 132 || npc.type == 186 || npc.type == 187 || npc.type == 188 || npc.type == 189 || npc.type == 200 || npc.type == 223 || npc.type == 331 || npc.type == 332)
                {
                    num79 *= 1f + (1f - npc.scale);
                }
                if (npc.velocity.X < -num79 || npc.velocity.X > num79)
                {
                    if (npc.velocity.Y == 0f)
                    {
                        npc.velocity *= 0.8f;
                    }
                }
                else if (npc.velocity.X < num79 && npc.direction == 1)
                {
                    npc.velocity.X = npc.velocity.X + 0.07f;
                    if (npc.velocity.X > num79)
                    {
                        npc.velocity.X = num79;
                    }
                }
                else if (npc.velocity.X > -num79 && npc.direction == -1)
                {
                    npc.velocity.X = npc.velocity.X - 0.07f;
                    if (npc.velocity.X < -num79)
                    {
                        npc.velocity.X = -num79;
                    }
                }
            
            bool flag23 = false;
            if (npc.velocity.Y == 0f)
            {
                int num167 = (int)(npc.position.Y + (float)npc.height + 7f) / 16;
                int num168 = (int)npc.position.X / 16;
                int num169 = (int)(npc.position.X + (float)npc.width) / 16;
                for (int num170 = num168; num170 <= num169; num170++)
                {
                    if (Main.tile[num170, num167] == null)
                    {
                        return;
                    }
                    if (Main.tile[num170, num167].nactive() && Main.tileSolid[(int)Main.tile[num170, num167].type])
                    {
                        flag23 = true;
                        break;
                    }
                }
            }
            
            if (npc.velocity.Y >= 0f)
            {
                int num171 = 0;
                if (npc.velocity.X < 0f)
                {
                    num171 = -1;
                }
                if (npc.velocity.X > 0f)
                {
                    num171 = 1;
                }
                Vector2 position2 = npc.position;
                position2.X += npc.velocity.X;
                int num172 = (int)((position2.X + (float)(npc.width / 2) + (float)((npc.width / 2 + 1) * num171)) / 16f);
                int num173 = (int)((position2.Y + (float)npc.height - 1f) / 16f);
                if (Main.tile[num172, num173] == null)
                {
                    Main.tile[num172, num173] = new Tile();
                }
                if (Main.tile[num172, num173 - 1] == null)
                {
                    Main.tile[num172, num173 - 1] = new Tile();
                }
                if (Main.tile[num172, num173 - 2] == null)
                {
                    Main.tile[num172, num173 - 2] = new Tile();
                }
                if (Main.tile[num172, num173 - 3] == null)
                {
                    Main.tile[num172, num173 - 3] = new Tile();
                }
                if (Main.tile[num172, num173 + 1] == null)
                {
                    Main.tile[num172, num173 + 1] = new Tile();
                }
                if (Main.tile[num172 - num171, num173 - 3] == null)
                {
                    Main.tile[num172 - num171, num173 - 3] = new Tile();
                }
                if ((float)(num172 * 16) < position2.X + (float)npc.width && (float)(num172 * 16 + 16) > position2.X && ((Main.tile[num172, num173].nactive() && !Main.tile[num172, num173].topSlope() && !Main.tile[num172, num173 - 1].topSlope() && Main.tileSolid[(int)Main.tile[num172, num173].type] && !Main.tileSolidTop[(int)Main.tile[num172, num173].type]) || (Main.tile[num172, num173 - 1].halfBrick() && Main.tile[num172, num173 - 1].nactive())) && (!Main.tile[num172, num173 - 1].nactive() || !Main.tileSolid[(int)Main.tile[num172, num173 - 1].type] || Main.tileSolidTop[(int)Main.tile[num172, num173 - 1].type] || (Main.tile[num172, num173 - 1].halfBrick() && (!Main.tile[num172, num173 - 4].nactive() || !Main.tileSolid[(int)Main.tile[num172, num173 - 4].type] || Main.tileSolidTop[(int)Main.tile[num172, num173 - 4].type]))) && (!Main.tile[num172, num173 - 2].nactive() || !Main.tileSolid[(int)Main.tile[num172, num173 - 2].type] || Main.tileSolidTop[(int)Main.tile[num172, num173 - 2].type]) && (!Main.tile[num172, num173 - 3].nactive() || !Main.tileSolid[(int)Main.tile[num172, num173 - 3].type] || Main.tileSolidTop[(int)Main.tile[num172, num173 - 3].type]) && (!Main.tile[num172 - num171, num173 - 3].nactive() || !Main.tileSolid[(int)Main.tile[num172 - num171, num173 - 3].type]))
                {
                    float num174 = (float)(num173 * 16);
                    if (Main.tile[num172, num173].halfBrick())
                    {
                        num174 += 8f;
                    }
                    if (Main.tile[num172, num173 - 1].halfBrick())
                    {
                        num174 -= 8f;
                    }
                    if (num174 < position2.Y + (float)npc.height)
                    {
                        float num175 = position2.Y + (float)npc.height - num174;
                        float num176 = 16.1f;
                        if (npc.type == 163 || npc.type == 164 || npc.type == 236 || npc.type == 239 || npc.type == 530)
                        {
                            num176 += 8f;
                        }
                        if (num175 <= num176)
                        {
                            npc.gfxOffY += npc.position.Y + (float)npc.height - num174;
                            npc.position.Y = num174 - (float)npc.height;
                            if (num175 < 9f)
                            {
                                npc.stepSpeed = 1f;
                            }
                            else
                            {
                                npc.stepSpeed = 2f;
                            }
                        }
                    }
                }
            }
            if (flag23)
            {
                int num177 = (int)((npc.position.X + (float)(npc.width / 2) + (float)(15 * npc.direction)) / 16f);
                int num178 = (int)((npc.position.Y + (float)npc.height - 15f) / 16f);
                
                if (Main.tile[num177, num178] == null)
                {
                    Main.tile[num177, num178] = new Tile();
                }
                if (Main.tile[num177, num178 - 1] == null)
                {
                    Main.tile[num177, num178 - 1] = new Tile();
                }
                if (Main.tile[num177, num178 - 2] == null)
                {
                    Main.tile[num177, num178 - 2] = new Tile();
                }
                if (Main.tile[num177, num178 - 3] == null)
                {
                    Main.tile[num177, num178 - 3] = new Tile();
                }
                if (Main.tile[num177, num178 + 1] == null)
                {
                    Main.tile[num177, num178 + 1] = new Tile();
                }
                if (Main.tile[num177 + npc.direction, num178 - 1] == null)
                {
                    Main.tile[num177 + npc.direction, num178 - 1] = new Tile();
                }
                if (Main.tile[num177 + npc.direction, num178 + 1] == null)
                {
                    Main.tile[num177 + npc.direction, num178 + 1] = new Tile();
                }
                if (Main.tile[num177 - npc.direction, num178 + 1] == null)
                {
                    Main.tile[num177 - npc.direction, num178 + 1] = new Tile();
                }
                Main.tile[num177, num178 + 1].halfBrick();
                if (Main.tile[num177, num178 - 1].nactive() && (Main.tile[num177, num178 - 1].type == 10 || Main.tile[num177, num178 - 1].type == 388) && flag6)
                {
                    npc.ai[2] += 1f;
                    npc.ai[3] = 0f;
                    if (npc.ai[2] >= 60f)
                    {
                       
                        npc.velocity.X = 0.5f * (float)(-(float)npc.direction);
                        int num179 = 5;
                        if (Main.tile[num177, num178 - 1].type == 388)
                        {
                            num179 = 2;
                        }
                        npc.ai[1] += (float)num179;
                        
                        npc.ai[2] = 0f;
                        if (npc.ai[1] >= 10f)
                        {
                            npc.ai[1] = 10f;
                        }
                        WorldGen.KillTile(num177, num178 - 1, true, false, false);
                    }
                }
                else
                {
                    int num180 = npc.spriteDirection;
                    if ((npc.velocity.X < 0f && num180 == -1) || (npc.velocity.X > 0f && num180 == 1))
                    {
                        if (npc.height >= 32 && Main.tile[num177, num178 - 2].nactive() && Main.tileSolid[(int)Main.tile[num177, num178 - 2].type])
                        {
                            if (Main.tile[num177, num178 - 3].nactive() && Main.tileSolid[(int)Main.tile[num177, num178 - 3].type])
                            {
                                npc.velocity.Y = -8f;
                                npc.netUpdate = true;
                            }
                            else
                            {
                                npc.velocity.Y = -7f;
                                npc.netUpdate = true;
                            }
                        }
                        else if (Main.tile[num177, num178 - 1].nactive() && Main.tileSolid[(int)Main.tile[num177, num178 - 1].type])
                        {
                            npc.velocity.Y = -6f;
                            npc.netUpdate = true;
                        }
                        else if (npc.position.Y + (float)npc.height - (float)(num178 * 16) > 20f && Main.tile[num177, num178].nactive() && !Main.tile[num177, num178].topSlope() && Main.tileSolid[(int)Main.tile[num177, num178].type])
                        {
                            npc.velocity.Y = -5f;
                            npc.netUpdate = true;
                        }
                        else if (npc.directionY < 0 && npc.type != 67 && (!Main.tile[num177, num178 + 1].nactive() || !Main.tileSolid[(int)Main.tile[num177, num178 + 1].type]) && (!Main.tile[num177 + npc.direction, num178 + 1].nactive() || !Main.tileSolid[(int)Main.tile[num177 + npc.direction, num178 + 1].type]))
                        {
                            npc.velocity.Y = -8f;
                            npc.velocity.X = npc.velocity.X * 1.5f;
                            npc.netUpdate = true;
                        }
                        else if (flag6)
                        {
                            npc.ai[1] = 0f;
                            npc.ai[2] = 0f;
                        }
                        if (npc.velocity.Y == 0f && flag4 && npc.ai[3] == 1f)
                        {
                            npc.velocity.Y = -5f;
                        }
                    }
                    
                }
            }
            else if (flag6)
            {
                npc.ai[1] = 0f;
                npc.ai[2] = 0f;
            }
        }
    }
}
