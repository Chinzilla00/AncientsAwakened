using System.Collections.Generic;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using BaseMod;

namespace AAMod.Items.BossSummons
{
    public class DreadSigil : BaseAAItem
	{

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dread Moon Sigil");
            Tooltip.SetDefault(@"A ragged old tablet said to contain the dark magic of a new moon
Summons Yamata
Only Usable at night
Non-Consumable");
        }
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.rare = 2;
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 500;
            item.consumable = false;
            item.rare = 10;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity13;;
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "EventideAbyssium", 10);
            recipe.AddIngredient(null, "DarkMatter", 5);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }

        public override bool UseItem(Player player)
		{
            SpawnBoss(player, mod.NPCType<NPCs.Bosses.Yamata.Yamata>(), true, new Vector2(player.Center.X, player.Center.Y - 100), "Yamata, Dread Nightmare");
            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/YamataRoar"), player.position);
            if (!AAWorld.downedYamata)
            {
                Main.NewText("You DARE enter my territory, Terrarian?! NYEHEHEHEHEH..! Big mistake..!", new Color(45, 46, 70));
            }
            if (AAWorld.downedYamata)
            {
                Main.NewText("Back for more..?! This time you won’t be so lucky you little whelp..!", new Color(45, 46, 70));
            }

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
                if (NPC.AnyNPCs(mod.NPCType<NPCs.Bosses.Shen.ShenDoragon>()))
                {
                    return false;
                }
                if (NPC.AnyNPCs(mod.NPCType<NPCs.Bosses.Shen.ShenA>()))
                {
                    return false;
                }
                if (!AAWorld.downedYamata && player.GetModPlayer<AAPlayer>(mod).ZoneRisingMoonLake)
                {
                    if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("You NEED to use that sigil on the altar at the center of the mire! Trust me, nothing bad will happen!", new Color(45, 46, 70), false);
                    return false;
                }
				if (NPC.AnyNPCs(mod.NPCType("Yamata")))
				{
					if(player.whoAmI == Main.myPlayer) BaseUtility.Chat("WHAT THE HELL ARE YOU DOING?! I'M ALREADY HERE!!!", new Color(45, 46, 70), false);
					return false;
				}
                if (NPC.AnyNPCs(mod.NPCType("YamataA")))
                {
                    if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("WHAT THE HELL ARE YOU DOING?! I'M ALREADY HERE!!!", new Color(146, 30, 68), false);
                    return false;
                }
                for (int m = 0; m < Main.maxProjectiles; m++)
                {
                    Projectile p = Main.projectile[m];
                    if (p != null && p.active && p.type == mod.ProjectileType("YamataTransition"))
                    {
                        return false;
                    }
                }
                return true;
			}
			if(player.whoAmI == Main.myPlayer) BaseUtility.Chat("Hey Dumbo! Mire is that way!", new Color(45, 46, 70), false);			
			return false;
		}

        public static void SpawnBoss(Player player, int bossType, bool spawnMessage = true, Vector2 npcCenter = default(Vector2), string overrideDisplayName = "", bool namePlural = false)
        {
            if (npcCenter == default(Vector2))
                npcCenter = player.Center;
            if (Main.netMode != 1)
            {
                if (NPC.AnyNPCs(bossType)) { return; }
                int npcID = NPC.NewNPC((int)npcCenter.X, (int)npcCenter.Y, bossType, 0);
                Main.npc[npcID].Center = npcCenter;
                Main.npc[npcID].netUpdate2 = true;
                if (spawnMessage)
                {
                    string npcName = (!string.IsNullOrEmpty(Main.npc[npcID].GivenName) ? Main.npc[npcID].GivenName : overrideDisplayName);
                    if ((npcName == null || npcName.Equals("")) && Main.npc[npcID].modNPC != null)
                        npcName = Main.npc[npcID].modNPC.DisplayName.GetDefault();
                    if (namePlural)
                    {
                        if (Main.netMode == 0) { Main.NewText(npcName + " have awoken!", 175, 75, 255, false); }
                        else
                        if (Main.netMode == 2)
                        {
                            NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(npcName + " have awoken!"), new Color(175, 75, 255), -1);
                        }
                    }
                    else
                    {
                        if (Main.netMode == 0) { Main.NewText(Language.GetTextValue("Announcement.HasAwoken", npcName), 175, 75, 255, false); }
                        else
                        if (Main.netMode == 2)
                        {
                            NetMessage.BroadcastChatMessage(NetworkText.FromKey("Announcement.HasAwoken", new object[]
                            {
                            NetworkText.FromLiteral(npcName)
                            }), new Color(175, 75, 255), -1);
                        }
                    }
                }
            }
            else
            {
                //I have no idea how to convert this to the standard system so im gonna post this method too lol
                AANet.SendNetMessage(AANet.SummonNPCFromClient, (byte)player.whoAmI, (short)bossType, spawnMessage, (int)npcCenter.X, (int)npcCenter.Y, overrideDisplayName, namePlural);
            }
        }

    }
}