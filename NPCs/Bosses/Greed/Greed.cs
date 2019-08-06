using System;
using System.IO;
using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Greed
{
    [AutoloadBossHead]	
	public class Greed : ModNPC
	{
        public int damage = 0;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Greed");
            Main.npcFrameCount[npc.type] = 3;
		}

		public override void SetDefaults()
		{
			npc.npcSlots = 5f;
            npc.width = 38;
            npc.height = 38;
            npc.damage = 35;
            npc.defense = 25;
            npc.lifeMax = 50000;
            npc.value = Item.buyPrice(0, 5, 0, 0);
            npc.knockBackResist = 0f;
            npc.aiStyle = -1;
            animationType = 10;
            npc.behindTiles = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit5;
            npc.DeathSound = SoundID.NPCDeath7;
            npc.netAlways = true;
            npc.boss = true;
            bossBag = mod.ItemType("GreedBag");
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Greed");
        }

        public float[] internalAI = new float[6];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == 2 || Main.dedServ)
            {
                writer.Write(internalAI[0]);
                writer.Write(internalAI[1]);
                writer.Write(internalAI[2]);
                writer.Write(internalAI[3]);
                writer.Write(internalAI[4]);
                writer.Write(internalAI[5]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == 1)
            {
                internalAI[0] = reader.ReadFloat();
                internalAI[1] = reader.ReadFloat();
                internalAI[2] = reader.ReadFloat();
                internalAI[3] = reader.ReadFloat();
                internalAI[4] = reader.ReadFloat();
                internalAI[5] = reader.ReadFloat();
            }
        }

        public override void AI()
        {
            bool isDigging = false;
            AIWorm(npc, ref isDigging, new int[] { mod.NPCType<Greed>(), mod.NPCType<GreedBody>(), mod.NPCType<GreedTail>(), }, 0f, 8f, 0.07f, true, true, true, true, true);
        }

        public static void AIWorm(NPC npc, ref bool isDigging, int[] wormTypes, float partDistanceAddon = 0f, float maxSpeed = 8f, float gravityResist = 0.07f, bool fly = false,  bool ignoreTiles = false, bool spawnTileDust = true, bool soundEffects = true, bool rotateAverage = false)
        {
            bool singlePiece = wormTypes.Length == 1;
            bool isHead = npc.type == wormTypes[0];
            bool isTail = npc.type == wormTypes[wormTypes.Length - 1];
            bool isBody = !isHead && !isTail;
            int wormLength = wormTypes.Length;

            if (npc.ai[3] > 0f)
                npc.realLife = (int)npc.ai[3];

            if (npc.ai[0] == -1f)
                npc.ai[0] = npc.whoAmI;

            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead)
                npc.TargetClosest(true);

            if (isHead)
            {
                if ((npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead) && npc.timeLeft > 300)
                    npc.timeLeft = 300;
            }
            else
            {
                npc.timeLeft = 50;
            }
            if (Main.netMode != 1)
            {
                if (!singlePiece)
                {
                    npc.ai[3] = npc.whoAmI;
                    npc.realLife = npc.whoAmI;
                    int npcID = npc.whoAmI;
                    for (int m = 1; m < wormLength - 1; m++)
                    {
                        int npcType = wormTypes[m];

                        float ai0 = 0;
                        float ai1 = npcID;
                        float ai2 = m;
                        float ai3 = npc.ai[3];

                        int newnpcID = NPC.NewNPC((int)(npc.Center.X), (int)(npc.Center.Y), npcType, npc.whoAmI, ai0, ai1, ai2, ai3);
                        Main.npc[npcID].ai[0] = newnpcID;
                        Main.npc[npcID].netUpdate = true;
                        npcID = newnpcID;
                    }
                    npc.netUpdate = true;
                }
                if (npc.type != wormTypes[0] && (!Main.npc[(int)npc.ai[1]].active || Main.npc[(int)npc.ai[1]].aiStyle != npc.aiStyle))
                {
                    npc.life = 0;
                    npc.HitEffect(0, 10.0);
                    npc.active = false;
                }
                if (npc.type != wormTypes[wormTypes.Length - 1] && (!Main.npc[(int)npc.ai[0]].active || Main.npc[(int)npc.ai[0]].aiStyle != npc.aiStyle))
                {
                    npc.life = 0;
                    npc.HitEffect(0, 10.0);
                    npc.active = false;
                }
                if (!npc.active && Main.netMode == 2)
                    NetMessage.SendData(28, -1, -1, NetworkText.FromLiteral(""), npc.whoAmI, 1, 0f, 0f, -1);
            }
            int tileX = (int)(npc.position.X / 16f) - 1;
            int tileCenterX = (int)((npc.Center.X) / 16f) + 2;
            int tileY = (int)(npc.position.Y / 16f) - 1;
            int tileCenterY = (int)((npc.Center.Y) / 16f) + 2;
            if (tileX < 0) { tileX = 0; }
            if (tileCenterX > Main.maxTilesX) { tileCenterX = Main.maxTilesX; }
            if (tileY < 0) { tileY = 0; }
            if (tileCenterY > Main.maxTilesY) { tileCenterY = Main.maxTilesY; }
            bool canMove = false;
            if (fly || ignoreTiles) { canMove = true; }
            if (!canMove || spawnTileDust)
            {
                for (int tX = tileX; tX < tileCenterX; tX++)
                {
                    for (int tY = tileY; tY < tileCenterY; tY++)
                    {
                        Tile checkTile = BaseWorldGen.GetTileSafely(tX, tY);
                        if (checkTile != null && ((checkTile.nactive() && (Main.tileSolid[checkTile.type] || (Main.tileSolidTop[checkTile.type] && checkTile.frameY == 0))) || checkTile.liquid > 64))
                        {
                            Vector2 tPos;
                            tPos.X = tX * 16;
                            tPos.Y = tY * 16;
                            if (npc.position.X + npc.width > tPos.X && npc.position.X < tPos.X + 16f && npc.position.Y + npc.height > tPos.Y && npc.position.Y < tPos.Y + 16f)
                            {
                                canMove = true;
                                if (spawnTileDust && Main.rand.Next(100) == 0 && checkTile.nactive())
                                {
                                    WorldGen.KillTile(tX, tY, true, true, false);
                                }
                            }
                        }
                    }
                }
            }
            if (!canMove && npc.type == wormTypes[0])
            {
                Rectangle rectangle = new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height);
                int playerCheckDistance = 1000;
                bool canMove2 = true;
                for (int m3 = 0; m3 < 255; m3++)
                {
                    if (Main.player[m3].active)
                    {
                        Rectangle rectangle2 = new Rectangle((int)Main.player[m3].position.X - playerCheckDistance, (int)Main.player[m3].position.Y - playerCheckDistance, playerCheckDistance * 2, playerCheckDistance * 2);
                        if (rectangle.Intersects(rectangle2))
                        {
                            canMove2 = false;
                            break;
                        }
                    }
                }
                if (canMove2) { canMove = true; }
            }
            if (fly)
            {
                if (npc.velocity.X < 0f) { npc.spriteDirection = 1; } else if (npc.velocity.X > 0f) { npc.spriteDirection = -1; }
            }
            Vector2 npcCenter = npc.Center;
            float playerCenterX = Main.player[npc.target].Center.X;
            float playerCenterY = Main.player[npc.target].Center.Y;
            playerCenterX = (int)(playerCenterX / 16f) * 16; playerCenterY = (int)(playerCenterY / 16f) * 16;
            npcCenter.X = (int)(npcCenter.X / 16f) * 16; npcCenter.Y = (int)(npcCenter.Y / 16f) * 16;
            playerCenterX -= npcCenter.X; playerCenterY -= npcCenter.Y;
            float dist = (float)Math.Sqrt(playerCenterX * playerCenterX + playerCenterY * playerCenterY);
            isDigging = canMove;
            if (npc.ai[1] > 0f && npc.ai[1] < Main.npc.Length)
            {
                try
                {
                    npcCenter = npc.Center;
                    playerCenterX = Main.npc[(int)npc.ai[1]].Center.X - npcCenter.X;
                    playerCenterY = Main.npc[(int)npc.ai[1]].Center.Y - npcCenter.Y;
                }
                catch
                {
                }
                if (!rotateAverage || npc.type == wormTypes[0])
                {
                    npc.rotation = (float)Math.Atan2(playerCenterY, playerCenterX) + 1.57f;
                }
                else
                {
                    NPC frontNPC = Main.npc[(int)npc.ai[1]];
                    Vector2 rotVec = BaseUtility.RotateVector(frontNPC.Center, frontNPC.Center + new Vector2(0f, 30f), frontNPC.rotation);
                    npc.rotation = BaseUtility.RotationTo(npc.Center, rotVec) + 1.57f;
                }
                dist = (float)Math.Sqrt(playerCenterX * playerCenterX + playerCenterY * playerCenterY);
                dist = (dist - npc.width - partDistanceAddon) / dist;
                playerCenterX *= dist;
                playerCenterY *= dist;
                npc.velocity = default;
                npc.position.X += playerCenterX;
                npc.position.Y += playerCenterY;
                if (fly)
                {
                    if (playerCenterX < 0f) { npc.spriteDirection = 1; return; }
                    else
                    if (playerCenterX > 0f) { npc.spriteDirection = -1; return; }
                }
            }
            else
            {
                if (!canMove)
                {
                    npc.TargetClosest(true);
                    npc.velocity.Y += 0.11f;
                    if (npc.velocity.Y > maxSpeed) { npc.velocity.Y = maxSpeed; }
                    if (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) < maxSpeed * 0.4)
                    {
                        if (npc.velocity.X < 0f) { npc.velocity.X -= gravityResist * 1.1f; } else { npc.velocity.X += gravityResist * 1.1f; }
                    }
                    else
                    if (npc.velocity.Y == maxSpeed)
                    {
                        if (npc.velocity.X < playerCenterX) { npc.velocity.X += gravityResist; }
                        else
                        if (npc.velocity.X > playerCenterX) { npc.velocity.X -= gravityResist; }
                    }
                    else
                    if (npc.velocity.Y > 4f)
                    {
                        if (npc.velocity.X < 0f) { npc.velocity.X += gravityResist * 0.9f; } else { npc.velocity.X -= gravityResist * 0.9f; }
                    }
                }
                else
                {
                    if (soundEffects && npc.soundDelay == 0)
                    {
                        float distSoundDelay = dist / 40f;
                        if (distSoundDelay < 10f) { distSoundDelay = 10f; }
                        if (distSoundDelay > 20f) { distSoundDelay = 20f; }
                        npc.soundDelay = (int)distSoundDelay;
                        Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 1);
                    }
                    dist = (float)Math.Sqrt(playerCenterX * playerCenterX + playerCenterY * playerCenterY);
                    float absPlayerCenterX = Math.Abs(playerCenterX);
                    float absPlayerCenterY = Math.Abs(playerCenterY);
                    float newSpeed = maxSpeed / dist;
                    playerCenterX *= newSpeed;
                    playerCenterY *= newSpeed;
                    bool dontFall = false;
                    if (fly)
                    {
                        if (((npc.velocity.X > 0f && playerCenterX < 0f) || (npc.velocity.X < 0f && playerCenterX > 0f) || (npc.velocity.Y > 0f && playerCenterY < 0f) || (npc.velocity.Y < 0f && playerCenterY > 0f)) && Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) > gravityResist / 2f && dist < 300f)
                        {
                            dontFall = true;
                            if (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) < maxSpeed) { npc.velocity *= 1.1f; }
                        }
                        if (npc.position.Y > Main.player[npc.target].position.Y || Main.player[npc.target].position.Y / 16f > Main.worldSurface || Main.player[npc.target].dead)
                        {
                            dontFall = true;
                            if (Math.Abs(npc.velocity.X) < maxSpeed / 2f)
                            {
                                if (npc.velocity.X == 0f) { npc.velocity.X -= npc.direction; }
                                npc.velocity.X *= 1.1f;
                            }
                            else
                                if (npc.velocity.Y > -maxSpeed) { npc.velocity.Y -= gravityResist; }
                        }
                    }
                    if (!dontFall)
                    {
                        if ((npc.velocity.X > 0f && playerCenterX > 0f) || (npc.velocity.X < 0f && playerCenterX < 0f) || (npc.velocity.Y > 0f && playerCenterY > 0f) || (npc.velocity.Y < 0f && playerCenterY < 0f))
                        {
                            if (npc.velocity.X < playerCenterX) { npc.velocity.X += gravityResist; }
                            else
                                if (npc.velocity.X > playerCenterX) { npc.velocity.X -= gravityResist; }
                            if (npc.velocity.Y < playerCenterY) { npc.velocity.Y += gravityResist; }
                            else
                                if (npc.velocity.Y > playerCenterY) { npc.velocity.Y -= gravityResist; }
                            if (Math.Abs(playerCenterY) < maxSpeed * 0.2 && ((npc.velocity.X > 0f && playerCenterX < 0f) || (npc.velocity.X < 0f && playerCenterX > 0f)))
                            {
                                if (npc.velocity.Y > 0f) { npc.velocity.Y += gravityResist * 2f; } else { npc.velocity.Y -= gravityResist * 2f; }
                            }
                            if (Math.Abs(playerCenterX) < maxSpeed * 0.2 && ((npc.velocity.Y > 0f && playerCenterY < 0f) || (npc.velocity.Y < 0f && playerCenterY > 0f)))
                            {
                                if (npc.velocity.X > 0f) { npc.velocity.X += gravityResist * 2f; } else { npc.velocity.X -= gravityResist * 2f; }
                            }
                        }
                        else
                            if (absPlayerCenterX > absPlayerCenterY)
                        {
                            if (npc.velocity.X < playerCenterX) { npc.velocity.X += gravityResist * 1.1f; }
                            else
                                if (npc.velocity.X > playerCenterX) { npc.velocity.X -= gravityResist * 1.1f; }

                            if (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) < maxSpeed * 0.5)
                            {
                                if (npc.velocity.Y > 0f) { npc.velocity.Y += gravityResist; } else { npc.velocity.Y -= gravityResist; }
                            }
                        }
                        else
                        {
                            if (npc.velocity.Y < playerCenterY) { npc.velocity.Y += gravityResist * 1.1f; }
                            else
                                if (npc.velocity.Y > playerCenterY) { npc.velocity.Y -= gravityResist * 1.1f; }
                            if (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) < maxSpeed * 0.5)
                            {
                                if (npc.velocity.X > 0f) { npc.velocity.X += gravityResist; } else { npc.velocity.X -= gravityResist; }
                            }
                        }
                    }
                }
                if (!rotateAverage || npc.type == wormTypes[0])
                {
                    npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X) + 1.57f;
                }
                else
                {
                    NPC frontNPC = Main.npc[(int)npc.ai[1]];
                    Vector2 rotVec = BaseUtility.RotateVector(frontNPC.Center, frontNPC.Center + new Vector2(0f, 30f), frontNPC.rotation);
                    npc.rotation = BaseUtility.RotationTo(npc.Center, rotVec) + 1.57f;
                }
                if (npc.type == wormTypes[0])
                {
                    if (canMove)
                    {
                        if (npc.localAI[0] != 1f) { npc.netUpdate = true; }
                        npc.localAI[0] = 1f;
                    }
                    else
                    {
                        if (npc.localAI[0] != 0f) { npc.netUpdate = true; }
                        npc.localAI[0] = 0f;
                    }
                    if (((npc.velocity.X > 0f && npc.oldVelocity.X < 0f) || (npc.velocity.X < 0f && npc.oldVelocity.X > 0f) || (npc.velocity.Y > 0f && npc.oldVelocity.Y < 0f) || (npc.velocity.Y < 0f && npc.oldVelocity.Y > 0f)) && !npc.justHit)
                    {
                        npc.netUpdate = true;
                        return;
                    }
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            if (npc.type == mod.NPCType<GreedBody>())
            {
                npc.frame.Y = frameHeight * (int)npc.ai[2];
            }
        }

        public override void PostAI()
        {
            if (npc.type == mod.NPCType<GreedBody>())
            {
                switch ((int)npc.ai[2])
                {
                }

            }
        }

        public override void OnHitPlayer(Player player, int damage, bool crit)
		{
			if (Main.expertMode)
			{
				player.AddBuff(BuffID.Chilled, 200, true);
			}
			else
			{
				player.AddBuff(BuffID.Chilled, 100, true);
			}
		}

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.HealingPotion;   //boss drops
            AAWorld.downedSerpent = true;
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.lifeMax = (int)(npc.lifeMax * 0.75f * bossLifeScale);
			npc.damage = (int)(npc.damage * 0.85f);
		}

        public override void HitEffect(int hitDirection, double damage)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, DustID.Dirt, hitDirection, -1f, 0, default, 1f);
            }
            if (npc.life == 0)
            {
                for (int k = 0; k < 5; k++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, DustID.Dirt, hitDirection, -1f, 0, default, 1f);
                }
            }
        }

        public override void NPCLoot()
        {
            if (!Main.expertMode)
            {
                AAWorld.downedSerpent = true;
                npc.DropLoot(mod.ItemType("CovetiteCoin"), 10, 15);
                string[] lootTable = {  };
                int loot = Main.rand.Next(lootTable.Length);
                //npc.DropLoot(Items.Vanity.Mask.GreedMask.type, 1f / 7);
                npc.DropLoot(mod.ItemType(lootTable[loot]));
            }
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("GreedTrophy"));
            }
            npc.value = 0f;
            npc.boss = false;
        }
    }

    public class GreedBody : Greed
    {
        public override string Texture => "AAMod/NPCs/Bosses/Greed/GreedBody";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Greed");
            Main.npcFrameCount[npc.type] = 23;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.dontCountMe = true;
        }

        public override bool PreNPCLoot()
        {
            return false;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }

        public override bool CheckActive()
        {
            if (NPC.AnyNPCs(mod.NPCType<Greed>()))
            {
                return false;
            }
            return true;
        }
    }

    public class GreedTail : Greed
    {
        public override string Texture => "AAMod/NPCs/Bosses/Greed/GreedTail";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Greed");
            Main.npcFrameCount[npc.type] = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.dontCountMe = true;
        }

        public override bool PreNPCLoot()
        {
            return false;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }

        public override bool CheckActive()
        {
            if (NPC.AnyNPCs(mod.NPCType<Greed>()))
            {
                return false;
            }
            return true;
        }
    }
}