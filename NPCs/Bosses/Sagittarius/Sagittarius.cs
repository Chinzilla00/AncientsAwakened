using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;
using System.IO;

namespace AAMod.NPCs.Bosses.Sagittarius
{
    [AutoloadBossHead]
    public class Sagittarius : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sagittarius");
		}

		public override void SetDefaults()
        {
            npc.lifeMax = 6000;
            npc.boss = true;
            npc.defense = 300;
            npc.damage = 35;
            npc.width = 92;
            npc.height = 92;
            npc.aiStyle = -1;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Sagittarius");
            npc.knockBackResist = 0f;
            npc.noGravity = true;
            npc.noTileCollide = true;
            bossBag = mod.ItemType("SagBag");
        }

        public static float[] MovementType = new float[2];
        public float[] shootAI = new float[1];
        public float[] internalAI = new float[7];
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
                writer.Write(internalAI[6]);
                writer.Write(shootAI[0]);
                writer.Write(MovementType[0]);
                writer.Write(MovementType[1]);
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
                internalAI[6] = reader.ReadFloat();
                shootAI[0] = reader.ReadFloat();
                MovementType[0] = reader.ReadFloat();
                MovementType[1] = reader.ReadFloat();
            }
        }

        public int ProbeCount = Main.expertMode ? 12 : 6;

        public override void AI()
        {
            npc.noGravity = true;
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);

            if (player.Center.X > npc.Center.X)
            {
                npc.direction = -1;
            }
            else
            {
                npc.direction = 1;
            }

            if (internalAI[0] == 0)
            {
                if (Main.netMode != 1)
                {
                    for (int m = 0; m < ProbeCount; m++)
                    {
                        int npcID = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("SagittariusOrbiter"), 0);
                        Main.npc[npcID].Center = npc.Center;
                        Main.npc[npcID].velocity = new Vector2(MathHelper.Lerp(-1f, 1f, (float)Main.rand.NextDouble()), MathHelper.Lerp(-1f, 1f, (float)Main.rand.NextDouble()));
                        Main.npc[npcID].velocity *= 8f;
                        Main.npc[npcID].ai[0] = m;
                        Main.npc[npcID].netUpdate2 = true; Main.npc[npcID].netUpdate = true;
                    }
                }
                internalAI[0] = 1;
            }

            if (MovementType[0] == 0)
            {
                internalAI[6]++;

                MovementType[1] -= 5;
                if (MovementType[1] <= 0)
                {
                    MovementType[1] = 0;
                }

                if (internalAI[6] > 200)
                {
                    internalAI[6] = 0f;
                    MovementType[0] = Main.rand.Next(3);
                    npc.netUpdate = true;
                }
            }
            if (MovementType[0] == 1)
            {
                internalAI[6]++;

                if (internalAI[6] > 240)
                {
                    internalAI[6] = 0;
                    MovementType[0] = 5;
                    npc.netUpdate = true;
                }
            }
            if (MovementType[0] == 2)
            {
                MovementType[1] += 5;
                if (MovementType[1] >= 255)
                {
                    MovementType[0] = 3;
                    npc.netUpdate = true;
                }
            }
            if (MovementType[0] == 3)
            {
                MovementType[1] -= 5;
                if (MovementType[1] <= 0)
                {
                    MovementType[1] = 0;
                }
                
                internalAI[6]++;

                if (internalAI[6] > 360)
                {
                    internalAI[6] = 0;
                    MovementType[0] = 5;
                    npc.netUpdate = true;
                }

            }
            else if (MovementType[0] == 4 || MovementType[0] == 5)
            {
                MovementType[1] += 5;
                if (MovementType[1] >= 255)
                {
                    MovementType[0] = 0;
                    npc.netUpdate = true;
                }
            }

            if (internalAI[4] < 60)
            {
                internalAI[4]++;
            }
            if (!NPC.AnyNPCs(mod.NPCType<SagittariusOrbiter>()) && internalAI[4] >= 60)
            {
                npc.Transform(mod.NPCType<SagittariusFree>());
            }

            if (internalAI[3] == 1f)
            {
                npc.TargetClosest(true);
                npc.velocity *= .7f;
                npc.alpha += 5;
                if (npc.alpha >= 255)
                {
                    npc.active = false;
                }
                if (!Main.player[npc.target].dead || Math.Abs(npc.position.X - Main.player[npc.target].position.X) <= 6000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) >= 6000f)
                {
                    npc.TargetClosest(true);

                    internalAI[0] = 0f;
                }
                return;
            }
            else
            {
                npc.TargetClosest(true);
                if (npc.alpha > 0)
                {
                    npc.alpha -= 10;
                }
                if (npc.alpha <= 0)
                {
                    npc.alpha = 0;
                }
            }

            if (Main.player[npc.target].dead)
            {
                npc.TargetClosest(true);
                if (Main.player[npc.target].dead)
                {
                    if (internalAI[3] != 1f)
                    {
                        if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("Sagittarius1"), Color.PaleVioletRed);
                        internalAI[3] = 1f;
                    }
                }
            }
            else if (Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 5000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 5000f || !modPlayer.ZoneVoid)
            {
                npc.TargetClosest(true);
                if (Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 5000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 5000f)
                {
                    if (internalAI[3] != 1f)
                    {
                        if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("Sagittarius2"), Color.PaleVioletRed);
                        internalAI[3] = 1f;
                    }
                }
            }
            

            npc.ai[1]++;

            if (npc.ai[1] >= 300)
            {
                internalAI[1] = 0;
                internalAI[2] = Main.rand.Next(3);
                npc.ai = new float[4];
                npc.netUpdate = true;
            }


            
            if (internalAI[2] == 1) //Chaase down the target
            {
                BaseAI.AIElemental(npc, ref npc.ai, null, 120, false, false, 10, 10, 10, 2.5f);
                npc.rotation = 0;
            }
            else //Hover over target
            {
                BaseAI.AISpaceOctopus(npc, ref npc.ai, Main.player[npc.target].Center, 0.3f, 7f, 250f, 70f, null);
                npc.rotation = 0;
            }

            npc.noTileCollide = true;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                int dust1 = mod.DustType<Dusts.VoidDust>();
                int dust2 = mod.DustType<Dusts.VoidDust>();
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust1, 0f, 0f, 0);
                Main.dust[dust1].velocity *= 0.5f;
                Main.dust[dust1].scale *= 1.3f;
                Main.dust[dust1].fadeIn = 1f;
                Main.dust[dust1].noGravity = false;
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust2, 0f, 0f, 0);
                Main.dust[dust2].velocity *= 0.5f;
                Main.dust[dust2].scale *= 1.3f;
                Main.dust[dust2].fadeIn = 1f;
                Main.dust[dust2].noGravity = true;
            }
        }

        public override bool PreDraw(SpriteBatch sb, Color dColor)
        {
            BaseDrawing.DrawTexture(sb, Main.npcTexture[npc.type], 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, 1, npc.frame, dColor, true);
            BaseDrawing.DrawTexture(sb, mod.GetTexture("Glowmasks/Sagittarius_Glow"), 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, 1, npc.frame, GenericUtils.COLOR_GLOWPULSE, true);
            return false;
        }

        public override void NPCLoot()
        {
            AAWorld.downedSag = true;
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SagTrophy"));
            }
            if (!Main.expertMode)
            {
                if (Main.rand.Next(7) == 0)
                {
                    npc.DropLoot(mod.ItemType("SagMask"));
                }
                string[] lootTable = { "SagCore", "NeutronStaff", "Legg" };
                int loot = Main.rand.Next(lootTable.Length);
                npc.DropLoot(mod.ItemType(lootTable[loot]));
                Item.NewItem(npc.Center, mod.ItemType<Items.Materials.Doomite>(), Main.rand.Next(30, 40));
            }
            else
            {
                npc.DropBossBags();
            }
        }
        
    }
}
