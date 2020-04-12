using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles.Altar
{
    public class WormSpawn : ModNPC
    {
        public override string Texture => "AAMod/BlankTex";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Heavenly Voice");
            Terraria.ID.NPCID.Sets.TechnicallyABoss[npc.type] = true;
        }
        public override void SetDefaults()
        {
            npc.width = 46;
            npc.height = 46;
            npc.alpha = 255;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Prequinox");
            npc.lifeMax = 1;
            npc.dontTakeDamage = true;
            npc.noGravity = true;
            npc.aiStyle = -1;
            npc.timeLeft = 10000000;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
        }

        public override void AI()
        {
            if (!npc.HasNPCTarget)
            {
                npc.TargetClosest();
            }
            Player player = Main.player[npc.target];
            npc.Center = player.Center - new Vector2(0, 300f);

            if (!NPC.AnyNPCs(ModContent.NPCType<DBPortal>()))
            {
                WormAltar.SpawnBoss(player, ModContent.NPCType<DBPortal>(), false, player.Center);
            }
            if (!NPC.AnyNPCs(ModContent.NPCType<NCPortal>()))
            {
                WormAltar.SpawnBoss(player, ModContent.NPCType<NCPortal>(), false, player.Center);
            }

            npc.ai[0]++;

            string s = Main.netMode == 0 ? "" : Lang.TheEquinox("s");

            if (npc.ai[0] == 180)
            {
                if (Main.netMode != 1) BaseUtility.Chat(Lang.TheEquinox("WormSpawn1"), new Color(0, 255, 181));
            }

            if (npc.ai[0] == 360)
            {

                if (Main.netMode != 1) BaseUtility.Chat(Lang.TheEquinox("WormSpawn2") + s + Lang.TheEquinox("WormSpawn3"), new Color(43, 178, 245));
            }

            if (npc.ai[0] == 540)
            {
                if (Main.netMode != 1) BaseUtility.Chat(Lang.TheEquinox("WormSpawn4"), new Color(0, 255, 181));
            }

            if (npc.ai[0] == 720)
            {
                if (Main.netMode != 1) BaseUtility.Chat(Lang.TheEquinox("WormSpawn5"), new Color(43, 178, 245));
            }
            if (npc.ai[0] == 900)
            {
                if (Main.netMode != 1) BaseUtility.Chat(Lang.TheEquinox("WormSpawn6") + s + Lang.TheEquinox("WormSpawn7"), new Color(43, 178, 245));
            }

            if (npc.ai[0] == 960)
            {
                if (Main.netMode != 1) BaseUtility.Chat(Lang.TheEquinox("WormSpawn8"), new Color(0, 255, 181));
            }

            if (npc.ai[0] == 1140)
            {
                if (Main.netMode != 1) BaseUtility.Chat(Lang.TheEquinox("WormSpawn9"), new Color(43, 178, 245));
            }

            if (npc.ai[0] == 1320)
            {
                if (Main.netMode != 1) BaseUtility.Chat(Lang.TheEquinox("WormSpawn10"), new Color(0, 255, 181));
            }

            if (npc.ai[0] == 1520)
            {
                if (Main.netMode != 1) BaseUtility.Chat(Lang.TheEquinox("WormSpawn11"), new Color(0, 255, 181));
            }

            if (npc.ai[0] >= 1880)
            {
                string name = Main.netMode == 0 ? player.name : Lang.TheEquinox("heroes");

                if (Main.netMode != 1) BaseUtility.Chat(Lang.TheEquinox("WormSpawn12") + name + ".", new Color(0, 255, 181));

                AAWorld.WormActive = true;
                npc.active = false;
                npc.netUpdate = true;
            }
        }

        public override bool PreAI()
        {
            if (AAConfigClient.Instance.NoBossDialogue)
            {
                return false;
            }
            return true;
        }

        public override bool PreDraw(SpriteBatch spritebatch, Color lightColor)
        {
            return false;
        }
    }
}