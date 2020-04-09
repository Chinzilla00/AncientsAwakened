using BaseMod;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Enemies.Terrarium.PreHM
{
    public class PurityCrawler : ModNPC
    {
        public override string Texture => "AAMod/NPCs/Enemies/Terrarium/PreHM/PurityCrawler";
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Purity Crawler");
			Main.npcFrameCount[npc.type] = 5;
		}

        public bool Val = false;
        public int[] subNPCs = new int[0];
        public int swapTicks = 0, swapTicksMax = 20;

        public override void SetDefaults()
		{
            npc.lifeMax =  60;
            npc.defense = 5;
            npc.damage = 10;
            npc.width = 26;
            npc.height = 20;
            npc.aiStyle = -1;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0.4f;
            npc.alpha = 255;
            banner = npc.type;
			bannerItem = mod.ItemType("PurityCrawlerBanner");
        }

        public override void NPCLoot()
        {
            if (Main.rand.Next(4) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Items.Materials.TerraShard>());
            }
        }

        public override Color? GetAlpha(Color drawColor)
        {
            return Color.White;
        }

        public override void AI()
        {
            if (npc.alpha != 0)
            {
                for (int spawnDust = 0; spawnDust < 2; spawnDust++)
                {
                    int num935 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 107, 0f, 0f, 100, default, 2f);
                    Main.dust[num935].noGravity = true;
                    Main.dust[num935].noLight = true;
                }
            }
            npc.alpha -= 12;
            if (npc.alpha < 0)
            {
                npc.alpha = 0;
            }
            BaseAI.AIZombie(npc, ref npc.ai, false, false, 0, 0.07f, 3f, 3, 4, 60, true, 10, 60, true, null, false);
        }
    }
}
