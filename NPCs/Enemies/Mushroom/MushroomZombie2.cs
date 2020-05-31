using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.NPCs.Enemies.Mushroom
{
    public class MushroomZombie2 : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mushroom Zombie");
            Main.npcFrameCount[npc.type] = 3;
		}

		public override void SetDefaults()
        {
            npc.width = 18;
            npc.height = 40;
            npc.aiStyle = 3;
            npc.damage = 10;
            npc.defense = 10;
            npc.lifeMax = 70;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath6;
            animationType = NPCID.ZombieMushroom;
            npc.knockBackResist = 0.4f;
            npc.value = 1000f;
            npc.buffImmune[31] = false;
            banner = npc.type;
			bannerItem = mod.ItemType("MushroomZombieBanner");
        }

        public override void AI()
        {
            Globals.AAAI.InfernoFighterAI(npc, ref npc.ai, true, true, 1, 0.07f, 1f, 3, 4, 60, true, 10, 60, true, null, false);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
            return spawnInfo.player.GetModPlayer<AAPlayer>().ZoneMush ? .7f : 0f;
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

		public override void NPCLoot()
		{
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Mushroom);
        }
	}
}