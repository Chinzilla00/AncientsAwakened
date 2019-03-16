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
    public class SagittariusFree : Sagittarius
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sagittarius-A");
            Main.npcFrameCount[npc.type] = 4;
		}

		public override void SetDefaults()
		{
            npc.lifeMax = 7000;
            npc.defense = 0;
            npc.damage = 55;
            npc.width = 74;
            npc.height = 70;
            npc.aiStyle = -1;
            npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/Sounds/Zerohit");
            npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/Sounds/ZeroDeath");
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Sagittarius");
            npc.knockBackResist = 0f;
            npc.noGravity = true;
            npc.alpha = 255;
        }
        
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if ((Main.netMode == 2 || Main.dedServ))
            {
                writer.Write(internalAI[0]);
                writer.Write(internalAI[1]);
                writer.Write(internalAI[2]);
                writer.Write(internalAI[3]);
                writer.Write(shootAI[0]);
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
                shootAI[0] = reader.ReadFloat();
            }
        }
       
        public override void AI()
        {
            npc.noGravity = true;
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];
            AAPlayer modplayer = player.GetModPlayer<AAPlayer>(mod);
            RingRoatation += .3f;
            npc.ai[1]++;
            if (internalAI[3] > 0)
            {
                internalAI[3]--;
            }

            if (Main.player[npc.target].dead && Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 5000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 5000f)
            {
                npc.TargetClosest(true);
                if (Main.player[npc.target].dead)
                {
                    if (internalAI[0] != 2f || internalAI[0] != 3f)
                    {
                        Main.NewText("target(s) neutralized. returning to stealth mode.", Color.PaleVioletRed);
                        internalAI[0] = 3f;
                    }
                }
                if (Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 5000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 5000f)
                {
                    if (internalAI[0] != 1f || internalAI[0] != 3f)
                    {
                        Main.NewText("target(s) lost. returning to stealth mode.", Color.PaleVioletRed);
                        internalAI[0] = 3f;
                    }
                }
            }
            if (internalAI[0] == 3f)
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

            if (!modplayer.ZoneVoid && !AAWorld.downedZero && !NPC.AnyNPCs(mod.NPCType<VoidReturn>()))
            {
                Main.NewText("target is attempting to leave the vicininty. engage retrieval program.", Color.PaleVioletRed);
                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType<VoidReturn>());
            }

            internalAI[1]++;

            if (internalAI[1] >= 300)
            {
                internalAI[1] = 0;
                internalAI[2] = internalAI[3] <= 0 ? Main.rand.Next(3) : Main.rand.Next(2);
                if (internalAI[2] == 2)
                {
                    Main.NewText("initializing repair program", Color.PaleVioletRed);
                }
                npc.ai = new float[4];
                npc.netUpdate = true;
            }
            
            if (internalAI[2] == 1) 
            {
                BaseAI.AIEater(npc, ref npc.ai, 0.05f, 6f, 0, false, true);
                BaseAI.ShootPeriodic(npc, player.position, player.width, player.height, mod.ProjType("DoomLaser"), ref shootAI[0], 5, (int)(npc.damage * (Main.expertMode ? 0.25f : 0.5f)), 10f, true, new Vector2(20f, 15f));
            }
            else if (internalAI[2] == 2) //Shield Mode
            {
                ShieldScale += .02f;
                if (ShieldScale >= 1f)
                {
                    ShieldScale = 1f;
                }
                internalAI[3] = 1000; // To prevent him from doing his shield attack repeatedly
                npc.ai[3]++;
                if (npc.ai[3] > 2)
                {
                    npc.ai[3] = 0;
                    npc.life += 1;
                }
                BaseAI.AISpaceOctopus(npc, ref npc.ai, Main.player[npc.target].Center, 0.07f, 6f, 250f, 70f, null);
            }
            else
            {
                BaseAI.AIEye(npc, ref npc.ai, false, true, .3f, .3f, 6, 4, 0, 0);
                npc.rotation = 0;
            }
            
            if (internalAI[2] != 2f)
            {
                ShieldScale -= .02f;
                if (ShieldScale <= 0f)
                {
                    ShieldScale = 0f;
                }
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


        public override void NPCLoot()
        {
            if (NPC.AnyNPCs(mod.NPCType<Sagittarius>()))
            {
                Item.NewItem(npc.Center, mod.ItemType<Items.Materials.VoidEnergy>(), Main.rand.Next(20, 30));
            }
        }

        public float ShieldScale = 0f;
        public float RingRoatation = 0;

        public override bool PreDraw(SpriteBatch sb, Color dColor)
        {
            Texture2D Shield = mod.GetTexture("NPCs/Bosses/Sagittarius/SagittariusShield");
            Texture2D Ring = mod.GetTexture("NPCs/Bosses/Sagittarius/SagittariusFreeRing");
            Texture2D RingGlow = mod.GetTexture("Glowmasks/SagittariusFreeRing_Glow");
            Texture2D GlowTex = mod.GetTexture("Glowmasks/SagittariusFree_Glow");

            BaseDrawing.DrawTexture(sb, Main.npcTexture[npc.type], 0, npc, dColor);
            BaseDrawing.DrawTexture(sb, GlowTex, 0, npc, AAColor.ZeroShield);

            if (ShieldScale > 0)
            {
                BaseDrawing.DrawTexture(sb, Shield, 0, npc.position, npc.width, npc.height, ShieldScale, 0, 0, 1, new Rectangle(0, 0, Shield.Width, Shield.Height), AAColor.ZeroShield, true);
            }
            BaseDrawing.DrawTexture(sb, Ring, 0, npc.position, npc.width, npc.height, 1, RingRoatation, 0, 1, new Rectangle(0, 0, Ring.Width, Ring.Height), dColor, true);
            BaseDrawing.DrawTexture(sb, RingGlow, 0, npc.position, npc.width, npc.height, 1, RingRoatation, 0, 1, new Rectangle(0, 0, RingGlow.Width, RingGlow.Height), AAColor.ZeroShield, true);
            return false;
        }
    }
}
