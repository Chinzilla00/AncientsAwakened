using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.NPCs.Enemies.Mushroom
{
    public class MushroomCrab : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mushroom Crab");
            Main.npcFrameCount[npc.type] = 5;
		}

		public override void SetDefaults()
        {
            npc.width = 44;
            npc.height = 34;
            npc.aiStyle = 3;
            npc.damage = 16;
            npc.defense = 20;
            npc.lifeMax = 140;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath6;
            animationType = NPCID.AnomuraFungus;
            npc.knockBackResist = 0.3f;
            npc.value = 1300f;
            npc.buffImmune[31] = false;
            npc.npcSlots = 0.3f;
            banner = npc.type;
			bannerItem = mod.ItemType("MushroomCrabBanner");
        }

        public override void AI()
        {
            Globals.AAAI.InfernoFighterAI(npc, ref npc.ai, true, false, -1, 0.13f, 3f, 3, 4, 60, true, 10, 60, true, null, false);
        }

        public override void HitEffect(int hitDirection, double damage)
		{

            int dust1 = ModContent.DustType<Dusts.MushDust>();
            if (npc.life <= 0)
			{
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust1, 0f, 0f, 0);
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust1, 0f, 0f, 0);
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust1, 0f, 0f, 0);
            }
		}

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.player.GetModPlayer<AAPlayer>().ZoneMush ? .4f : 0f;
        }

        public override void NPCLoot()
		{
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Mushroom);
        }
	}
}