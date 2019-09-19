using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Serpent
{
    public class SerpentTail : ModNPC
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Subzero Serpent");
	        Main.npcFrameCount[npc.type] = 1;		
		}
		
		public override void SetDefaults()
        {
            npc.npcSlots = 5f;
            npc.width = 38;
            npc.height = 38;
            npc.damage = 35;
            npc.defense = 25;
            npc.lifeMax = 6000;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.knockBackResist = 0f;
            npc.aiStyle = -1;
            animationType = 10;
            npc.behindTiles = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit5;
            npc.DeathSound = SoundID.NPCDeath7;
            npc.netAlways = true;
            npc.boss = true;
            bossBag = mod.ItemType<Items.Boss.Serpent.SerpentBag>();
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Boss6");
            npc.alpha = 50;
            npc.dontCountMe = true;
		}

        public override void AI()
        {
            Player player = Main.player[npc.target];
            npc.dontTakeDamage = !player.ZoneSnow;
            if (!Main.npc[(int)npc.ai[1]].active)
            {
                npc.life = 0;
                npc.HitEffect(0, 10.0);
                npc.active = false;
            }
            if (Main.npc[(int)npc.ai[1]].alpha < 128)
            {
                npc.alpha -= 42;
                if (npc.alpha < 0)
                {
                    npc.alpha = 0;
                }
            }
        }

        public override bool PreNPCLoot()
		{
			return false;
		}

		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			return false;
		}

        public override bool CheckActive()
        {
            if (NPC.AnyNPCs(mod.NPCType<SerpentHead>()))
            {
                return false;
            }
            return true;
        }
    }
}