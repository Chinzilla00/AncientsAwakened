using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AAMod.NPCs.Bosses.AH.Ashe;

namespace AAMod.NPCs.Bosses.Akuma.Awakened
{
    [AutoloadBossHead]
    public class AsheA : Ashe
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ashe Akuma");
            Main.npcFrameCount[npc.type] = 24;
        }


        public override void SetDefaults()
        {
            npc.width = 40;
            npc.height = 80;
            npc.damage = 80;
            npc.defense = 40;
            npc.lifeMax = 100000;
            npc.value = Item.sellPrice(0, 0, 0, 0);
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
            npc.knockBackResist = 0f;
            npc.knockBackResist = 0f;
            npc.lavaImmune = true;
            npc.netAlways = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
        }
        public int body = -1;

        public override bool PreAI()
        {
            if (!NPC.AnyNPCs(mod.NPCType<AkumaA>()))
            {
                int DeathAnim = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType<AsheVanish>(), 0);
                Main.npc[DeathAnim].velocity = npc.velocity;
                npc.active = false;
                npc.netUpdate = true;
            }
            return true;
        }

        public override bool CheckActive()
        {
            if (!NPC.AnyNPCs(mod.NPCType<AkumaA>()))
            {
                return true;
            }
            return false;
        }
        

        public override void NPCLoot()
        {
            npc.value = 0f;
            npc.boss = false;
            int DeathAnim = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType<AsheVanish>(), 0);
            Main.npc[DeathAnim].velocity = npc.velocity;
            if (!NPC.AnyNPCs(mod.NPCType<AkumaA>()))
            {
                if (Main.netMode != 1) BaseMod.BaseUtility.Chat(Lang.BossChat("AkumaAAshe1"), new Color(102, 20, 48));
                return;
            }
            npc.DropLoot(mod.ItemType<Items.Blocks.DaybreakIncineriteOre>(), Main.rand.Next(10, 25));
            if (Main.netMode != 1) BaseMod.BaseUtility.Chat(Lang.BossChat("AkumaAAshe2"), new Color(102, 20, 48));
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = 0;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.6f * bossLifeScale);  //boss life scale in expertmode
            npc.damage = (int)(npc.damage * 0.6f);
        }
    }
}


