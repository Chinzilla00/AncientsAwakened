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
    public class RajahSpawn : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Doom");
            Main.npcFrameCount[npc.type] = 8;
        }

        public override void SetDefaults()
        {
            npc.width = 300;
            npc.height = 221;
            npc.aiStyle = -1;
            npc.damage = 0;
            npc.defense = 80;
            npc.lifeMax = 50000;
            npc.dontTakeDamage = true;
            npc.knockBackResist = 0f;
            npc.npcSlots = 1000f;
            npc.boss = true;
            npc.netAlways = true;
            npc.noGravity = false;
            music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/RajahTheme");
        }

        public int frameHeight = 220;

        public override void AI()
        {
            if (npc.ai[1] == 0f)
            {
                if (npc.velocity.Y == 0f)
                {
                    Main.PlaySound(SoundID.Item14, npc.position);
                    npc.ai[1] = 1f;
                    for (int num622 = (int)npc.position.X - 20; num622 < (int)npc.position.X + npc.width + 40; num622 += 20)
                    {
                        for (int num623 = 0; num623 < 4; num623++)
                        {
                            int num624 = Dust.NewDust(new Vector2(npc.position.X - 20f, npc.position.Y + (float)npc.height), npc.width + 20, 4, 31, 0f, 0f, 100, default(Color), 1.5f);
                            Main.dust[num624].velocity *= 0.2f;
                        }
                        int num625 = Gore.NewGore(new Vector2((float)(num622 - 20), npc.position.Y + (float)npc.height - 8f), default(Vector2), Main.rand.Next(61, 64), 1f);
                        Main.gore[num625].velocity *= 0.4f;
                    }
                }
            }
            npc.frameCounter++;
            if (npc.velocity.Y != 0)
            {
                if (npc.frameCounter > 7)
                {
                    npc.frameCounter = 0;
                    npc.frame.Y += frameHeight;
                }
                if (npc.frame.Y > frameHeight * 3)
                {
                    npc.frame.Y = 0;
                }
            }
            else
            {
                if (npc.frameCounter > 6)
                {
                    npc.frameCounter = 0;
                    npc.frame.Y += frameHeight;
                }
                if (npc.frame.Y > frameHeight * 7)
                {
                    npc.frame.Y = frameHeight * 7;
                }
                if (npc.ai[0]++ > 400)
                {
                    npc.active = false;
                }
            }
        }
    }
}