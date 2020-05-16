using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.IO;
using AAMod.NPCs.Enemies.Terrarium.Hardmode;
using AAMod.NPCs.Bosses.Core.Projectiles;
using System.Collections.Generic;

namespace AAMod.NPCs.Bosses.Core
{
    [AutoloadBossHead]
    public class Core : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Biomite Core");
            Main.npcFrameCount[npc.type] = 8;
		}

		public override void SetDefaults()
        {
            npc.lifeMax = 6000;
            npc.boss = true;
            npc.defense = 0;
            npc.damage = 40;
            npc.width = 74;
            npc.height = 70;
            npc.aiStyle = -1;
            npc.value = Item.sellPrice(0, 16, 0, 0);
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Core");
            npc.knockBackResist = 0f;
            npc.noGravity = true;
            bossBag = mod.ItemType("CoreBag");
            npc.alpha = 255;
        }

        public float[] internalAI = new float[4];
        
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
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
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                internalAI[0] = reader.ReadFloat();
                internalAI[1] = reader.ReadFloat();
                internalAI[2] = reader.ReadFloat();
                internalAI[3] = reader.ReadFloat();
            }
        }

        public override void AI()
        {
            #region Points

            Vector2 origin = Origin();
            Vector2 topCenter = origin + Vector16(140, 125) + Vector16(5, 4);
            Vector2 topLeft = origin + Vector16(106, 129) + Vector16(5, 4);
            Vector2 topRight = origin + Vector16(174, 129) + Vector16(5, 4);
            Vector2 BottomLeft = origin + Vector16(113, 151) + Vector16(5, 4);
            Vector2 BottomRight = origin + Vector16(167, 151) + Vector16(5, 4);
            Vector2 BottomCenter = origin + Vector16(140, 156) + Vector16(5, 4);

            #endregion

            if (npc.ai[3] > 16)
            {
                npc.ai[3] = 1;
            }

            #region Preamble

            if (internalAI[0] != 1 && !AAWorld.downedCore)
            {
                npc.dontTakeDamage = true;
                npc.Center = topCenter;
                if (internalAI[1] % 10 == 0)
                {
                    npc.ai[3] += 1;
                }

                internalAI[1]++;

                if (internalAI[1] < 40)
                {
                    frameShell = 0;
                }
                if (internalAI[1] == 40)
                {
                    frameShell = 1;
                }
                if (internalAI[1] == 60)
                {
                    frameShell = 2;
                }
                if (internalAI[1] >= 80)
                {
                    frameShell = 3;
                }

                if (internalAI[1] >= 130)
                {
                    npc.alpha -= 5;
                }

                if (internalAI[1] >= 220 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    internalAI[0]++;
                    npc.dontTakeDamage = false;
                    npc.netUpdate = true;
                }
                return;
            }
            #endregion

            if (!npc.HasPlayerTarget)
            {
                npc.TargetClosest();
            }

            Player player = Main.player[npc.target];

            if (player.dead || !player.active || (npc.position.X - Main.player[npc.target].position.X) > 6000f || (npc.position.X - Main.player[npc.target].position.X) < -6000f || (npc.position.Y - Main.player[npc.target].position.Y) > 6000f || (npc.position.Y - Main.player[npc.target].position.Y) < -6000f)
            {
                npc.TargetClosest(true);
                player = Main.player[npc.target];

                if (player.dead || !player.active || (npc.position.X - Main.player[npc.target].position.X) > 6000f || (npc.position.X - Main.player[npc.target].position.X) < -6000f || (npc.position.Y - Main.player[npc.target].position.Y) > 6000f || (npc.position.Y - Main.player[npc.target].position.Y) < -6000f)
                {
                    Item.NewItem(new Vector2(origin.X + 144, origin.Y + 134), ModContent.ItemType<Items.Materials.TerraCrystal>(), 1);
                    npc.active = false;
                }
            }

            npc.ai[0]++;

            if (npc.ai[1] == 0) //Changing Positions
            {
                npc.dontTakeDamage = true;

                if (npc.ai[0] % 15 == 0)
                {
                    if (frameShell > 0)
                    {
                        frameShell -= 1;
                    }
                    else
                    {
                        frameShell = 0;
                    }
                }

                if (npc.ai[0] > 75 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    int pos = Main.rand.Next(6);

                    Vector2 CurrentPos = npc.Center;
                    Vector2 newPos = CurrentPos;

                    while (newPos == CurrentPos)
                    {
                        switch (pos)
                        {
                            case 0:
                                newPos = topCenter;
                                break;
                            case 1:
                                newPos = topRight;
                                break;
                            case 2:
                                newPos = topLeft;
                                break;
                            case 3:
                                newPos = BottomLeft;
                                break;
                            case 4:
                                newPos = BottomRight;
                                break;
                            default:
                                newPos = BottomCenter;
                                break;
                        }
                    }

                    npc.Center = newPos;

                    npc.ai[0] = 0;
                    npc.ai[1] = 1;
                    npc.ai[3] = Main.rand.Next(1, 17);
                    npc.netUpdate = true;
                }
                return;
            }
            if (npc.ai[1] == 1) //Changing Attacks
            {
                if (npc.ai[0] % 15 == 0)
                {
                    npc.ai[3]++;
                }

                int chooseTime = Main.expertMode ? 60 : 90;

                if (npc.ai[0] == chooseTime && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    npc.ai[0] = 0;
                    npc.ai[1] = 2;
                    npc.dontTakeDamage = false;
                    npc.netUpdate = true;
                }
                return;
            }

            if (npc.ai[0] % 15 == 0)
            {
                if (frameShell < 3)
                {
                    frameShell += 1;
                }
                else
                {
                    frameShell = 3;
                }
            }

            if (npc.ai[0] > 90)
            {
                switch ((int)npc.ai[3])
                {
                    default: //Terra

                        if (npc.ai[0] > 180 && NPC.CountNPCS(ModContent.NPCType<TerraProbe>()) + NPC.CountNPCS(ModContent.NPCType<TerraWatcher>()) < 5 && Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            int MinType = Main.rand.Next(2) == 0 ? ModContent.NPCType<TerraProbe>() : ModContent.NPCType<TerraWatcher>();

                            int m = NPC.NewNPC((int)npc.position.X + 100, (int)npc.position.Y, MinType);
                            Main.npc[m].Center = new Vector2(npc.Center.X + 100, npc.Center.Y);

                            MinType = Main.rand.Next(2) == 0 ? ModContent.NPCType<TerraProbe>() : ModContent.NPCType<TerraWatcher>();

                            int n = NPC.NewNPC((int)npc.position.X - 100, (int)npc.position.Y, MinType);
                            Main.npc[n].Center = new Vector2(npc.Center.X - 100, npc.Center.Y);

                            if (npc.life < npc.lifeMax / 2)
                            {
                                int o = NPC.NewNPC((int)npc.position.X, (int)npc.position.Y + 100, MinType);
                                Main.npc[o].Center = new Vector2(npc.Center.X, npc.Center.Y + 100);
                            }
                            npc.ai[0] = 0;
                            npc.ai[1] = 0;
                            npc.ai[3] = 0;
                        }

                        break;
                    case 2: //Desert

                        if (npc.ai[0] % 198 == 0)
                        {
                            Sandstorm();
                        }

                        break;
                    case 3: //Corruption

                        if (npc.ai[0] % 91 == 0)
                        {
                            for(int i = 0; i < 8; i++)
                            {
                                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 5f * (float)Math.Sin(i * (Math.PI / 4)), 5f * (float)Math.Cos(i * (Math.PI / 4)), ProjectileID.CursedFlameHostile, 50, 1f, Main.myPlayer, -1f, 0f);
                            }
                        }

                        break;
                    case 4: //Jungle

                        break;
                    case 5: //Inferno

                        npc.rotation += .01f;

                        Vector2 Speed = npc.rotation.ToRotationVector2();

                        if (npc.ai[0] % 6 == 0)
                        {
                            Main.PlaySound(SoundID.Item34, npc.position);

                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, Speed.X * 5, Speed.Y , ModContent.ProjectileType<InfernoBreath>(), 20, 0, Main.myPlayer, 0f, 0f);
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, -Speed.X, -Speed.Y, ModContent.ProjectileType<InfernoBreath>(), 20, 0, Main.myPlayer, 0f, 0f);
                        }

                        break;
                    case 6: //Glowing Mushroom

                        break;
                    case 7: //Hell

                        if (npc.ai[0] % 60 == 0)
                        {
                            int velY = Main.rand.Next(3, 7);
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 5, velY, ModContent.ProjectileType<HellFireball>(), 50, 1f, Main.myPlayer, -1f, 0f);
                            velY = Main.rand.Next(3, 7);
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, -5, -velY, ModContent.ProjectileType<HellFireball>(), 50, 1f, Main.myPlayer, -1f, 0f);
                            velY = Main.rand.Next(3, 7);
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 5, -velY, ModContent.ProjectileType<HellFireball>(), 50, 1f, Main.myPlayer, -1f, 0f);
                            velY = Main.rand.Next(3, 7);
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, -5, velY, ModContent.ProjectileType<HellFireball>(), 50, 1f, Main.myPlayer, -1f, 0f);
                        }

                        break;
                    case 8: //Cavern

                        if (npc.ai[0] == 91)
                        {
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0, 0, ModContent.ProjectileType<RockFall>(), 40, 0, Main.myPlayer, npc.whoAmI);
                        }

                        break;
                    case 9: //Void

                        break;
                    case 10: //Snow

                        if (npc.ai[0] % 198 == 0)
                        {
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0, 0, ModContent.ProjectileType<SnowCloud>(), 40, 0, Main.myPlayer, player.Center.X, player.Center.Y - Main.rand.Next(100, 150));
                        }

                        break;
                    case 11: //Crimson

                        if (npc.ai[0] % 61 == 0)
                        {
                            for (int i = 0; i < 6; i++)
                            {
                                Vector2 speed = Main.player[npc.target].Center - npc.Center;
                                speed.Y -= Math.Abs(speed.X) * 0.2f;
                                speed.Normalize();
                                speed *= 8f;
                                speed += npc.velocity / 3f;
                                speed.X += Main.rand.Next(-20, 21) * 0.08f;
                                speed.Y += Main.rand.Next(-20, 21) * 0.08f;
                                Projectile.NewProjectile(npc.Center, speed, ProjectileID.GoldenShowerHostile, 40, 0f, Main.myPlayer);
                            }
                        }
                        break;
                    case 12: //Dungeon

                        if (npc.ai[0] % 20 == 0)
                        {
                            int ShootX = 6;
                            int ShootY = 0;

                            if (Main.rand.Next(2) == 0)
                            {
                                ShootY = 6;
                            }

                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, ShootX, ShootY, ProjectileID.Skull, 50, 1f, Main.myPlayer, -1f, 0f);
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, -ShootX, -ShootY, ProjectileID.Skull, 50, 1f, Main.myPlayer, -1f, 0f);
                            if (ShootY != 0)
                            {
                                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, ShootX, -ShootY, ProjectileID.Skull, 50, 1f, Main.myPlayer, -1f, 0f);
                                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, -ShootX, ShootY, ProjectileID.Skull, 50, 1f, Main.myPlayer, -1f, 0f);
                            }
                        }

                        break;
                    case 13: //Mire

                        break;
                    case 14: //Ocean

                        break;
                    case 15: //Hallow

                        if (npc.ai[0] % 120 == 0)
                        {
                            int x = 6;
                            int y = 0;

                            if (Main.rand.Next(2) == 0)
                            {
                                y = 6;
                            }

                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, x, y, ModContent.ProjectileType<Rainbow>(), 50, 1f, Main.myPlayer, -1f, 0f);
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, -x, -y, ModContent.ProjectileType<Rainbow>(), 50, 1f, Main.myPlayer, -1f, 0f);
                            if (y != 0)
                            {
                                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, x, -y, ModContent.ProjectileType<Rainbow>(), 50, 1f, Main.myPlayer, -1f, 0f);
                                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, -x, y, ModContent.ProjectileType<Rainbow>(), 50, 1f, Main.myPlayer, -1f, 0f);
                            }
                        }

                        break;
                    case 16: //Sky

                        if (npc.ai[0] % 60f == 0f && Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            int[] array4 = new int[5];
                            Vector2[] array5 = new Vector2[5];
                            int num838 = 0;
                            float num839 = 2000f;
                            for (int num840 = 0; num840 < 255; num840++)
                            {
                                if (Main.player[num840].active && !Main.player[num840].dead)
                                {
                                    Vector2 center9 = Main.player[num840].Center;
                                    float num841 = Vector2.Distance(center9, npc.Center);
                                    if (num841 < num839 && Collision.CanHit(npc.Center, 1, 1, center9, 1, 1))
                                    {
                                        array4[num838] = num840;
                                        array5[num838] = center9;
                                        if (++num838 >= array5.Length)
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                            for (int num842 = 0; num842 < num838; num842++)
                            {
                                Vector2 vector82 = array5[num842] - npc.Center;
                                float ai = Main.rand.Next(100);
                                Vector2 vector83 = Vector2.Normalize(vector82.RotatedByRandom(0.78539818525314331)) * 14f;
                                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, vector83.X, vector83.Y, ModContent.ProjectileType<Athena.Olympian.AthenaShock>(), npc.damage, 0f, Main.myPlayer, vector82.ToRotation(), ai);
                            }
                        }

                        break;
                }

                int ChangeRate = (npc.life < (int)(npc.lifeMax * .66f)) ? 300 : (npc.life < npc.lifeMax / 3) ? 450 : 600;

                if (npc.ai[0] > ChangeRate)
                {
                    npc.ai[0] = 0;
                    npc.ai[1] = 0;
                }
            }

            npc.direction = npc.spriteDirection = 1;
        }

        public void Sandstorm()
        {
            List<Point> list4 = new List<Point>();
            Vector2 vec5 = Main.player[npc.target].Center + new Vector2(Main.player[npc.target].velocity.X * 30f, 0f);
            Point point14 = vec5.ToTileCoordinates();
            int num1468 = 0;
            while (num1468 < 1000 && list4.Count < 3)
            {
                bool flag118 = false;
                int num1469 = Main.rand.Next(point14.X - 30, point14.X + 30 + 1);
                foreach (Point current in list4)
                {
                    if (Math.Abs(current.X - num1469) < 10)
                    {
                        flag118 = true;
                        break;
                    }
                }
                if (!flag118)
                {
                    int startY = point14.Y - 20;
                    Collision.ExpandVertically(num1469, startY, out int num1470, out int num1471, 1, 51);
                    if (StrayMethods.CanSpawnSandstormHostile(new Vector2(num1469, num1471 - 15) * 16f, 15, 15))
                    {
                        list4.Add(new Point(num1469, num1471 - 15));
                    }
                }
                num1468++;
            }
            foreach (Point current2 in list4)
            {
                Projectile.NewProjectile(current2.X * 16, current2.Y * 16, 0f, 0f, ModContent.ProjectileType<SandstormProj>(), 0, 0f, Main.myPlayer, 0f, 0f);
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                int dust1 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 107, 0f, 0f, 0);
                Main.dust[dust1].velocity *= 0.5f;
                Main.dust[dust1].scale *= 1.3f;
                Main.dust[dust1].fadeIn = 1f;
                Main.dust[dust1].noGravity = false;
            }
        }

        public override void NPCLoot()
        {
            AAWorld.downedSag = true;
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("CoreTrophy"));
            }
            if (!Main.expertMode)
            {
                if (Main.rand.Next(7) == 0)
                {
                    npc.DropLoot(mod.ItemType("CoreMask"));
                }
                string[] lootTable = {  };
                int loot = Main.rand.Next(lootTable.Length);
                npc.DropLoot(mod.ItemType(lootTable[loot]));
                Item.NewItem(npc.Center, ModContent.ItemType<Items.Materials.TerraCrystal>(), Main.rand.Next(1, 4));
            }
            else
            {
                npc.DropBossBags();
            }
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (npc.frameCounter > 10)
            {
                npc.frameCounter = 0;
                npc.frame.Y += frameHeight;
                if (npc.frame.Y > frameHeight * 7)
                {
                    npc.frame.Y = 0;
                }
            }
        }

        public Vector2 Origin()
        {
            Point origin = new Point((int)(Main.maxTilesX * 0.35f), (int)(Main.maxTilesY * 0.38f));
            if (Main.dungeonX < Main.maxTilesX / 2)
            {
                origin = new Point((int)(Main.maxTilesX * 0.65f), (int)(Main.maxTilesY * 0.38f));
            }
            return new Vector2(origin.X * 16f, origin.Y * 16f);
        }

        public Vector2 Vector16(int x, int y)
        {
            return new Vector2(x * 16, y * 16);
        }

        public int frameShell = 0;

        public float RingRoatation = 0;

        public override bool PreDraw(SpriteBatch sb, Color dColor)
        {
            Texture2D CoreBack = mod.GetTexture("NPCs/Bosses/Core/CoreBack");
            Texture2D Core = Main.npcTexture[npc.type];
            Texture2D CoreShell = mod.GetTexture("NPCs/Bosses/Core/CoreShell");
            Texture2D Glow = mod.GetTexture("NPCs/Bosses/Core/CoreGlow");

            Rectangle ShellFrame = BaseDrawing.GetFrame(frameShell, 156, 128, 0, 0);
            Rectangle GlowFrame = BaseDrawing.GetFrame((int)npc.ai[3] - 1, 156, 128, 0, 0); 
            Rectangle CoreBackSprite = BaseDrawing.GetFrame(0, 156, 128, 0, 0); ;

            BaseDrawing.DrawTexture(sb, CoreBack, 0, npc.position, npc.width, npc.height, 1, 0, 0, 1, CoreBackSprite, dColor, true);
            BaseDrawing.DrawTexture(sb, Core, 0, npc.position, npc.width, npc.height, 1, 0, 0, 8, npc.frame, npc.GetAlpha(GlowColor()), true);
            BaseDrawing.DrawTexture(sb, CoreShell, 0, npc.position, npc.width, npc.height, 1, 0, 1, 4, ShellFrame, dColor, true);
            BaseDrawing.DrawTexture(sb, Glow, 0, npc.position, npc.width, npc.height, 1,0, 0, 16, GlowFrame, Color.White, true);
            
            return false;
        }

        public Color GlowColor()
        {
            switch ((int)npc.ai[3])
            {
                case 1:
                    return Color.Green;
                case 2:
                    return Color.Yellow;
                case 3:
                    return new Color(104, 90, 144);
                case 4:
                    return Color.LightGreen;
                case 5:
                    return Color.OrangeRed;
                case 6:
                    return Color.MediumSlateBlue;
                case 7:
                    return Color.DarkOrange;
                case 8:
                    return Color.Sienna;
                case 9:
                    return new Color(50, 50, 60);
                case 10:
                    return Color.White;
                case 11:
                    return Color.Red;
                case 12:
                    return Color.DarkSlateBlue;
                case 13:
                    return Color.Indigo;
                case 14:
                    return Color.Blue;
                case 15:
                    return Color.Fuchsia;
                case 16:
                    return Color.DeepSkyBlue;
                default:
                    return Color.Green;
            }
        }
    }
}
