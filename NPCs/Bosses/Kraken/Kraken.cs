using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Kraken
{
    public class Kraken : ModNPC
    {
        public NPC Tentacle1;
        public NPC Tentacle2;
        public NPC Tentacle3;
        public NPC Tentacle4;
        public NPC Tentacle5;
        public NPC Tentacle6;


        public float[] customAI = new float[4];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if ((Main.netMode == 2 || Main.dedServ))
            {
                writer.Write((short)customAI[0]);
                writer.Write((short)customAI[1]);
                writer.Write((short)customAI[2]);
                writer.Write((short)customAI[3]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == 1)
            {
                customAI[0] = reader.ReadFloat();
                customAI[1] = reader.ReadFloat();
                customAI[2] = reader.ReadFloat();
                customAI[3] = reader.ReadFloat();
            }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Kraken");
            
        }
        public override void SetDefaults()
        {
            npc.width = 106;
            npc.height = 122;
            //npc.alpha = 255;
            npc.damage = 80;
            npc.defense = 40;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Kraken");
            npc.lifeMax = 120000;
            npc.dontTakeDamage = false;
            npc.noGravity = true;
            npc.aiStyle = -1;
            //npc.timeLeft = 10;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
        }

        int frameheight = 122;

        public override void AI()
        {
            Player player = Main.player[npc.target];
            
            BaseAI.AISkull(npc, ref customAI, true, 7f, 100f, .04f, .049f);

            npc.ai[1]++;
            npc.frameCounter++;


            if (npc.ai[1] < 400)
            {
                if (npc.frameCounter > 8)
                {
                    npc.frame.Y += 130;
                }
                if (npc.frame.Y >= 130 * 4)
                {
                    npc.frame.Y = 0;
                }
            }
            else
            {
                if (npc.ai[1] >= 407)
                {
                    npc.frame.Y = frameheight * 4;
                }
                if (npc.ai[1] >= 414)
                {
                    npc.frame.Y = frameheight * 5;
                }
                if (npc.ai[1] >= 421)
                {
                    npc.frame.Y = frameheight * 6;
                }
                if (npc.ai[1] == 421)
                {
                    Projectile.NewProjectile(new Vector2(player.Center.X + 80, player.Center.Y), new Vector2(0, 0), mod.ProjectileType<Tentacle>(), 120, 0);
                }
                if (npc.ai[1] >= 428 && npc.ai[1] < 490)
                {
                    if (npc.frameCounter > 8)
                    {
                        npc.frame.Y += frameheight;
                    }
                    if (npc.frame.Y >= frameheight * 10)
                    {
                        npc.frame.Y = frameheight * 7;
                    }
                }
                if (npc.ai[1] >= 497)
                {
                    npc.frame.Y = frameheight * 11;
                }
                if (npc.ai[1] >= 504)
                {
                    npc.frame.Y = frameheight * 12;
                }
                if (npc.ai[1] >= 511)
                {
                    npc.frame.Y = frameheight * 13;
                }
                if (npc.ai[1] >= 518)
                {
                    npc.frame.Y = 0;
                    npc.ai[1] = 0;
                }
            }
        }
    }
}