using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Greed
{
    public class GreedMinion : ModNPC
	{
        public int MinionType = 0;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ore Construct");
			Main.npcFrameCount[npc.type] = 15;
		}

		public override void SetDefaults()
		{
            npc.lifeMax = 200;
            npc.defense = 20;
            npc.damage = 50;
            npc.width = 26;
            npc.height = 20;
            npc.aiStyle = -1;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0.4f;
            npc.noGravity = true;
        }

        public float[] internalAI = new float[4];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == 2 || Main.dedServ)
            {
                writer.Write(internalAI[0]);
                writer.Write(internalAI[1]);
                writer.Write(internalAI[2]);
                writer.Write(internalAI[3]);
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
        int idleTimer = 0;
        public override void AI()
        {
            Player player = Main.player[npc.target];

            MinionType = (int)npc.ai[0];

            if (npc.ai[2] == 0)
            {
                if (Main.netMode != 1)
                {
                    npc.ai[2]++;
                }
                if (npc.ai[2] > 30)
                {
                    npc.ai[2] = 1;
                    npc.netUpdate = true;
                }
            }
            else
            {
                if (MinionType <= 7 ||
                   MinionType == 9 ||
                   MinionType == 10 ||
                   MinionType == 11 ||
                   MinionType == 12 ||
                   MinionType == 13 ||
                   MinionType == 14 ||
                   MinionType == 15 ||
                   MinionType == 16 ||
                   MinionType == 18 ||
                   MinionType == 19 ||
                   MinionType == 20) //If Earlygame ore, Crimtane, Incinerite, Abyssium, Yttrium, Hellstone, Cobalt, Palladium, Mythril, Adamantite, Titanium, or Uranium, have Melee AI.
                {
                    float Speed = 3;
                    if (MinionType == 11 ||
                   MinionType == 12)
                    {
                        Speed = 5;
                    }
                    BaseAI.AIElemental(npc, ref internalAI, ref idleTimer, null, 120, false, true, 400, 200f, 180, Speed);
                }
                else //if Demonite, Oricalcum, Chlorophite, or Technecium, Ranged AI
                {
                    npc.noTileCollide = true;
                    BaseAI.AISkull(npc, ref internalAI, false, 6, 350, 0.14f, .2f);

                    if (Main.netMode != 1)
                    {
                        int p;
                        if (MinionType == 8) //Demonite
                        {
                            p = ProjectileID.CursedFlameHostile;
                            BaseAI.ShootPeriodic(npc, player.position, player.width, player.height, p, ref npc.ai[1], 120, npc.damage / 2, 9, true);
                        }
                        else if (MinionType == 21) //Chlorophyte
                        {
                            p = ProjectileID.CrystalLeafShot;
                            BaseAI.ShootPeriodic(npc, player.position, player.width, player.height, p, ref npc.ai[1], 120, npc.damage / 2, 9, true);
                        }
                        else if (MinionType == 17 && npc.ai[1]++ > 180) //Oricalcum
                        {
                            npc.ai[1] = 0;
                            int direction = npc.direction;
                            float num = Main.screenPosition.X;
                            if (direction < 0)
                            {
                                num += Main.screenWidth;
                            }
                            float num2 = Main.screenPosition.Y;
                            num2 += Main.rand.Next(Main.screenHeight);
                            Vector2 vector = new Vector2(num, num2);
                            float num3 = npc.position.X - vector.X;
                            float num4 = npc.position.Y - vector.Y;
                            num3 += Main.rand.Next(-50, 51) * 0.1f;
                            num4 += Main.rand.Next(-50, 51) * 0.1f;
                            int num5 = 24;
                            float num6 = (float)Math.Sqrt(num3 * num3 + num4 * num4);
                            num6 = num5 / num6;
                            num3 *= num6;
                            num4 *= num6;
                            int l = Projectile.NewProjectile(num, num2, num3, num4, 221, 36, 0f, Main.myPlayer, 0f, 0f);
                            Main.projectile[l].friendly = false;
                            Main.projectile[l].hostile = true;
                            npc.netUpdate = true;
                        }
                        else //Technecium
                        {
                            p = mod.ProjectileType<TCharge>();
                            BaseAI.ShootPeriodic(npc, player.position, player.width, player.height, p, ref npc.ai[1], 90, npc.damage / 2, 9, true);
                        }
                    }
                }
            }
            if (MinionType == 19) //Titanium Shadow Dodge
            {
                shadowDodge = shadowDodgeTimer > 0;
                if (shadowDodge)
                {
                    if (Main.netMode != 1)
                    {
                        if (!npc.dontTakeDamage)
                        {
                            npc.dontTakeDamage = true;
                            npc.netUpdate = true;
                        }
                        shadowDodgeTimer--;
                    }

                    shadowDodgeCount += 1f;
                    if (shadowDodgeCount > 30f)
                    {
                        shadowDodgeCount = 30f;
                    }
                }
                else
                {
                    if (Main.netMode != 1)
                    {
                        if (npc.dontTakeDamage)
                        {
                            npc.dontTakeDamage = false;
                            npc.netUpdate = true;
                        }
                    }
                    npc.dontTakeDamage = false;
                    shadowDodgeCount -= 1f;
                    if (shadowDodgeCount < 0f)
                    {
                        shadowDodgeCount = 0f;
                    }
                }
            }
            if (MinionType == 9 || MinionType == 15)
            {
                RegenCounter++;

                if (RegenCounter == 300)
                {
                    npc.life++;
                    if (MinionType == 9)
                    {
                        npc.life++;
                    }
                }
            }
            npc.rotation = 0;
        }

        bool shadowDodge = false;
        float shadowDodgeCount = 0;
        int shadowDodgeTimer = 0;
        int RegenCounter = 0;

        public override void HitEffect(int hitDirection, double damage)
        {
            if (MinionType == 9 || MinionType == 15)
            {
                RegenCounter = 0;
            }
        }


        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (MinionType == 13)
            {
                target.AddBuff(BuffID.OnFire, 200);
            }
            if (MinionType == 19 && shadowDodgeTimer <= 0)
            {
                shadowDodgeTimer = 180;
            }
        }

        public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            if (MinionType == 10)
            {
                damage *= .9f;
            }
            return true;
        }

        Color bodyColor;
        Color glowColor;

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (npc.frameCounter >= 10)
            {
                npc.frame.Y += frameHeight;
                if (npc.ai[2] == 0)
                {
                    if (npc.frame.Y > (frameHeight * 3))
                    {
                        npc.frame.Y = frameHeight * 3;
                    }
                }
                else
                {
                    if (npc.frame.Y > (frameHeight * 14) || npc.frame.Y < (frameHeight * 3))
                    {
                        npc.frame.Y = frameHeight * 3;
                    }
                }
                npc.frameCounter = 0;
            }
        }

        public void SetColor()
        {
            switch (MinionType)
            {
                case 0: bodyColor = new Color(150, 67, 22); glowColor = new Color(255, 229, 183); break; //Copper
                case 1: bodyColor = new Color(87, 92, 80); glowColor = new Color(187, 165, 124); break; //Tin
                case 2: bodyColor = new Color(87, 60, 60); glowColor = new Color(189, 159, 139); break; //Iron
                case 3: bodyColor = new Color(47, 62, 87); glowColor = new Color(104, 140, 150); break; //Lead
                case 4: bodyColor = new Color(61, 72, 73); glowColor = new Color(122, 140, 144); break; //Silver
                case 5: bodyColor = new Color(39, 70, 40); glowColor = new Color(154, 190, 155); break; //Tungsten
                case 6: bodyColor = new Color(148, 126, 24); glowColor = new Color(255, 249, 183); break; //Gold
                case 7: bodyColor = new Color(72, 73, 114); glowColor = new Color(190, 222, 222); break; //Platinum
                case 8: bodyColor = new Color(68, 69, 114); glowColor = new Color(120, 117, 179); break; //Demonite
                case 9: bodyColor = new Color(76, 76, 76); glowColor = new Color(216, 59, 63); break; //Crimtane
                case 10: bodyColor = new Color(62, 58, 48); glowColor = new Color(204, 108, 42); break; //Incinerite
                case 11: bodyColor = new Color(52, 36, 88); glowColor = new Color(18, 103, 92); break; //Abyssium
                case 12: bodyColor = new Color(128, 73, 97); glowColor = new Color(228, 155, 102); break; //Yttrium
                case 13: bodyColor = new Color(102, 34, 34); glowColor = new Color(238, 102, 70); break; //Hellstone
                case 14: bodyColor = new Color(4, 48, 111); glowColor = new Color(143, 210, 253); break; //Cobalt
                case 15: bodyColor = new Color(160, 36, 11); glowColor = new Color(245, 95, 55); break; //Palladium
                case 16: bodyColor = new Color(22, 119, 125); glowColor = new Color(212, 255, 190); break; //Mythril
                case 17: bodyColor = new Color(151, 0, 127); glowColor = new Color(248, 113, 227); break; //Oricalcum
                case 18: bodyColor = new Color(128, 26, 52); glowColor = new Color(221, 85, 152); break; //Adamantite
                case 19: bodyColor = new Color(91, 90, 119); glowColor = new Color(190, 187, 220); break; //Titanium
                case 20: bodyColor = new Color(28, 39, 67); glowColor = new Color(92, 157, 103); break; //Uranium
                case 21: bodyColor = new Color(36, 137, 0); glowColor = new Color(234, 254, 126); break; //Chlorophyte
                default: bodyColor = new Color(68, 81, 112); glowColor = new Color(96, 225, 225); break; //Technecium
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            SetColor();
            Texture2D glowTex = mod.GetTexture("Glowmasks/GreedMinion_Glow");
            if (MinionType == 20)
            {
                Texture2D shield = mod.GetTexture("NPCs/Bosses/Greed/UraniumShield");
                BaseDrawing.DrawTexture(spriteBatch, shield, 0, npc.Center, shield.Width, shield.Height, npc.scale, npc.rotation, npc.direction, 1, new Rectangle(0, 0, shield.Width, shield.Height), AAColor.Uranium, true);
            }

            if (shadowDodgeCount > 0f && MinionType == 19)
            {
                Vector2 position;
                position.X = npc.position.X + shadowDodgeCount;
                position.Y = npc.position.Y + npc.gfxOffY;
                BaseDrawing.DrawTexture(spriteBatch, Main.npcTexture[npc.type], 0, position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, 15, npc.frame, new Color(bodyColor.R, bodyColor.G, bodyColor.B, 30), true);
                position.X = npc.position.X - shadowDodgeCount;
                BaseDrawing.DrawTexture(spriteBatch, Main.npcTexture[npc.type], 0, position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, 15, npc.frame, new Color(bodyColor.R, bodyColor.G, bodyColor.B, 30), true);
            }
            BaseDrawing.DrawTexture(spriteBatch, Main.npcTexture[npc.type], 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, 15, npc.frame, glowColor, true);

            BaseDrawing.DrawTexture(spriteBatch, glowTex, 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, 15, npc.frame, glowColor, true);
            return false;
        }

        public void SetStats()
        {
            switch (MinionType)
            {
                case 0:
                    npc.defense = 6; npc.damage = 8; break;
                case 1:
                    npc.defense = 7; npc.damage = 9; break;
                case 2:
                    npc.defense = 9; npc.damage = 10; break;
                case 3:
                    npc.defense = 11; npc.damage = 11; break;
                case 4:
                    npc.defense = 13; npc.damage = 11; break;
                case 5:
                    npc.defense = 15; npc.damage = 12; break;
                case 6:
                    npc.defense = 16; npc.damage = 13; break;
                case 7:
                    npc.defense = 20; npc.damage = 15; break;
                case 8:
                    npc.defense = 19; npc.damage = 17; break;
                case 9:
                    npc.defense = 19; npc.damage = 22; break;
                case 10:
                    npc.defense = 21; npc.damage = 26; break;
                case 11:
                    npc.defense = 15; npc.damage = 14; break;
                case 12:
                    npc.defense = 18; npc.damage = 27; break;
                case 13:
                    npc.defense = 25; npc.damage = 36; break;
                case 14:
                    npc.defense = 16; npc.damage = 40; break;
                case 15:
                    npc.defense = 32; npc.damage = 36; break;
                case 16:
                    npc.defense = 27; npc.damage = 50; break;
                case 17:
                    npc.defense = 42; npc.damage = 47; break;
                case 18:
                    npc.defense = 40; npc.damage = 60; break;
                case 19:
                    npc.defense = 49; npc.damage = 52; break;
                case 20:
                    npc.defense = 53; npc.damage = 60; break;
                case 21:
                    npc.defense = 56; npc.damage = 75; break;
                default:
                    npc.defense = 58; npc.damage = 70; break;
            }
        }
    }
}