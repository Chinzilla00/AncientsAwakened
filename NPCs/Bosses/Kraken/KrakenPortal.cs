using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Kraken
{
    internal class KrakenPortal : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Portal");
        }

        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.hostile = true;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.penetrate = 1;
            projectile.alpha = 255;
            projectile.timeLeft = 100;
            projectile.aiStyle = -1;
        }
        

        public int usetimer = 0;

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            projectile.rotation += .02f;
            usetimer++;
            if (usetimer >= 60)
            {
                SpawnBoss(player, "Kraken", "Kraken");
            }
        }

        public void SpawnBoss(Player player, string name, string displayName)
        {
            if (Main.netMode != 1)
            {
                int bossType = mod.NPCType(name);
                if (NPC.AnyNPCs(bossType)) { return; } //don't spawn if there's already a boss!
                int npcID = NPC.NewNPC((int)player.Center.X, (int)player.Center.Y, bossType, 0);
                Main.npc[npcID].Center = projectile.Center;
                Main.npc[npcID].netUpdate2 = true;
            }
        }
        
    }
}