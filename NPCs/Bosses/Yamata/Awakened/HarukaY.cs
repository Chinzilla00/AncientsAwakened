using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;
using AAMod.NPCs.Bosses.AH.Haruka;

namespace AAMod.NPCs.Bosses.Yamata.Awakened
{
    [AutoloadBossHead]
    public class HarukaY : Haruka
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Haruka Yamata");
            Main.npcFrameCount[npc.type] = 27;
            NPCID.Sets.TechnicallyABoss[npc.type] = true;
        }

        public override void SetDefaults()
        {
            npc.width = 50;
            npc.height = 60;
            npc.friendly = false;
            npc.damage = 80;
            npc.defense = 50;
            npc.lifeMax = 90000;
            npc.HitSound = SoundID.NPCHit1;
            npc.knockBackResist = 0f;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.lavaImmune = true;
            npc.netAlways = true;
            npc.noGravity = true;
            npc.boss = false;
            npc.value = Item.sellPrice(0, 0, 0, 0);
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Yamata2");
        }

        public int body = -1;

        public override void PostAI()
        {
            Player player = Main.player[npc.target];
            if (internalAI[0] != AISTATE_SPIN)
            {
                if (player.Center.X > npc.Center.X) //If NPC's X position is higher than the player's
                {
                    if (pos == -250)
                    {
                        pos = 250;
                    }
                    npc.direction = 1;
                }
                else //If NPC's X position is lower than the player's
                {
                    if (pos == 250)
                    {
                        pos = -250;
                    }
                    npc.direction = -1;
                }
            }
            else
            {
                npc.direction = npc.velocity.X > 0 ? 1 : -1;
            }
            if (!NPC.AnyNPCs(Terraria.ModLoader.ModContent.NPCType<YamataA>()))
            {
                npc.life = 0;
            }

            if (body == -1)
            {
                int npcID = BaseAI.GetNPC(npc.Center, mod.NPCType("YamataA"), -1, null);
                if (npcID >= 0) body = npcID;
            }
            if (body == -1) return;
            NPC Yamata = Main.npc[body];
            if (Yamata == null || Yamata.life <= 0 || !Yamata.active || Yamata.type != mod.NPCType("YamataA")) { BaseAI.KillNPCWithLoot(npc); return; }
            if (Yamata.life <= Yamata.lifeMax / 5)
            {
                music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/RayOfHope");
            }
        }

        public override bool CheckActive()
        {
            if (!NPC.AnyNPCs(Terraria.ModLoader.ModContent.NPCType<YamataA>()))
            {
                return true;
            }
            return false;
        }

        public override void NPCLoot()
        {
            npc.value = 0f;
            npc.boss = false;
            int DeathAnim = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, Terraria.ModLoader.ModContent.NPCType<HarukaVanish>(), 0);
            Main.npc[DeathAnim].velocity = npc.velocity;
            if (!NPC.AnyNPCs(Terraria.ModLoader.ModContent.NPCType<YamataA>()))
            {
                if (Main.netMode != 1) BaseUtility.Chat("Dad, you moron..! Whatever, Can't really say I didn't see it coming.", new Color(72, 78, 117));
                return;
            }
            if (Main.netMode != 1) BaseUtility.Chat("That's it. I'm done, YOU deal with them, dad.", new Color(72, 78, 117));
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = 0;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.6f * bossLifeScale);  //boss life scale in expertmode
            npc.damage = (int)(npc.damage * .9f);
        }
    }
}


