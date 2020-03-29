using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using BaseMod;
using System;

namespace AAMod.NPCs.Enemies.Inferno
{
    public abstract class Flamespitter : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Flamespitter");
            Main.npcFrameCount[npc.type] = 15;
		}

		public override void SetDefaults()
        {
            npc.width = 40;
            npc.height = 52;
            npc.damage = 20;
			npc.defense = 15;
			npc.lifeMax = 100;
			npc.HitSound = SoundID.NPCHit4;
			npc.DeathSound = SoundID.NPCDeath6;
            npc.value = 240000f;
            npc.knockBackResist = .30f;
            npc.aiStyle = -1;
            npc.noTileCollide = true;
            npc.noGravity = true;
            npc.lavaImmune = true;
            npc.buffImmune[BuffID.OnFire] = true;
            banner = npc.type;
			bannerItem = mod.ItemType("FlamespitterBanner");
        }

        public bool teleport = false;
        public bool digUp = false;


        public override void AI()
        {
            npc.TargetClosest(true);

            BaseAI.LookAt(Main.player[npc.target].Center, npc, 1, 0f, 0f, true);
            int distFromPlayer = 20;
            bool checkGround = true;
            int teleportInterval = 650;
            int attackInterval = 100;
            int stopAttackInterval = 500;
            int frameHeight = 52;
            
            Func<int, int, bool> CanTeleportTo = null;
            npc.velocity.X = npc.velocity.X * 0.93f;

            if (npc.velocity.X > -0.1 && npc.velocity.X < 0.1)
            {
                npc.velocity.X = 0f;
            }

            if (npc.ai[0] == 0f)
            {
                npc.ai[0] = Math.Max(0, Math.Max(teleportInterval, teleportInterval - 150));
            }

            if (npc.ai[2] != 0f && npc.ai[3] != 0f)
            {
                npc.position.X = npc.ai[2] * 16f - npc.width / 2 + 8f;
                npc.position.Y = npc.ai[3] * 16f - npc.height;
                npc.velocity.X = 0f; npc.velocity.Y = 0f;
                npc.ai[2] = 0f; npc.ai[3] = 0f;
            }

            if (npc.justHit)
            {
                npc.ai[0] = 0;
            }

            npc.ai[0]++;
            if (attackInterval != -1 && npc.ai[0] < stopAttackInterval && npc.ai[0] % attackInterval == 0)
            {
                npc.ai[1] = 30f;
                npc.netUpdate = true;
            }
            else if (npc.ai[0] >= teleportInterval && Main.netMode != 1)
            {
                npc.ai[0] = 1f;
                if (teleport == true)
                {
                    int playerTileX = (int)Main.player[npc.target].position.X / 16;
                    int playerTileY = (int)Main.player[npc.target].position.Y / 16;
                    int tileX = (int)npc.position.X / 16;
                    int tileY = (int)npc.position.Y / 16;
                    int teleportCheckCount = 0;
                    bool hasTeleportPoint = false;
                    //player is too far away, don't teleport.
                    if (Vector2.Distance(npc.Center, Main.player[npc.target].Center) > 2000f)
                    {
                        teleportCheckCount = 100;
                        hasTeleportPoint = true;
                    }
                    while (!hasTeleportPoint && teleportCheckCount < 100)
                    {
                        teleportCheckCount++;
                        int tpTileX = Main.rand.Next(playerTileX - distFromPlayer, playerTileX + distFromPlayer);
                        int tpTileY = Main.rand.Next(playerTileY - distFromPlayer, playerTileY + distFromPlayer);
                        for (int tpY = tpTileY; tpY < playerTileY + distFromPlayer; tpY++)
                        {
                            if ((tpY < playerTileY - 4 || tpY > playerTileY + 4 || tpTileX < playerTileX - 4 || tpTileX > playerTileX + 4) && (tpY < tileY - 1 || tpY > tileY + 1 || tpTileX < tileX - 1 || tpTileX > tileX + 1) && (!checkGround || Main.tile[tpTileX, tpY].nactive()))
                            {
                                if ((CanTeleportTo != null && CanTeleportTo(tpTileX, tpY)) || (!Main.tile[tpTileX, tpY - 1].lava() && (!checkGround || Main.tileSolid[Main.tile[tpTileX, tpY].type]) && !Collision.SolidTiles(tpTileX - 1, tpTileX + 1, tpY - 4, tpY - 1)))
                                {
                                    if (attackInterval != -1) { npc.ai[1] = 20f; }
                                    npc.ai[2] = tpTileX;
                                    npc.ai[3] = tpY;
                                    hasTeleportPoint = true;
                                    teleport = false;
                                    digUp = true;
                                    break;
                                }
                            }
                        }
                    }
                    npc.netUpdate = true;
                }
            }
            
            if (attackInterval != -1 && npc.ai[1] > 0f)
            {
                npc.ai[1] -= 1f;
                if (npc.ai[1] == 25f)
                {
                    Projectile.NewProjectile(new Vector2(npc.position.X + 17f, npc.position.Y + 18f), new Vector2(-6 + Main.rand.Next(-6, 0), -4 + Main.rand.Next(-4, 0)), mod.ProjectileType("Magma"), 15, 3); ;
                }
            }

            npc.frameCounter++;

            if (npc.ai[0] >= teleportInterval && Main.netMode != 1) //walk or charge
            {
                if (npc.frameCounter >= 6)
                {
                    npc.frameCounter = 0;
                    npc.frame.Y += frameHeight;
                    if (npc.frame.Y > (frameHeight * 14))
                    {
                        npc.alpha = 255;
                        teleport = true;
                        npc.frameCounter = 0;
                        npc.frame.Y = 0;
                    }
                }
            }

            if (digUp) //walk or charge
            {
                if (npc.frameCounter >= 6)
                {
                    npc.frameCounter = 0;
                    npc.frame.Y += frameHeight;
                    if (npc.frame.Y > (frameHeight * 5))
                    {
                        npc.alpha = 0;
                        digUp = false;
                        npc.frameCounter = 0;
                        npc.frame.Y = frameHeight * 6;
                    }
                }
            }

            if (attackInterval != -1 && npc.ai[1] > 0f)
            {
                if (npc.frameCounter >= 6)
                {
                    npc.frameCounter = 0;
                    npc.frame.Y += frameHeight;
                    if (npc.frame.Y > (frameHeight * 5))
                    {
                        npc.frameCounter = 0;
                        npc.frame.Y = frameHeight * 6;
                    }
                }
            }

            else if (npc.ai[1] == 25f)
            {
                npc.frame.Y = frameHeight * 6;
            }
            else
            {
                npc.frameCounter = 0;
            }
        }
        
		public override void HitEffect(int hitDirection, double damage)
		{

            int dust1 = ModContent.DustType<Dusts.BroodmotherDust>();
            if (npc.life <= 0)
			{
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust1, 0f, 0f, 0);
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust1, 0f, 0f, 0);
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust1, 0f, 0f, 0);
            }
		}

		public override void NPCLoot()
		{
            if (Main.rand.NextFloat() < 0.1f)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("DragonScale"));
            }
        }
	}
}