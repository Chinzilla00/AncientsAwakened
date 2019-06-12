using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Usable
{
    public class GoblinDoll : ModItem
    {
        public override void SetDefaults()
        {

            item.width = 16;
            item.height = 28;
            item.rare = 0;
            item.value = 50000;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(204, 102, 0);
                }
            }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Goblin Tinkerer Doll");
            Tooltip.SetDefault(@"I'm sorry, little one...");
        }

        public override void PostUpdate()
        {
            if (item.lavaWet)
            {
                for (int i = 0; i < 200; ++i)
                {
                    if (Main.npc[i].type == NPCID.GoblinTinkerer && Main.npc[i].active)
                    {
                        Player player = Main.player[item.owner];
                        player.QuickSpawnItem(mod.ItemType<Accessories.SoulStone>());
                        Main.npc[i].StrikeNPCNoInteraction(9999, 10f, -Main.npc[i].direction, false, false, false);
                        Main.NewText("The soul stone materializes in your hand", 180, 120, 0);
                    }
                }
            }
        }
    }
}
