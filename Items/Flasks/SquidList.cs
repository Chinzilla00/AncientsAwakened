using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Flasks
{
    public class SquidList : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lovecraftian's Research List");
            Tooltip.SetDefault(@"I need these things, please!
1. Something from that biosphere underground. A crystal from it could work...
2. Flesh samples from the corruption. I want to see what those things are made of.
3. A bone sample from the crimson. I'm wondering how their bone structure works.
4. Find whatever the creatures in the mire eat. Just kill them until one drops its food or something.
5. A scale from something in the inferno. I'm trying to figure out how these things are so fire resistant.
6. Spore Samples from the jungle. These mushrooms are really annoying, maybe I can make something to kill them off.
7. A sample of whatever the Mushroom Monarch and Feudal Fungus are made of. I might need some help studying these, though...
8. A piece of scrap metal from those floating islands west of here. That stuff looks interesting to make equipment with.
9. Something sparkly from the Hallow. I wonder if I can use it to make more potions...");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 22;
            item.value = 0;
            item.rare = 0;
        }
    }
}