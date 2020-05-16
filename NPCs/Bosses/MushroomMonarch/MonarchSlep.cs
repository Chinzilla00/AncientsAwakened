using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.NPCs.Bosses.MushroomMonarch
{
    public class MonarchSlep : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Very Large Mushroom");
            Main.npcFrameCount[npc.type] = 1;
        }

        public override void SetDefaults()
        {
            npc.lifeMax = 200;
            npc.defense = 0;
            npc.damage = 0;
            npc.width = 74;
            npc.height = 70;
            npc.aiStyle = -1;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0f;
            npc.noTileCollide = false;
            npc.noGravity = false;
            npc.value = 0;
            npc.rarity = 1;
        }

        public override bool PreAI()
        {
            npc.velocity.Y += .1f;
            return false;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            bool biomeCorrect = spawnInfo.player.InZone("Surface") && spawnInfo.player.InZone("Purity") || spawnInfo.player.GetModPlayer<AAPlayer>().ZoneMush;
            if (spawnInfo.playerSafe || NPC.AnyNPCs(mod.NPCType("MonarchSlep")) || NPC.AnyNPCs(mod.NPCType("MonarchWake")) || NPC.AnyNPCs(mod.NPCType("MushroomMonarch")))
            {
                return 0f;
            }
            if (biomeCorrect || Main.dayTime)
            {
                return SpawnCondition.OverworldDaySlime.Chance * 0.001f;
            }
            return 0f;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            for (int k = 0; k < 3; k++)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, ModContent.DustType<Dusts.MushDust>(), hitDirection, -1f, 0, default, 1f);
            }
            if (Main.netMode != NetmodeID.MultiplayerClient && (NPC.CountNPCS(mod.NPCType("MonarchWake")) + NPC.CountNPCS(mod.NPCType("MushroomMonarch"))) < 1)
            {
                int id = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("MonarchWake"));
                Main.npc[id].position = npc.position;
            }
            npc.active = false;
            npc.life = 0;
        }
    }
}
