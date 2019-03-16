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
    public class Sagittarius : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sagittarius");
		}

		public override void SetDefaults()
		{
            npc.lifeMax = 7000;
            npc.defense = 130;
            npc.damage = 40;
            npc.width = 92;
            npc.height = 92;
            npc.aiStyle = -1;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Sagittarius");
            npc.knockBackResist = 0f;
            npc.noGravity = true;
        }

        public float[] shootAI = new float[1];
        public float[] MovementType = new float[1];
        public float[] internalAI = new float[6];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if ((Main.netMode == 2 || Main.dedServ))
            {
                writer.Write(internalAI[0]);
                writer.Write(internalAI[1]);
                writer.Write(internalAI[2]);
                writer.Write(internalAI[3]);
                writer.Write(internalAI[4]);
                writer.Write(internalAI[5]);
                writer.Write(shootAI[0]);
                writer.Write(MovementType[0]);
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
                shootAI[0] = reader.ReadFloat();
                MovementType[0] = reader.ReadFloat();
            }
        }

        public int ProbeCount = Main.expertMode ? 6 : 4;

        public override void AI()
        {
            npc.noGravity = true;
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];

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
                        Main.npc[npcID].netUpdate2 = true;
                    }
                }
                internalAI[0] = 1;
            }

            internalAI[5]++;
            if (internalAI[5] > 200)
            {
                internalAI[5] = 0;
                MovementType[0] = Main.rand.Next(3);
                npc.netUpdate = true;
            }

            if (internalAI[4] < 60)
            {
                internalAI[4]++;
            }
            if (!NPC.AnyNPCs(mod.NPCType<SagittariusOrbiter>()) && internalAI[4] >= 60)
            {
                npc.Transform(mod.NPCType<SagittariusFree>());
            }

            if (Main.player[npc.target].dead)
            {
                npc.TargetClosest(true);
                if (Main.player[npc.target].dead)
                {
                    if (internalAI[3] != 2f || internalAI[3] != 3f)
                    {
                        internalAI[3] = 1f;
                    }
                }
            }
            else if (Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 5000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 5000f)
            {
                npc.TargetClosest(true);
                if (Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 5000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 5000f)
                {
                    if (internalAI[3] != 1f || internalAI[3] != 3f)
                    {
                        internalAI[3] = 2f;
                    }
                }
            }

            if (internalAI[3] == 1f)
            {
                Main.NewText("target(s) neutralized. returning to stealth mode.", Color.PaleVioletRed);
                internalAI[3] = 3f;
            }
            else if (internalAI[0] == 2f)
            {
                Main.NewText("target(s) lost. returning to stealth mode.", Color.PaleVioletRed);
                internalAI[3] = 3f;
            }
            else if (internalAI[3] == 3f)
            {
                npc.TargetClosest(true);
                npc.velocity *= .7f;
                npc.alpha += 5;
                if (npc.alpha >= 255)
                {
                    npc.active = false;
                }
                if (Main.player[npc.target].dead || Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 6000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 6000f)
                {
                    npc.TargetClosest(true);

                    internalAI[0] = 0f;
                }
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

            npc.ai[1]++;

            if (npc.ai[1] >= 300)
            {
                internalAI[1] = 0;
                internalAI[2] = Main.rand.Next(3);
                npc.ai = new float[4];
                npc.netUpdate = true;
            }


            if (internalAI[2] == 0) //Hover over target
            {
                BaseAI.AISpaceOctopus(npc, ref npc.ai, Main.player[npc.target].Center, 0.07f, 6f, 250f, 70f, null);
                npc.rotation = 0;
            }
            else
            if (internalAI[2] == 1) //Chaase down the target
            {
                BaseAI.AIElemental(npc, ref npc.ai, null, 120, false, false, 10, 10, 10, 1f);
                npc.rotation = 0;
            }
            else //Charge the target
            {
                BaseAI.AIWeapon(npc, ref npc.ai, 120, 100, 4f, 1f, .5f);
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                int dust1 = mod.DustType<Dusts.VoidDust>();
                int dust2 = mod.DustType<Dusts.VoidDust>();
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust1, 0f, 0f, 0, default(Color), 1f);
                Main.dust[dust1].velocity *= 0.5f;
                Main.dust[dust1].scale *= 1.3f;
                Main.dust[dust1].fadeIn = 1f;
                Main.dust[dust1].noGravity = false;
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust2, 0f, 0f, 0, default(Color), 1f);
                Main.dust[dust2].velocity *= 0.5f;
                Main.dust[dust2].scale *= 1.3f;
                Main.dust[dust2].fadeIn = 1f;
                Main.dust[dust2].noGravity = true;
            }
        }

        public override bool PreDraw(SpriteBatch sb, Color dColor)
        {
            BaseDrawing.DrawTexture(sb, Main.npcTexture[npc.type], 0, npc, dColor);
            BaseDrawing.DrawTexture(sb, mod.GetTexture("Glowmasks/SagittariusBoss_Glow"), 0, npc, AAColor.ZeroShield);
            BaseDrawing.DrawAfterimage(sb, mod.GetTexture("Glowmasks/SagittariusBoss_Glow"), 0, npc, 2f, 0.9f, 5, true, 0f, 0f, AAColor.ZeroShield);
            return false;
        }

        public override void NPCLoot()
        {
            Item.NewItem(npc.Center, mod.ItemType<Items.Materials.VoidEnergy>(), Main.rand.Next(30, 40));
        }
    }
}
