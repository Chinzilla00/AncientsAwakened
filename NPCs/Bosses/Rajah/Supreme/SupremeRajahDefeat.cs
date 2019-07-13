using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.NPCs.Bosses.Rajah.Supreme
{
    [AutoloadBossHead]
    public class SupremeRajahDefeat : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rajah Rabbit");
            Main.npcFrameCount[npc.type] = 9;
        }

        public override void SetDefaults()
        {
            npc.width = 130;
            npc.height = 220;
            npc.aiStyle = -1;
            npc.damage = 0;
            npc.defense = 90;
            npc.lifeMax = 50000;
            npc.knockBackResist = 0f;
            npc.npcSlots = 1000f;
            npc.dontTakeDamage = true;
            npc.boss = true;
            npc.netAlways = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/silence");
            npc.noTileCollide = false;
        }

        public override void AI()
        {
            if (npc.velocity.Y == 0 && Main.netMode != 1)
            {
                npc.ai[0]++;
            }

            if (npc.ai[0] == 120)
            {
                BaseUtility.Chat("Rgh...", 107, 137, 179, true);
            }
            if (npc.ai[0] == 240)
            {
                BaseUtility.Chat("...so...", 107, 137, 179, true);
            }
            if (npc.ai[0] == 360)
            {
                BaseUtility.Chat("Even when I'm at my most powerful...", 107, 137, 179, true);
            }
            if (npc.ai[0] == 480)
            {
                BaseUtility.Chat("...I still can't beat you.", 107, 137, 179, true);
            }
            if (npc.ai[0] >= 600)
            {
                npc.ai[1] = 1;
                music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/ThinkAboutIt");
                npc.netUpdate = true;
            }
            if (npc.ai[0] == 600)
            {
                BaseUtility.Chat("...", 107, 137, 179, true);
            }
            if (npc.ai[0] >= 840)
            {
                npc.ai[1] = 2;
                npc.netUpdate = true;
            }
            if (npc.ai[0] == 840)
            {
                BaseUtility.Chat("...Terrarian...maybe...", 107, 137, 179, true);
            }
            if (npc.ai[0] == 960)
            {
                BaseUtility.Chat("Perhaps this is all just a sign that...maybe my time as protector...", 107, 137, 179, true);
            }
            if (npc.ai[0] == 1080)
            {
                BaseUtility.Chat("...is finally up. It might be time to pass on the baton.", 107, 137, 179, true);
            }
            if (npc.ai[0] == 1200)
            {
                BaseUtility.Chat("...I forgive you for every rabbit you've killed, but in return...I want you to take my place...", 107, 137, 179, true);
            }
            if (npc.ai[0] == 1380)
            {
                BaseUtility.Chat("...as their champion. Their protector.", 107, 137, 179, true);
            }
            if (npc.ai[0] == 1540)
            {
                BaseUtility.Chat("I only want the best for the creatures of this world...and if you're stronger than I am...", 107, 137, 179, true);
            }
            if (npc.ai[0] == 1660)
            {
                string Name;
                if (Main.netMode != 0)
                {
                    Name = "Terrarians";
                }
                else
                {
                    Name = Main.player[Main.myPlayer].name;
                }
                BaseUtility.Chat("Who better to take my place than you, " + Name + "?", 107, 137, 179, true);
            }
            if (npc.ai[0] == 1780)
            {
                BaseUtility.Chat("Be the one the innocent can look to in their time of need.", 107, 137, 179, true);
            }
            if (npc.ai[0] == 1900)
            {
                BaseUtility.Chat("Think about it.", 107, 137, 179, true);
            }
            if (npc.ai[0] == 2020)
            {
                BaseUtility.Chat("And if you ever want to spar...use one of those special carrots. I'd be glad to earn my honor back.", 107, 137, 179, true);
            }
            if (npc.ai[0] >= 2180)
            {
                BaseUtility.Chat("...See ya, kiddo.", 107, 137, 179, true);
                AAWorld.downedRajahsRevenge = true;
                BaseUtility.Chat("Rajah Rabbit's speech warms your heart. You no longer have the will to harm rabbits. Do him proud.", Color.Green, true);
                int p = Projectile.NewProjectile(npc.position, npc.velocity, mod.ProjectileType<SupremeRajahLeave>(), 100, 0, Main.myPlayer);
                Main.projectile[p].position = npc.position;
                npc.active = false;
                npc.netUpdate = true;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            if (npc.ai[1] == 0)
            {
                if (npc.frameCounter++ > 15)
                {
                    npc.frameCounter = 0;
                    npc.frame.Y += frameHeight;
                }
                if (npc.frame.Y > frameHeight * 3)
                {
                    npc.frame.Y = 0;
                }
            }
            else if (npc.ai[1] == 1)
            {
                npc.frame.Y = frameHeight * 4;
            }
            else if (npc.ai[1] == 2)
            {
                if (npc.frameCounter++ > 15)
                {
                    npc.frameCounter = 0;
                    npc.frame.Y += frameHeight;
                }
                if (npc.frame.Y > frameHeight * 8 || npc.frame.Y < frameHeight * 5)
                {
                    npc.frame.Y = frameHeight * 5;
                }
            }
        }
    }
}