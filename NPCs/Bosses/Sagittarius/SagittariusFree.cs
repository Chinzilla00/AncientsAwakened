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
    public class SagittariusFree : Sagittarius
	{
        public int damage = 0;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sagittarius-A");
            Main.npcFrameCount[npc.type] = 5;
		}

		public override void SetDefaults()
        {
            npc.lifeMax = 6000;
            npc.boss = true;
            npc.defense = 0;
            npc.damage = 70;
            npc.width = 74;
            npc.height = 70;
            npc.aiStyle = -1;
            npc.value = Item.sellPrice(0, 8, 0, 0);
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Sagittarius");
            npc.knockBackResist = 0f;
            npc.noGravity = true;
            npc.alpha = 255;
            bossBag = mod.ItemType("SagBag");
        }
        
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
                internalAI[4] = reader.ReadFloat();
                shootAI[0] = reader.ReadFloat();
            }
        }
       
        public override void AI()
        {
            if (Main.expertMode)
            {
                damage = npc.damage / 4;
            }
            else
            {
                damage = npc.damage / 2;
            }
            npc.noGravity = true;
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];
            AAPlayer modplayer = player.GetModPlayer<AAPlayer>(mod);
            RingRoatation += .05f;
            npc.ai[1]++;
            if (internalAI[3] > 0)
            {
                internalAI[3]--;
            }

            npc.frameCounter++;
            if (npc.frameCounter > 7)
            {
                npc.frame.Y += 72;
                npc.frameCounter = 0;
                if (npc.frame.Y > 72 * 4)
                {
                    npc.frame.Y = 0;
                }
            }

            if (Main.player[npc.target].dead && Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 5000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 5000f)
            {
                npc.TargetClosest(true);
                if (Main.player[npc.target].dead)
                {
                    if (internalAI[0] != 1f)
                    {
                        if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("SagittariusFree1"), Color.PaleVioletRed);
                        internalAI[0] = 1f;
                    }
                }
                if (Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 5000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 5000f)
                {
                    if (internalAI[0] != 1f)
                    {
                        if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("SagittariusFree2"), Color.PaleVioletRed);
                        internalAI[0] = 1f;
                    }
                }
            }
            if (internalAI[0] == 1f)
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

            internalAI[1]++;

            if (internalAI[2] == 2)
            {
                npc.dontTakeDamage = true;
            }
            else
            {
                npc.dontTakeDamage = false;
            }

            if (internalAI[1] >= 300)
            {
                internalAI[1] = 0;
                internalAI[2] = internalAI[3] <= 0 ? Main.rand.Next(3) : Main.rand.Next(2);
                internalAI[4] = Main.rand.Next(2);
                if (internalAI[2] == 2)
                {
                    if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("SagittariusFree3"), Color.PaleVioletRed);
                }
                npc.ai = new float[4];
                npc.netUpdate = true;
            }
            
            if (internalAI[2] == 1) 
            {
                BaseAI.AIEater(npc, ref npc.ai, 0.05f, 4f, 0, false, true);
                npc.rotation = 0;
                BaseAI.ShootPeriodic(npc, player.position, player.width, player.height, mod.ProjType("SagProj"), ref shootAI[0], 15, damage, 10f, true, new Vector2(20f, 15f));
            }
            else if (internalAI[2] == 2) //Shield Mode
            {
                ShieldScale += .02f;
                if (ShieldScale >= 1f)
                {
                    ShieldScale = 1f;
                }
                internalAI[3] = 1200;
                npc.life += 2;
                BaseAI.AISpaceOctopus(npc, ref npc.ai, Main.player[npc.target].Center, 0.07f, 5f, 250f, 70f, ShootLaser);
            }
            else
            {
                BaseAI.AIEye(npc, ref npc.ai, false, true, .3f, .3f, 4, 4, 0, 0);
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
            bool foundLocation = false;
            if (internalAI[4] == 1)
            {
                npc.alpha += 5;

                int Xint = Main.rand.Next(-500, 500);
                int Yint = Main.rand.Next(-500, 500);

                if (npc.alpha >= 255)
                {
                    npc.alpha = 255;
                    if ((Xint < -100 || Xint > 100) && (Yint < -90 || Yint > 90))
                    {
                        foundLocation = true;
                    }
                }
                if (foundLocation)
                {
                    Vector2 tele = new Vector2(player.Center.X + Xint, player.Center.Y + Yint);
                    npc.Center = tele;
                    internalAI[4] = 0;
                }
            }
            else
            {
                npc.alpha -= 3;
                if (npc.alpha <= 0)
                {
                    npc.alpha = 0;
                }
            }

            npc.noTileCollide = true;
        }

        public void ShootLaser(NPC npc, Vector2 velocity)
        {
            float spread = 45f * 0.0174f;
            float baseSpeed = (float)Math.Sqrt(200);
            double startAngle = Math.Atan2(10, 10) - .1d;
            double deltaAngle = spread / 6f;
            double offsetAngle;
            Player player = Main.player[npc.target];
            shootAI[0]++;
            if (shootAI[0] >= 10)
            {
                shootAI[0] = 0;
                for (int i = 0; i < Main.rand.Next(1, 3); i++)
                {
                    offsetAngle = startAngle + (deltaAngle * i);
                    Projectile.NewProjectile(player.position.X, player.position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), mod.ProjectileType<Zero.DeathLaser>(), damage, 2, Main.myPlayer);
                }
            }
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
                Item.NewItem(npc.Center, mod.ItemType<Items.Materials.Doomite>(), Main.rand.Next(20, 30));
            }
            else
            {
                npc.DropBossBags();
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

            BaseDrawing.DrawTexture(sb, Main.npcTexture[npc.type], 0, npc, dColor, true);
            BaseDrawing.DrawTexture(sb, GlowTex, 0, npc, ColorUtils.COLOR_GLOWPULSE, true);

            if (ShieldScale > 0)
            {
                BaseDrawing.DrawTexture(sb, Shield, 0, npc.position, npc.width, npc.height, ShieldScale, 0, 0, 1, new Rectangle(0, 0, Shield.Width, Shield.Height), AAColor.ZeroShield, true);
            }
            BaseDrawing.DrawTexture(sb, Ring, 0, npc.position, npc.width, npc.height, 1, RingRoatation, 0, 1, new Rectangle(0, 0, Ring.Width, Ring.Height), dColor, true);
            BaseDrawing.DrawTexture(sb, RingGlow, 0, npc.position, npc.width, npc.height, 1, RingRoatation, 0, 1, new Rectangle(0, 0, RingGlow.Width, RingGlow.Height), ColorUtils.COLOR_GLOWPULSE, true);
            return false;
        }
    }
}
