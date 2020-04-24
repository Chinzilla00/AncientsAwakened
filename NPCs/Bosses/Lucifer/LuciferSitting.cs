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

        public static bool SwitchInfo = false;
        public static bool Who = false;
        public static bool ThePit = false;
        public static bool Pit1 = false;
        public static bool Pit2 = false;
        public static bool Pit3 = false;
        public static bool Pit4 = false;
        public static bool Pit5 = false;
        public static bool Pit6 = false;
        public static bool Pit7 = false;
        public static bool Pit8 = false;
        public static bool Pit9 = false;
        public static bool Pit10 = false;
        public static bool Pit11 = false;
        public static bool Pit12 = false;
        public static bool Pit13 = false;
        public static bool Pit14 = false;
        public static bool Pit15 = false;
        public static bool Pit16 = false;
        public static bool Pit17 = false;
        public static bool Pit18 = false;
        public static bool Pit19 = false;
        public static bool Pit20 = false;
        public static bool LuciferC = false;
        public static int ChatNumber = 0;

        public override void ResetEffects()
        {
            SwitchInfo = false;
            ThePit = false;
            Who = false;
            Pit1 = false;
            Pit2 = false;
            Pit3 = false;
            Pit4 = false;
            Pit5 = false;
            Pit6 = false;
            Pit7 = false;
            Pit8 = false;
            Pit9 = false;
            Pit10 = false;
            Pit11 = false;
            Pit12 = false;
            Pit13 = false;
            Pit14 = false;
            Pit15 = false;
            Pit16 = false;
            Pit17 = false;
            Pit18 = false;
            Pit19 = false;
            Pit20 = false;
            LuciferC = false;
        }
    
        public override void SetChatButtons(ref string button, ref string button2)
        {
            string SwitchInfoT = "Cycle Dialogue";
            string ThePitT = "What is this place?";
            string WhoT = "Who are you?";
            string Pit1T = "Pit Challenge 1 (1 Copper)";
            string Pit2T = "Pit Challenge 2 (10 Silver)";
            string Pit3T = "Pit Challenge 3 (50 Silver)";
            string Pit4T = "Pit Challenge 4 (1 Gold)";
            string Pit5T = "A Glutton for a Good Time (5 Gold)";
            string Pit6T = "Pit Challenge 6 (10 Gold)";
            string Pit7T = "Pit Challenge 7 (15 Gold)";
            string Pit8T = "Pit Challenge 8 (20 Gold)";
            string Pit9T = "Pit Challenge 9 (25 Gold)";
            string Pit10T = "A Demon's Wrath (40 Gold)";
            string Pit11T = "Pit Challenge 11 (50 Gold)";
            string Pit12T = "Pit Challenge 12 (60 Gold)";
            string Pit13T = "Pit Challenge 13 (70 Gold)";
            string Pit14T = "Pit Challenge 14 (80 Gold)";
            string Pit15T = "Bloodlust (1 Platinum)";
            string Pit16T = "Pit Challenge 16 (2 Platinum)";
            string Pit17T = "Pit Challenge 17 (5 Platinum)";
            string Pit18T = "Pit Challenge 18 (10 Platinum)";
            string Pit19T = "Pit Challenge 19 (15 Platinum)";
            string Pit20T = "An Act of Treachery (30 Platinum)";
            string LuciferT = "My turn. ('Free!')";

            button = SwitchInfoT;

            if (ChatNumber == 1)
            {
                button2 = WhoT;
                Pit1 = true;
            }
            else if (ChatNumber == 2)
            {
                button2 = Pit1T;
                Pit1 = true;
            }
            else if (ChatNumber == 3)
            {
                button2 = Pit2T;
                Pit2 = true;
            }
            else if (ChatNumber == 4)
            {
                button2 = Pit3T;
                Pit3 = true;
            }
            else if (ChatNumber == 5)
            {
                button2 = Pit4T;
                Pit4 = true;
            }
            else if (ChatNumber == 6)
            {
                button2 = Pit5T;
                Pit5 = true;
            }
            else if (ChatNumber == 7)
            {
                button2 = Pit6T;
                Pit6 = true;
            }
            else if (ChatNumber == 8)
            {
                button2 = Pit7T;
                Pit7 = true;
            }
            else if (ChatNumber == 9)
            {
                button2 = Pit8T;
                Pit8 = true;
            }
            else if (ChatNumber == 10)
            {
                button2 = Pit9T;
                Pit9 = true;
            }
            else if (ChatNumber == 11)
            {
                button2 = Pit10T;
                Pit10 = true;
            }
            else if (ChatNumber == 12)
            {
                button2 = Pit11T;
                Pit11 = true;
            }
            else if (ChatNumber == 13)
            {
                button2 = Pit12T;
                Pit12 = true;
            }
            else if (ChatNumber == 14)
            {
                button2 = Pit13T;
                Pit13 = true;
            }
            else if (ChatNumber == 15)
            {
                button2 = Pit14T;
                Pit14 = true;
            }
            else if (ChatNumber == 16)
            {
                button2 = Pit15T;
                Pit15 = true;
            }
            else if (ChatNumber == 17)
            {
                button2 = Pit16T;
                Pit16 = true;
            }
            else if (ChatNumber == 18)
            {
                button2 = Pit17T;
                Pit17 = true;
            }
            else if (ChatNumber == 19)
            {
                button2 = Pit18T;
                Pit18 = true;
            }
            else if (ChatNumber == 20)
            {
                button2 = Pit19T;
                Pit19 = true;
            }
            else if (ChatNumber == 21)
            {
                button2 = Pit20T;
                Pit20 = true;
            }
            else if (ChatNumber == 22)
            {
                button2 = LuciferT;
                LuciferC = true;
            }
            else
            {
                ChatNumber = 0;
                button2 = ThePitT;
                ThePit = true;
            }
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