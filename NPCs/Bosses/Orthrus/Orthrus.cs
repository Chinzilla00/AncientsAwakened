using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using BaseMod;
using AAMod.NPCs.Bosses.Yamata.Awakened;

namespace AAMod.NPCs.Bosses.Orthrus
{
    [AutoloadBossHead]
    public class Orthrus : YamataBoss
	{
        public NPC Head1;
        public NPC Head2;
        public bool HeadsSpawned = false;

        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if ((Main.netMode == 2 || Main.dedServ))
            {
                writer.Write((float)internalAI[0]);
                writer.Write((float)internalAI[1]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == 1)
            {
                internalAI[0] = reader.ReadFloat();
                internalAI[1] = reader.ReadFloat();
            }
        }

        public override void SetStaticDefaults()
        {
            displayName = "Orthrus X";
            Main.npcFrameCount[npc.type] = 12;
        }

        public override void SetDefaults()
        {
            npc.npcSlots = 100;
            npc.width = 96;
            animationType = NPCID.HellArmoredBonesSword;
            npc.height = 78;
            npc.aiStyle = -1;
            npc.damage = 40;
            npc.defense = 99999999;
            npc.lifeMax = 22000;
            npc.value = Item.buyPrice(0, 10, 0, 0);
            npc.DeathSound = new LegacySoundStyle(2, 88, Terraria.Audio.SoundType.Sound);
            npc.knockBackResist = 0f;
            npc.boss = true;
            music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/Siege");
            npc.noGravity = false;
            npc.netAlways = true;
            npc.frame = BaseDrawing.GetFrame(frameCount, frameWidth, frameHeight, 0, 2);
            frameBottom = BaseDrawing.GetFrame(frameCount, frameWidth, 44, 0, 2);
            bossBag = mod.ItemType("OrthrusBag");
            npc.noTileCollide = false;
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }

