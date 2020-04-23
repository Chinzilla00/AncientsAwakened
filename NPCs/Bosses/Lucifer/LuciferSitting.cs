using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace AAMod.NPCs.Bosses.Lucifer
{
    public class LuciferSitting : ModNPC
    {
        public override void SetDefaults()
        {
            npc.friendly = true;
            npc.townNPC = true;
            npc.dontTakeDamage = true;
            npc.noGravity = true;
            npc.width = 56;
            npc.height = 82;
            npc.damage = 0;
            npc.defense = 0;
            npc.lifeMax = 1;
            npc.aiStyle = -1;
            npc.knockBackResist = 0f;
            npc.npcSlots = 0;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lucifer the Pit Lord");
            Main.npcFrameCount[npc.type] = 9;
            NPCID.Sets.TownCritter[npc.type] = true;
        }

        public override bool UsesPartyHat() { return false; }

        public override void AI()
        {
            npc.wet = false;
            npc.lavaWet = false;
            npc.honeyWet = false;
            npc.velocity.X = npc.velocity.Y = 0f;
            npc.dontTakeDamage = true;
            npc.immune[255] = 30;
            if (Main.netMode != 1)
            {
                npc.homeless = false;
                npc.homeTileX = -1;
                npc.homeTileY = -1;
                npc.netUpdate = true;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (npc.frameCounter >= 8)
            {
                npc.frame.Y += frameHeight;
                npc.frameCounter = 0;
                if (npc.frame.Y > frameHeight * 8)
                {
                    npc.frame.Y = 0;
                }
            }
        }

        public override void ResetEffects()
        {

        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = "Who are you?";
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            if (firstButton)
            {
                Main.npcChatText = WhoU();
                Main.PlaySound(12, -1, -1, 1);
            }
        }

        public override string GetChat()
        {
            WeightedRandom<string> chat = new WeightedRandom<string>();

            chat.Add(@"Come back later. I'm setting up shop here.

Huh? What am I doin'?! I'm supervising.");
            return chat;
        }

        public string WhoU()
        {
            WeightedRandom<string> chat = new WeightedRandom<string>();

            chat.Add(@"Who am I?! I'm--

Who am I kiddin'. You know who I am. Alpha wouldn't have made you a tester if you didn't.");
            return chat;
        }
    }
}