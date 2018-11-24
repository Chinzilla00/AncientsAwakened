using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Hydra
{
    [AutoloadBossHead]
    public class Hydra : YamataBoss
	{
        public NPC Head1;
        public NPC Head2;
        public NPC Head3;
        public bool HeadsSpawned = false;

        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if ((Main.netMode == 2 || Main.dedServ))
            {
                writer.Write((float)internalAI[0]);
                writer.Write((float)internalAI[1]);
                writer.Write((float)internalAI[2]);
                writer.Write((float)internalAI[3]);
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
            }
        }

        public override void SetStaticDefaults()
        {
            displayName = "Hydra";
            Main.npcFrameCount[npc.type] = 9;
        }

        public override void SetDefaults()
        {
            npc.npcSlots = 100;
            npc.width = 40;
            npc.height = 45;
            npc.aiStyle = -1;
            npc.damage = 35;
            npc.defense = 9;
            npc.lifeMax = 5000;
            npc.value = Item.buyPrice(0, 5, 0, 0);
            npc.DeathSound = new LegacySoundStyle(2, 88, Terraria.Audio.SoundType.Sound);
            npc.knockBackResist = 0f;
            npc.boss = true;
            music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/HydraTheme");
            npc.noGravity = false;
            npc.netAlways = true;
            for (int m = 0; m < npc.buffImmune.Length; m++) npc.buffImmune[m] = true;
            frameWidth = 74;
            frameHeight = 44;
            npc.frame = BaseDrawing.GetFrame(frameCount, frameWidth, frameHeight, 0, 2);
            frameBottom = BaseDrawing.GetFrame(frameCount, frameWidth, 44, 0, 2);
            if (Main.expertMode)
            {
                int playerCount = 0;
                float bossHPScalar = 1f, scalarIncrement = 0.35f;
                if (Main.netMode != 0)
                {
                    for (int i = 0; i < 255; i++)
                    {
                        if (Main.player[i].active)
                        {
                            playerCount++;
                        }
                    }
                    for (int j = 1; j < playerCount; j++)
                    {
                        bossHPScalar += scalarIncrement;
                        scalarIncrement += (1f - scalarIncrement) / 3f;
                    }
                }
                ScaleExpertStats(playerCount, bossHPScalar);
            }
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.SuperHealingPotion;
        }

        public override bool G_CanSpawn(int x, int y, int type, Player player)
        {
            return false;
        }

        public override void NPCLoot()
        {
            AAWorld.downedHydra = true;
            //npc.DropLoot(Items.Boss.Hydra.HydraTrophy.type, 1f / 10);

            if (!Main.expertMode)
            {
                npc.DropLoot(mod.ItemType("HydraHide"), 30, 50);
                npc.DropLoot(mod.ItemType("Abyssium"), 40, 90);
                //npc.DropLoot(Items.Vanity.Mask.HydraMask.type, 1f / 7);
            }
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            npc.value = 0f;
            npc.boss = false;
            
        }

        public float[] internalAI = new float[4];
        public Rectangle frameBottom = new Rectangle(0, 0, 1, 1);

        
        public Vector2 bottomVisualOffset = default(Vector2);
        public Vector2 topVisualOffset = default(Vector2);
        public static NPC dustMantid = null;

        public override void AI()
        {
            if (!HeadsSpawned)
            {
                if (Main.netMode != 1)
                {
                    npc.realLife = npc.whoAmI;
                    int latestNPC = npc.whoAmI;
                    latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y - 100, mod.NPCType("HydraHead1"), 0, npc.whoAmI);
                    Main.npc[(int)latestNPC].realLife = npc.whoAmI;
                    Main.npc[(int)latestNPC].ai[3] = npc.whoAmI;
                    Head1 = Main.npc[latestNPC];
                    latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y - 100, mod.NPCType("HydraHead2"), 0, npc.whoAmI);
                    Main.npc[(int)latestNPC].realLife = npc.whoAmI;
                    Main.npc[(int)latestNPC].ai[3] = npc.whoAmI;
                    Head2 = Main.npc[latestNPC];
                    latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y - 100, mod.NPCType("HydraHead3"), 0, npc.whoAmI);
                    Main.npc[(int)latestNPC].realLife = npc.whoAmI;
                    Main.npc[(int)latestNPC].ai[3] = npc.whoAmI;
                    Head3 = Main.npc[latestNPC];
                }
                HeadsSpawned = true;
            }
            BaseAI.AIZombie(npc, ref npc.ai, false, false, -1, 0.07f, 1f, 7, 7, 1000000, true, 1000000, 1000000, true, null, false);
        }
        public override void FindFrame(int frameHeight)
        {

            npc.frameCounter++;
            if (!npc.collideX)
            {
                npc.frame.Y = 0 * frameHeight;
            }
            else
            {
                if (npc.frameCounter < 5)
                {
                    npc.frame.Y = 0 * frameHeight;
                }
                else if (npc.frameCounter < 10)
                {
                    npc.frame.Y = 1 * frameHeight;
                }
                else if (npc.frameCounter < 15)
                {
                    npc.frame.Y = 2 * frameHeight;
                }
                else if (npc.frameCounter < 20)
                {
                    npc.frame.Y = 3 * frameHeight;
                }
                else if (npc.frameCounter < 25)
                {
                    npc.frame.Y = 4 * frameHeight;
                }
                else if (npc.frameCounter < 30)
                {
                    npc.frame.Y = 5 * frameHeight;
                }
                else if (npc.frameCounter < 35)
                {
                    npc.frame.Y = 6 * frameHeight;
                }
                else if (npc.frameCounter < 40)
                {
                    npc.frame.Y = 7 * frameHeight;
                }
                else if (npc.frameCounter < 45)
                {
                    npc.frame.Y = 8 * frameHeight;
                }
                else
                {
                    npc.frameCounter = 0;
                }
            }
            
        }


        public void DrawHead(SpriteBatch spriteBatch, string headTexture, string glowMaskTexture, NPC head, Color drawColor)
        {
            if (head.active)
            {
                Vector2 neckOrigin = new Vector2(npc.Center.X, npc.Center.Y - 50);
                Vector2 center = head.Center;
                Vector2 distToProj = neckOrigin - head.Center;
                float projRotation = distToProj.ToRotation() - 1.57f;
                float distance = distToProj.Length();
                spriteBatch.Draw(mod.GetTexture("NPCs/Bosses/Hydra/HydraNeck"), neckOrigin - Main.screenPosition,
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
                    spriteBatch.Draw(mod.GetTexture("NPCs/Bosses/Hydra/HydraNeck"), new Vector2(center.X - Main.screenPosition.X, center.Y - Main.screenPosition.Y),
                        new Rectangle(0, 0, 14, 22), drawColor, projRotation,
                        new Vector2(14 * 0.5f, 22 * 0.5f), 1f, SpriteEffects.None, 0f);

                }
                spriteBatch.Draw(mod.GetTexture("NPCs/Bosses/Hydra/HydraNeck"), neckOrigin - Main.screenPosition,
                            new Rectangle(0, 0, 14, 22), drawColor, projRotation,
                            new Vector2(14 * 0.5f, 22 * 0.5f), 1f, SpriteEffects.None, 0f);

                spriteBatch.Draw(mod.GetTexture(headTexture), new Vector2(head.Center.X - Main.screenPosition.X, head.Center.Y - Main.screenPosition.Y),
                            head.frame, drawColor, head.rotation,
                            new Vector2(36 * 0.5f, 32 * 0.5f), 1f, SpriteEffects.None, 0f);
                spriteBatch.Draw(mod.GetTexture(glowMaskTexture), new Vector2(head.Center.X - Main.screenPosition.X, head.Center.Y - Main.screenPosition.Y),
                        head.frame, Color.White, head.rotation,
                        new Vector2(36 * 0.5f, 32 * 0.5f), 1f, SpriteEffects.None, 0f);
            }
        }

        public override bool PreDraw(SpriteBatch sb, Color dColor)
        {
            BaseDrawing.DrawTexture(sb, mod.GetTexture("NPCs/Bosses/Hydra/HydraTail"), 0, npc.position + new Vector2(0f, npc.gfxOffY) + bottomVisualOffset, npc.width, npc.height, npc.scale, npc.rotation, npc.spriteDirection, Main.npcFrameCount[npc.type], frameBottom, dColor, false);
            if (Main.netMode == 0)
            {
                DrawHead(sb, "NPCs/Bosses/Hydra/HydraHead2", "NPCs/Bosses/Hydra/HydraHead2_Glow", Head2, dColor);
                DrawHead(sb, "NPCs/Bosses/Hydra/HydraHead3", "NPCs/Bosses/Hydra/HydraHead3_Glow", Head3, dColor);
            }
            BaseDrawing.DrawTexture(sb, Main.npcTexture[npc.type], 0, npc.position + new Vector2(0f, npc.gfxOffY) + topVisualOffset, npc.width, npc.height, npc.scale, npc.rotation, npc.spriteDirection, Main.npcFrameCount[npc.type], npc.frame, dColor, false);
            if (Main.netMode == 0)
            {
                DrawHead(sb, "NPCs/Bosses/Hydra/HydraHead1", "NPCs/Bosses/Hydra/HydraHead1_Glow", Head1, dColor);
            }
            return false;
        }
    }
}