using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.NPCs.Enemies.Mushroom
{
    public class Mushbug : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mushbug");
            Main.npcFrameCount[npc.type] = 6;
		}

		public override void SetDefaults()
        {
            npc.CloneDefaults(NPCID.MushiLadybug);
            npc.width = 30;
            npc.height = 24;
            npc.aiStyle = 3;
            npc.damage = 10;
            npc.defense = 9;
            npc.lifeMax = 100;
            npc.HitSound = SoundID.NPCHit45;
            npc.DeathSound = SoundID.NPCDeath47;
            npc.knockBackResist = 0.3f;
            animationType = NPCID.MushiLadybug;
            npc.value = 1000f;
            npc.buffImmune[31] = false;
            npc.npcSlots = 0.3f;
            banner = npc.type;
			bannerItem = mod.ItemType("MushbugBanner");
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
            return spawnInfo.player.GetModPlayer<AAPlayer>().ZoneMush ? 1f : 0f;
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