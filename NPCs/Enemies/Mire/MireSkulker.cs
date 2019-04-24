using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Enemies.Mire
{

    public class MireSkulker : ModNPC
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Skulker");

            Main.npcFrameCount[npc.type] = 11;
        }

        public override void SetDefaults()
        {
            npc.lifeMax = 140;
            npc.damage = 8;
            npc.defense = 14;
            npc.knockBackResist = 1f;
            npc.value = Item.buyPrice(0, 0, 6, 45);
            npc.aiStyle = -1;
            npc.width = 104;
            npc.height = 28;
            npc.npcSlots = 1f;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.lavaImmune = true;
            npc.noGravity = false;
            npc.noTileCollide = false;

        }

        private bool Shell = false;
        private int ShellTimer = 0;

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
            }
        }

        public override void AI()
        {
            Player player = Main.player[npc.target];

            npc.defense = 14;
            npc.reflectingProjectiles = false;

            if (npc.velocity.X > 0) // so it faces the player
            {
                npc.spriteDirection = -1;
            }
            else
            {
                npc.spriteDirection = 1;
            }

            if (!Shell)
            {
                ShellTimer++;
                if (ShellTimer > 500)
                {
                    npc.frame.Y = 240 * 6;
                    ShellTimer = 0;
                    Shell = true;
                    npc.netUpdate = true;
                }
                if (npc.frameCounter++ > 9)
                {
                    npc.frame.Y += 40;
                    npc.frameCounter = 0;
                    if (npc.frame.Y > 200)
                    {
                        npc.frame.Y = 0;
                    }
                }
                BaseAI.AIZombie(npc, ref npc.ai, true, true, -1, 0.08f, 1f, 2, 3, 120);
            }
            else
            {
                npc.defense = 999;
                npc.reflectingProjectiles = true;
                ShellTimer++;
                if (ShellTimer < 120)
                {
                    if (npc.frame.Y < 240 * 6)
                    {
                        npc.frame.Y = 240 * 6;
                    }

                    if (npc.frameCounter++ > 9)
                    {
                        npc.frame.Y += 40;
                        npc.frameCounter = 0;
                        if (npc.frame.Y > 320)
                        {
                            npc.frame.Y = 320;
                        }
                    }
                }
                else
                {
                    npc.defense = 14;
                    npc.reflectingProjectiles = false;

                    if (npc.frameCounter++ > 9)
                    {
                        npc.frame.Y += 40;
                        npc.frameCounter = 0;
                        if (npc.frame.Y > 400)
                        {
                            Shell = false;
                            npc.netUpdate = true;
                        }
                    }
                }
            }
        }

        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("MirePod"));
        }
    }
}


