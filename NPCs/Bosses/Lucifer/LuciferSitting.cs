using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
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

        int chatNumber = 0;

        public override void ResetEffects()
        {
            chatNumber = 0;
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            string who = "Who are you?";
            string when = "When will it be done?";
            string why = "Why are you building it?";
            string bye = "Alright, goodbye.";
            if (chatNumber == 0)
            {
                button = who;
            }
            else if (chatNumber == 1)
            {
                button = when;
            }
            else if (chatNumber == 2)
            {
                button = why;
            }
            else if (chatNumber == 3)
            {
                button = bye;
            }
            else
            {
                button = null;
            }
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            if (firstButton)
            {
                chatNumber++;
                if (chatNumber == 0)
                {
                    Main.npcChatText = @"Who am I?! I'm-- Who am I kiddin'. You know who I am. Now skedaddle, I got an arena to get built.";
                }
                else if (chatNumber == 1)
                {
                    Main.npcChatText = @"I don't know, whenever my guys get off their lazy behinds and actually start building stuff.";
                }
                else if (chatNumber == 2)
                {
                    Main.npcChatText = @"You have a lot of questions, don't you? I'm building it because I want to watch guts spill. Why else?";
                }
                else if (chatNumber == 3)
                {
                    Main.npcChatText = @"See you around. Come back when I finish, I'd love to see you get gored! BWAHAHAHAHAHAHAHAHAH!!!";
                }
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
    }
}