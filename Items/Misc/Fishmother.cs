using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Misc
{
    public class Fishmother : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fishmother");
        }


        public override void SetDefaults()
        {
            item.questItem = true;
            item.maxStack = 1;
            item.width = 26;
            item.height = 26;
            item.uniqueStack = true;
            item.rare = -11;
        }

        public override bool IsQuestFish()
        {
            return true;
        }

        public override bool IsAnglerQuestAvailable()
        {
            return AAWorld.downedBrood;
        }

        public override void AnglerQuestChat(ref string description, ref string catchLocation)
        {
            description = "Okay so, I was walking through the Inferno looking for something hot for one of my epic pranks, when I saw this fat fish underwater, boiling the water around it. I thought 'PERFECT!' but I couldn't catch it because I didn't have my rod on me. Go get it, slave.";
            catchLocation = "Caught anywhere in the Inferno";
        }
    }
}