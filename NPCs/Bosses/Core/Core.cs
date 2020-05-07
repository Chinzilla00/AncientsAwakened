using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.IO;
using AAMod.NPCs.Enemies.Terrarium.Hardmode;
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
            Vector2 topCenter = origin + Vector16(141, 126);
            Vector2 topLeft = origin + Vector16(107, 130);
            Vector2 topRight = origin + Vector16(175, 130);
            Vector2 BottomLeft = origin + Vector16(114, 152);
            Vector2 BottomRight = origin + Vector16(168, 152);
            Vector2 BottomCenter = origin + Vector16(141, 157);

            #endregion

            #region Preamble

            if (internalAI[0] != 1 || !AAWorld.downedCore)
            {
                npc.position = topCenter;
                if (internalAI[1] % 5 == 0)
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
                if (internalAI[1] == 80)
                {
                    frameShell = 3;
                }
                if (internalAI[1] >= 100)
                {
                    frameShell = 4;
                }

                if (internalAI[1] >= 130)
                {
                    npc.alpha -= 5;
                }

                if (internalAI[1] >= 220 && Main.netMode != 1)
                {
                    internalAI[0]++;
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

                if (npc.ai[0] > 75 && Main.netMode != 1)
                {
                    int pos = Main.rand.Next(6);

                    switch (pos)
                    {
                        case 0:
                            npc.position = topCenter;
                            break;
                        case 1:
                            npc.position = topRight;
                            break;
                        case 2:
                            npc.position = topLeft;
                            break;
                        case 3:
                            npc.position = BottomLeft;
                            break;
                        case 4:
                            npc.position = BottomRight;
                            break;
                        default:
                            npc.position = BottomCenter;
                            break;
                    }
                    npc.ai[0] = 0;
                    npc.ai[1] = 1;
                    npc.netUpdate = true;
                }
                return;
            }
            if (npc.ai[1] == 1) //Changing Attacks
            {
                if (npc.ai[0] % 10 == 0)
                {
                    npc.ai[3] = Main.rand.Next(1, 17);
                }

                int chooseTime = Main.expertMode ? 60 : 90;

                if (npc.ai[0] == chooseTime && Main.netMode != 1)
                {
                    npc.ai[0] = 0;
                    npc.ai[1] = 2;
                    npc.dontTakeDamage = false;
                    npc.netUpdate = true;
                }
                return;
            }

            if (npc.ai[3] > 16)
            {
                npc.ai[3] = 1;
            }

            if (npc.ai[0] % 15 == 0)
            {
                if (frameShell < 4)
                {
                    frameShell += 1;
                }
                else
                {
                    frameShell = 4;
                }
            }

            if (npc.ai[0] > 90)
            {
                switch ((int)npc.ai[3])
                {
                    default:

                        if (npc.ai[0] > 180 && NPC.CountNPCS(ModContent.NPCType<TerraProbe>()) + NPC.CountNPCS(ModContent.NPCType<TerraWatcher>()) < 5 && Main.netMode != 1)
                        {
                            int MinType = Main.rand.Next(2) == 0 ? ModContent.NPCType<TerraProbe>() : ModContent.NPCType<TerraWatcher>();

                            int m = NPC.NewNPC((int)npc.position.X + 100, (int)npc.position.Y, MinType);
                            Main.npc[m].Center = new Vector2(npc.Center.X + 100, npc.Center.Y);

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
                    case 2:

                        if (npc.ai[0] % 198 == 0)
                        {
                            Sandstorm();
                        }

                        break;
                    case 3:

                        break;
                    case 4:

                        break;
                    case 5:

                        break;
                    case 6:

                        break;
                    case 7:

                        break;
                    case 8:

                        break;
                    case 9:

                        break;
                    case 10:

                        break;
                    case 11:

                        break;
                    case 12:
                        int ShootX = 10;
                        int ShootY = 0;

                        if (Main.rand.Next(2) == 0)
                        {
                            ShootY = 10;
                        }

                        if (npc.ai[0] % 20 == 0)
                        {
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, ShootX, ShootY, 270, 50, 1f, Main.myPlayer, -1f, 0f);
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, -ShootX, -ShootY, 270, 50, 1f, Main.myPlayer, -1f, 0f);
                            if (ShootY != 0)
                            {
                                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, ShootX, -ShootY, 270, 50, 1f, Main.myPlayer, -1f, 0f);
                                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, -ShootX, ShootY, 270, 50, 1f, Main.myPlayer, -1f, 0f);
                            }
                        }

                        break;
                    case 13:

                        break;
                    case 14:

                        break;
                    case 15:

                        break;
                    case 16:

                        break;
                }

                int ChangeRate = (npc.life < (int)(npc.lifeMax * .66f)) ? 900 : (npc.life < npc.lifeMax / 3) ? 600 : 1200;

                if (npc.ai[0] > ChangeRate)
                {
                    npc.ai[0] = 0;
                    npc.ai[1] = 0;
                    npc.ai[3] = 0;
                }
            }
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
                Projectile.NewProjectile(current2.X * 16, current2.Y * 16, 0f, 0f, 658, 0, 0f, Main.myPlayer, 0f, 0f);
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
            Vector2 origin = new Vector2((int)(Main.maxTilesX * 0.35f), (int)(Main.maxTilesY * 0.38f));
            if (Main.dungeonX < Main.maxTilesX / 2)
            {
                origin = new Vector2((int)(Main.maxTilesX * 0.65f), (int)(Main.maxTilesY * 0.38f));
            }
            return origin * 16;
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

            Rectangle ShellFrame = BaseDrawing.GetFrame(frameShell, 128, 128, 0, 0);
            Rectangle GlowFrame = BaseDrawing.GetFrame((int)npc.ai[3], 128, 128, 0, 0); ;

            BaseDrawing.DrawTexture(sb, CoreBack, 0, npc.position, npc.width, npc.height, 1, 0, 0, 1, new Rectangle(0, 0, 88, 90), dColor, true);
            BaseDrawing.DrawTexture(sb, Core, 0, npc.position, npc.width, npc.height, 1, 0, 0, 8, npc.frame, npc.GetAlpha(GlowColor()), true);
            BaseDrawing.DrawTexture(sb, CoreShell, 0, npc.position, npc.width, npc.height, 1, 0, 5, 1, ShellFrame, dColor, true);
            BaseDrawing.DrawTexture(sb, Glow, 0, npc.position, npc.width, npc.height, 1, npc.rotation, 0, 16, GlowFrame, Color.White, true);
            
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
                    return Color.Purple;
                case 4:
                    return Color.LightGreen;
                case 5:
                    return Color.OrangeRed;
                case 6:
                    return Color.LightBlue;
                case 7:
                    return Color.Orange;
                case 8:
                    return Color.Brown;
                case 9:
                    return new Color(30, 30, 30);
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
                    return Color.Pink;
                case 16:
                    return Color.SkyBlue;
                default:
                    return Color.Green;
            }
        }
    }
}
