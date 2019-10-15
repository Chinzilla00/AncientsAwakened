namespace AAMod.Items.Misc
{
    public class Fishmother : BaseAAItem
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
            description = Lang.questFish("Fishmother");
            catchLocation = Lang.questFish("FishmotherLocation");
        }
    }
}