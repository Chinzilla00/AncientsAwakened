using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.NPCs.Enemies.Mushroom
{
    public class ShroomJelly : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mushroom Jelly");
            Main.npcFrameCount[npc.type] = 4;
		}

		public override void SetDefaults()
        {
            npc.CloneDefaults(NPCID.FungoFish);
            animationType = NPCID.FungoFish;
            npc.noGravity = true;
            npc.width = 26;
            npc.height = 26;
            npc.aiStyle = 18;
            npc.damage = 20;
            npc.defense = 20;
            npc.lifeMax = 70;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath6;
            npc.value = 1000f;
            npc.alpha = 20;
            npc.npcSlots = 0.3f;
        }

        public override void HitEffect(int hitDirection, double damage)
		{

            int dust1 = mod.DustType<Dusts.MushDust>();
            if (npc.life <= 0)
			{
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust1, 0f, 0f, 0, default(Color), 1f);
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust1, 0f, 0f, 0, default(Color), 1f);
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust1, 0f, 0f, 0, default(Color), 1f);
            }
		}

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.player.GetModPlayer<AAPlayer>(mod).ZoneMush && spawnInfo.water ? .7f : 0f;
        }

        public override void NPCLoot()
		{
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Mushroom);
        }
	}
}