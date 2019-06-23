using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Misc
{
    public class TriHeadedKoi : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tri-headed Koi");
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
            return AAWorld.downedHydra;
        }

        public override void AnglerQuestChat(ref string description, ref string catchLocation)
        {
            description = "I hate the Mire. I can't see a thing in there unless it's past my bedtime, but I know there's a killer fish in there that I want that has, get this, THREE HEADS. Can you go get it for me?";
            catchLocation = "Caught anywhere in the Mire";
        }
    }
}