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
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/WormSpawn");
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

            string s = Main.netMode == 0 ? "" : "s";

            if (npc.ai[0] == 180)
            {
                if (Main.netMode != 1) BaseUtility.Chat("It appears someone has reassembled our idol, dearest Daybringer.", new Color(0, 255, 181));
            }

            if (npc.ai[0] == 360)
            {

                if (Main.netMode != 1) BaseUtility.Chat("It appears so. Little one" + s + ", you have impressed us greatly", new Color(43, 178, 245));
            }

            if (npc.ai[0] == 540)
            {
                if (Main.netMode != 1) BaseUtility.Chat("Could they be the one we've been searching for..?", new Color(0, 255, 181));
            }

            if (npc.ai[0] == 720)
            {
                if (Main.netMode != 1) BaseUtility.Chat("...possibly.", new Color(43, 178, 245));
            }
            if (npc.ai[0] == 900)
            {
                if (Main.netMode != 1) BaseUtility.Chat("Young one" + s + ". We would like to provide you with one last test.", new Color(43, 178, 245));
            }

            if (npc.ai[0] == 960)
            {
                if (Main.netMode != 1) BaseUtility.Chat("A test of strength against us.", new Color(0, 255, 181));
            }

            if (npc.ai[0] == 1140)
            {
                if (Main.netMode != 1) BaseUtility.Chat("When you are ready, call us forth with the worm statue in your hand.", new Color(43, 178, 245));
            }

            if (npc.ai[0] == 1320)
            {
                if (Main.netMode != 1) BaseUtility.Chat("We will be able to find you because of these beacons you reactivated.", new Color(0, 255, 181));
            }

            if (npc.ai[0] == 1520)
            {
                if (Main.netMode != 1) BaseUtility.Chat("However...", new Color(0, 255, 181));
            }

            if (npc.ai[0] >= 1880)
            {
                string name = Main.netMode == 0 ? player.name : "heroes";

                if (Main.netMode != 1) BaseUtility.Chat("Do not expect us to go easy on you, " + name + ".", new Color(0, 255, 181));

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