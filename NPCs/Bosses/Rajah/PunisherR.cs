using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.NPCs.Bosses.Rajah
{
    public class PunisherR : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Doom");
            Main.npcFrameCount[npc.type] = 8;
        }

        public override void SetDefaults()
        {
            npc.width = 42;
            npc.height = 34;
            npc.aiStyle = -1;
            npc.damage = 100;
            npc.lifeMax = 1;
            npc.dontTakeDamage = true;
            npc.knockBackResist = 0f;
            npc.npcSlots = 0f;
            npc.netAlways = true;
            npc.noGravity = false;
            music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/RajahTheme");
        }

        public override void AI()
        {
            int Rabbit = AAModGlobalNPC.Rajah;
            NPC Rajah = Main.npc[Rabbit];
            if (Rabbit < 0)
            {
                npc.active = false;
                return;
            }
            if (Rajah.ai[3] != 2)
            {
                npc.ai[1] = -1;
                npc.alpha = 255;
            }
            else
            {
                if (npc.alpha > 0)
                {
                    npc.alpha -= 10;
                    if (npc.alpha < 0)
                    {
                        npc.alpha = 0;
                    }
                    npc.ai[1] = 0f;
                }
            }
            if (npc.ai[0] == 0f)
            {
                npc.noTileCollide = true;
                float num659 = 14f;
                if (npc.life < npc.lifeMax / 2)
                {
                    num659 += 3f;
                }
                if (npc.life < npc.lifeMax / 4)
                {
                    num659 += 3f;
                }
                if (Rajah.life < Rajah.lifeMax)
                {
                    num659 += 8f;
                }
                Vector2 vector79 = new Vector2(npc.Center.X, npc.Center.Y);
                float num660 = Rajah.Center.X - vector79.X;
                float num661 = Rajah.Center.Y - vector79.Y;
                num661 -= 9f;
                num660 += 78f;
                float num662 = (float)Math.Sqrt(num660 * num660 + num661 * num661);
                if (num662 < 12f + num659)
                {
                    npc.rotation = 0f;
                    npc.velocity.X = num660;
                    npc.velocity.Y = num661;
                    npc.ai[1] += 1f;
                    if (npc.life < npc.lifeMax / 2)
                    {
                        npc.ai[1] += 1f;
                    }
                    if (npc.life < npc.lifeMax / 4)
                    {
                        npc.ai[1] += 1f;
                    }
                    if (Rajah.life < Rajah.lifeMax)
                    {
                        npc.ai[1] += 10f;
                    }
                    if (npc.ai[1] >= 60f)
                    {
                        npc.TargetClosest(true);
                        if ((npc.Center.X - 100f < Main.player[npc.target].Center.X))
                        {
                            npc.ai[1] = 0f;
                            npc.ai[0] = 1f;
                            return;
                        }
                        npc.ai[1] = 0f;
                        return;
                    }
                }
                else
                {
                    num662 = num659 / num662;
                    npc.velocity.X = num660 * num662;
                    npc.velocity.Y = num661 * num662;
                    npc.rotation = (float)Math.Atan2(-npc.velocity.Y, npc.velocity.X);
                }
            }
            else if (npc.ai[0] == 1f)
            {
                npc.noTileCollide = true;
                npc.collideX = false;
                npc.collideY = false;
                float num663 = 12f;
                if (npc.life < npc.lifeMax / 2)
                {
                    num663 += 4f;
                }
                if (npc.life < npc.lifeMax / 4)
                {
                    num663 += 4f;
                }
                if (Rajah.life < Rajah.lifeMax)
                {
                    num663 += 10f;
                }
                Vector2 vector80 = new Vector2(npc.Center.X, npc.Center.Y);
                float num664 = Main.player[npc.target].Center.X - vector80.X;
                float num665 = Main.player[npc.target].Center.Y - vector80.Y;
                float num666 = (float)Math.Sqrt(num664 * num664 + num665 * num665);
                num666 = num663 / num666;
                npc.velocity.X = num664 * num666;
                npc.velocity.Y = num665 * num666;
                npc.ai[0] = 2f;
                npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X);
            }
            else if (npc.ai[0] == 2f)
            {
                if (Math.Abs(npc.velocity.X) > Math.Abs(npc.velocity.Y))
                {
                    if (npc.velocity.X > 0f && npc.Center.X > Main.player[npc.target].Center.X)
                    {
                        npc.noTileCollide = false;
                    }
                    if (npc.velocity.X < 0f && npc.Center.X < Main.player[npc.target].Center.X)
                    {
                        npc.noTileCollide = false;
                    }
                }
                else
                {
                    if (npc.velocity.Y > 0f && npc.Center.Y > Main.player[npc.target].Center.Y)
                    {
                        npc.noTileCollide = false;
                    }
                    if (npc.velocity.Y < 0f && npc.Center.Y < Main.player[npc.target].Center.Y)
                    {
                        npc.noTileCollide = false;
                    }
                }
                Vector2 vector81 = new Vector2(npc.Center.X, npc.Center.Y);
                float num667 = Rajah.Center.X - vector81.X;
                float num668 = Rajah.Center.Y - vector81.Y;
                num667 += Rajah.velocity.X;
                num668 += Rajah.velocity.Y;
                num668 -= 9f;
                num667 += 78f;
                float num669 = (float)Math.Sqrt(num667 * num667 + num668 * num668);
                if (Rajah.life < Rajah.lifeMax)
                {
                    npc.knockBackResist = 0f;
                    if (num669 > 700f || npc.collideX || npc.collideY)
                    {
                        npc.noTileCollide = true;
                        npc.ai[0] = 0f;
                        return;
                    }
                }
                else
                {
                    bool flag43 = npc.justHit;
                    if (flag43)
                    {
                        int num670 = 0;
                        while (num670 < 200)
                        {
                            num670++;
                        }
                    }
                    if (num669 > 600f || npc.collideX || npc.collideY || flag43)
                    {
                        npc.noTileCollide = true;
                        npc.ai[0] = 0f;
                        return;
                    }
                }
            }
            else if (npc.ai[0] == 3f)
            {
                npc.noTileCollide = true;
                float num671 = 12f;
                float num672 = 0.4f;
                Vector2 vector82 = new Vector2(npc.Center.X, npc.Center.Y);
                float num673 = Main.player[npc.target].Center.X - vector82.X;
                float num674 = Main.player[npc.target].Center.Y - vector82.Y;
                float num675 = (float)Math.Sqrt((num673 * num673 + num674 * num674));
                num675 = num671 / num675;
                num673 *= num675;
                num674 *= num675;
                if (npc.velocity.X < num673)
                {
                    npc.velocity.X = npc.velocity.X + num672;
                    if (npc.velocity.X < 0f && num673 > 0f)
                    {
                        npc.velocity.X = npc.velocity.X + num672 * 2f;
                    }
                }
                else if (npc.velocity.X > num673)
                {
                    npc.velocity.X = npc.velocity.X - num672;
                    if (npc.velocity.X > 0f && num673 < 0f)
                    {
                        npc.velocity.X = npc.velocity.X - num672 * 2f;
                    }
                }
                if (npc.velocity.Y < num674)
                {
                    npc.velocity.Y = npc.velocity.Y + num672;
                    if (npc.velocity.Y < 0f && num674 > 0f)
                    {
                        npc.velocity.Y = npc.velocity.Y + num672 * 2f;
                    }
                }
                else if (npc.velocity.Y > num674)
                {
                    npc.velocity.Y = npc.velocity.Y - num672;
                    if (npc.velocity.Y > 0f && num674 < 0f)
                    {
                        npc.velocity.Y = npc.velocity.Y - num672 * 2f;
                    }
                }
                npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X);
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            int Rabbit = AAModGlobalNPC.Rajah;
            NPC Rajah = Main.npc[Rabbit];
            if (Rajah.ai[3] != 2)
            {
                return false;
            }
            Vector2 vector6 = new Vector2(npc.Center.X, npc.Center.Y);
            float num19 = Rajah.Center.X - vector6.X;
            float num20 = Rajah.Center.Y - vector6.Y;
            num20 -= 7f;
            num19 += 66f;
            float rotation6 = (float)Math.Atan2(num20, num19) - 1.57f;
            bool flag6 = true;
            while (flag6)
            {
                float num21 = (float)Math.Sqrt((num19 * num19 + num20 * num20));
                if (num21 < 16f)
                {
                    flag6 = false;
                }
                else
                {
                    num21 = 16f / num21;
                    num19 *= num21;
                    num20 *= num21;
                    vector6.X += num19;
                    vector6.Y += num20;
                    num19 = Rajah.Center.X - vector6.X;
                    num20 = Rajah.Center.Y - vector6.Y;
                    num20 -= 7f;
                    num19 += 66f;
                    Color color6 = Lighting.GetColor((int)vector6.X / 16, (int)(vector6.Y / 16f));
                    Texture2D Chain = mod.GetTexture("NPCs/Bosses/Rajah/PunisherR_Chain");
                    Main.spriteBatch.Draw(Chain, new Vector2(vector6.X - Main.screenPosition.X, vector6.Y - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, 0, Chain.Width, Chain.Height)), color6, rotation6, new Vector2(Chain.Width * 0.5f, Chain.Height * 0.5f), 1f, SpriteEffects.None, 0f);
                }
            }
            return true;
        }
    }
}