        public override void NPCLoot()
        {
            if (Main.expertMode)
            {
                npc.DropBossBags();
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.SoulofMight, Main.rand.Next(25, 40));
            }
            else
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.SoulofMight, Main.rand.Next(20, 40));
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("FulguriteBar"), Main.rand.Next(30, 64));
                
            }
            AAWorld.downedOrthrus = true ;
        }
        
        public int playerTooFarDist = 800;
        public Rectangle frameBottom = new Rectangle(0, 0, 1, 1), frameHead = new Rectangle(0, 0, 1, 1);
        public bool prevHalfHPLeft = false, halfHPLeft = false, prevFourthHPLeft = false, fourthHPLeft = false;
        public Player playerTarget = null;
        public static int AISTATE_TURRET = 0, AISTATE_FLY = 1, AISTATE_DROP = 2, AISTATE_RISE = 3;
        public float[] internalAI = new float[4];

        //clientside stuff
        public Vector2 bottomVisualOffset = default(Vector2);
        public Vector2 topVisualOffset = default(Vector2);

        public override void AI()
        {

            npc.frameCounter++;

            if (internalAI[1] == AISTATE_TURRET)
            {
                if (npc.position.X + (float)(npc.width / 2) > Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) + 100f || npc.position.X + (float)(npc.width / 2) < Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - 100f)
                {
                    npc.noGravity = false;
                    npc.noTileCollide = false;
                    if (!HeadsSpawned)
                    {
                        if (Main.netMode != 1)
                        {
                            int latestNPC = npc.whoAmI;
                            latestNPC = NPC.NewNPC((int)npc.Center.X - 34, (int)npc.Center.Y - 23, mod.NPCType("OrthrusHead2"), 0, npc.whoAmI);
                            Main.npc[latestNPC].realLife = npc.whoAmI;
                            Main.npc[latestNPC].ai[0] = npc.whoAmI;
                            Head1 = Main.npc[latestNPC];
                            latestNPC = NPC.NewNPC((int)npc.Center.X + 34, (int)npc.Center.Y - 23, mod.NPCType("OrthrusHead1"), 0, npc.whoAmI);
                            Main.npc[latestNPC].realLife = npc.whoAmI;
                            Main.npc[latestNPC].ai[0] = npc.whoAmI;
                            Head2 = Main.npc[latestNPC];
                        }
                        HeadsSpawned = true;
                    }
                }
                else
                {
                    internalAI[1] = AISTATE_RISE;
                }
            }

            if (internalAI[1] == AISTATE_FLY)
            {

                HeadsSpawned = false;
                npc.noTileCollide = true;
                npc.noGravity = true;
                if (npc.position.X + (float)(npc.width / 2) > Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) + 100f  || npc.position.X + (float)(npc.width / 2) < Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - 100f)
                {
                    BaseAI.AIElemental(npc, ref npc.ai, false, 0, false, false, 800f, 100f, 60, 1.5f);
                }
                else
                {
                    internalAI[1] = AISTATE_DROP;
                }
            }

            if (internalAI[1] == AISTATE_TURRET) //Standing
            {
                if (npc.frameCounter >= 5)
                {
                    npc.frameCounter = 0;
                    npc.frame.Y += 102;
                    if (npc.frame.Y > (102 * 3))
                    {
                        npc.frameCounter = 0;
                        npc.frame.Y = 0;
                    }
                }
            }
            if (internalAI[1] == AISTATE_FLY) //Following
            {
                if (npc.frameCounter >= 5)
                {
                    npc.frameCounter = 0;
                    npc.frame.Y += 102;
                    if (npc.frame.Y > (102 * 7) || npc.frame.Y < (102 * 3))
                    {
                        npc.frameCounter = 0;
                        npc.frame.Y = 102 * 4;
                    }
                }
                npc.noGravity = true;
            }
            if (internalAI[1] == AISTATE_RISE) //Rising
            {
                if (npc.frameCounter < 5)
                {
                    npc.frame.Y = 102 * 11;
                }
                if (npc.frameCounter < 10)
                {
                    npc.frame.Y = 102 * 10;
                }
                if (npc.frameCounter < 15)
                {
                    npc.frame.Y = 102 * 9;
                }
                if (npc.frameCounter < 20)
                {
                    npc.frame.Y = 102 * 8;
                }
                if (npc.frameCounter < 25)
                {
                    npc.frameCounter = 0;
                    internalAI[1] = AISTATE_FLY;
                }
            }
            if (internalAI[1] == AISTATE_DROP) //Dropping
            {
                npc.noGravity = false;
                if (npc.frameCounter < 5)
                {
                    npc.frame.Y = 102 * 8;
                }
                if (npc.frameCounter < 10)
                {
                    npc.frame.Y = 102 * 9;
                }
                if (npc.frameCounter < 15)
                {
                    npc.frame.Y = 102 * 10;
                }
                if (npc.frameCounter < 20)
                {
                    npc.frame.Y = 102 * 11;
                }
                if (npc.frameCounter < 25)
                {
                    npc.frameCounter = 0;
                    internalAI[1] = AISTATE_TURRET;
                }
            }

        }

        public void AIMovementRunAway()
        {
            npc.velocity.X *= 0.9f;
            if (Math.Abs(npc.velocity.X) < 0.01f) npc.velocity.X = 0f;
            npc.velocity.Y += 0.25f;
			npc.noTileCollide = true;
            npc.rotation = 0f;
            if (npc.position.Y - npc.height - npc.velocity.Y >= Main.maxTilesY && Main.netMode != 1) { BaseAI.KillNPC(npc); npc.netUpdate2 = true; } //if out of map, kill boss
        }

        public void AIMovementNormal(float movementScalar = 1f, float playerDistance = -1f)
        {
            float movementScalar2 = Math.Min(4f, Math.Max(1f, (playerDistance / (float)playerTooFarDist) * 4f));
            bool playerTooFar = playerDistance > playerTooFarDist;
            BaseAI.AIZombie(npc, ref npc.ai, false, false, -1, 0.07f, 1f, 7, 7, 1000000, true, 1000000, 1000000, true, null, false);
            if (playerTooFar) npc.position += (playerTarget.position - playerTarget.oldPosition);
            npc.rotation = 0f;
        }

        

        public bool TargetClosest()
        {
            int[] players = BaseAI.GetPlayers(npc.Center, 4200f);
            float dist = 999999999f;
            int foundPlayer = -1;
            if (foundPlayer != -1)
            {
                BaseAI.SetTarget(npc, foundPlayer);
                playerTarget = Main.player[foundPlayer];
                return true;
            }
            else
            {
                for (int m = 0; m < players.Length; m++)
                {
                    Player p = Main.player[players[m]];
                    if (Vector2.Distance(p.Center, npc.Center) < dist)
                    {
                        dist = Vector2.Distance(p.Center, npc.Center);
                        foundPlayer = p.whoAmI;
                    }
                }
            }
            if (foundPlayer != -1)
            {
                BaseAI.SetTarget(npc, foundPlayer);
                playerTarget = Main.player[foundPlayer];
                return true;
            }
            return false;
        }

        
        public void DrawHead(SpriteBatch spriteBatch, string headTexture, string glowMaskTexture, NPC head, Color drawColor)
        {
            if (head != null && head.active)
            {
                string neckTex = ("NPCs/Bosses/Orthrus/OrthrusNeck");
                Texture2D neckTex2D = mod.GetTexture(neckTex);
                Vector2 neckOrigin = new Vector2(npc.Center.X, npc.Center.Y - 10);
                Vector2 connector = head.Center;
                BaseDrawing.DrawChain(spriteBatch, new Texture2D[] { null, neckTex2D, null }, 0, neckOrigin, connector, neckTex2D.Height - 10f, null, 1f, false, null);
                spriteBatch.Draw(mod.GetTexture(headTexture), new Vector2(head.Center.X - Main.screenPosition.X, head.Center.Y - Main.screenPosition.Y), head.frame, drawColor, head.rotation, new Vector2(36 * 0.5f, 32 * 0.5f), 1f, SpriteEffects.None, 0f);
                spriteBatch.Draw(mod.GetTexture(glowMaskTexture), new Vector2(head.Center.X - Main.screenPosition.X, head.Center.Y - Main.screenPosition.Y), head.frame, Color.White, head.rotation, new Vector2(36 * 0.5f, 32 * 0.5f), 1f, SpriteEffects.None, 0f);
                
            }
        }


        public override bool PreDraw(SpriteBatch sb, Color dColor)
        {
            DrawHead(sb, "NPCs/Bosses/Orthrus/OrthrusHead2", "NPCs/Bosses/Orthrus/OrthrusHead2_Glow", Head2, dColor); BaseDrawing.DrawTexture(sb, Main.npcTexture[npc.type], 0, npc.position + new Vector2(0f, npc.gfxOffY) + topVisualOffset, npc.width, npc.height, npc.scale, npc.rotation, npc.spriteDirection, Main.npcFrameCount[npc.type], npc.frame, dColor, false);
            DrawHead(sb, "NPCs/Bosses/Orthrus/OrthrusHead1", "NPCs/Bosses/Orthrus/OrthrusHead1_Glow", Head1, dColor);
            return false;
        }		
    }
}