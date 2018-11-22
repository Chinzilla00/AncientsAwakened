using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.EmperorFishron
{
    public class EmperorFishron : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Emperor Fishron");
			Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.DukeFishron];
		}

		public override void SetDefaults()
		{
            npc.width = 150;
            npc.height = 100;
            npc.aiStyle = 69;
            npc.damage = 110;
            npc.defense = 70;
            npc.lifeMax = 400000;
            npc.knockBackResist = 0f;
            npc.noTileCollide = true;
            npc.noGravity = true;
            npc.npcSlots = 10f;
            npc.HitSound = SoundID.NPCHit14;
            npc.DeathSound = SoundID.NPCDeath20;
            npc.value = 1000000f;
            npc.boss = true;
            npc.netAlways = true;
            npc.timeLeft = NPC.activeTime * 30;
            npc.buffImmune[20] = true;
            npc.buffImmune[24] = true;
            npc.buffImmune[31] = true;
            npc.buffImmune[44] = true;
            aiType = NPCID.DukeFishron;
			animationType = NPCID.DukeFishron;
		}
	}
}
