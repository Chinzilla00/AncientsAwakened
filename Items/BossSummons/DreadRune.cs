using System.Collections.Generic;

using Microsoft.Xna.Framework;

//using AAMod.NPCs.Bosses.Infinity;
using Terraria;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.Items.BossSummons
{
    public class DreadRune : BaseAAItem
	{

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dread Moon Rune");
            Tooltip.SetDefault(@"An enchanted tablet eminating with dark chaotic energy
Summons Yamata Awakened
Only Usable at night in the mire
Non-Consumable");
        }
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.rare = 2;
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;
            item.consumable = false;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(146, 30, 68);
                }
            }
        }

        public override bool UseItem(Player player)
		{
            BaseUtility.Chat("Yamata has been Awakened!", Color.Magenta.R, Color.Magenta.G, Color.Magenta.B);
            BaseUtility.Chat("Yeah, yeah I get it, my first phase is obnoxious. Let’s just get this over with..!", new Color(146, 30, 68));
            DreadSigil.SpawnBoss(player, mod.NPCType<NPCs.Bosses.Yamata.Awakened.YamataA>(), false, new Vector2(player.Center.X, player.Center.Y - 100), "Yamata, Dread Nightmare");
            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/YamataRoar"), player.position);
            return true;
		}

        public override bool CanUseItem(Player player)
        {
            if (Main.dayTime)
            {
                if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("NO! I DON'T WANNA FIGHT NOW! I NEED MY BEAUTY SLEEP! COME BACK AT NIGHT!", new Color(45, 46, 70), false);
                return false;
            }
            if (player.GetModPlayer<AAPlayer>(mod).ZoneMire)
            {
                if (!player.GetModPlayer<AAPlayer>(mod).ZoneRisingMoonLake && !AAWorld.downedYamata)
                {
                    if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("An image of the strange tree at the heart of the mire flashes through your mind", Color.Indigo, false);
                    return false;
                }
                if (NPC.AnyNPCs(mod.NPCType("Yamata")))
                {
                    if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("WHAT THE HELL ARE YOU DOING?! I'M ALREADY HERE!!!", new Color(45, 46, 70), false);
                    return false;
                }
                if (NPC.AnyNPCs(mod.NPCType("YamataA")))
                {
                    if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("WHAT THE HELL ARE YOU DOING?! I'M ALREADY HERE!!!", new Color(146, 30, 68), false);
                    return false;
                }
                if (NPC.AnyNPCs(mod.NPCType("YamataTransition")))
                {
                    return false;
                }
                return true;
            }
            if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("Hey Dumbo! Mire is that way!", new Color(45, 46, 70), false);
            return false;
        }
		
	}
}