using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.NPCs.Enemies.Inferno
{
    public class ChaoticDawn : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Chaotic Dawn");
            Main.npcFrameCount[npc.type] = 4;
		}

		public override void SetDefaults()
        {
            npc.width = 66;
            npc.height = 68;
            npc.damage = 60;
			npc.defense = 25;
			npc.lifeMax = 250;
			npc.HitSound = SoundID.NPCHit4;
			npc.DeathSound = SoundID.NPCDeath6;
            npc.value = 240000f;
            npc.knockBackResist = .30f;
            npc.aiStyle = -1;
        }

        public override void AI()
        {
            BaseAI.AIWeapon(npc, ref npc.ai, 100, 100, 9f, 1f, 1f);
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (npc.frameCounter < 3)
            {
                npc.frame.Y = 0 * frameHeight;
            }
            else if (npc.frameCounter < 6)
            {
                npc.frame.Y = 1 * frameHeight;
            }
            else if (npc.frameCounter < 9)
            {
                npc.frame.Y = 2 * frameHeight;
            }
            else if (npc.frameCounter < 12)
            {
                npc.frame.Y = 3 * frameHeight;
            }
            else
            {
                npc.frameCounter = 0;
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
            return spawnInfo.player.GetModPlayer<AAPlayer>(mod).ZoneInferno && spawnInfo.spawnTileY > Main.worldSurface && Main.hardMode ? .1f : 0f;
        }

		public override void HitEffect(int hitDirection, double damage)
		{

            int dust1 = mod.DustType<Dusts.BroodmotherDust>();
            if (npc.life <= 0)
			{
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust1, 0f, 0f, 0, default(Color), 1f);
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust1, 0f, 0f, 0, default(Color), 1f);
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust1, 0f, 0f, 0, default(Color), 1f);
            }
		}

		public override void NPCLoot()
		{
            
                if (Main.rand.NextFloat() < 0.1f)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("AbyssalTwilight"));
                }
        }
	}
}