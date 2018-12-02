using System.Collections.Generic;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using BaseMod;

namespace AAMod.Items.BossSummons
{
    public class DreadSigil : ModItem
	{

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dread Moon Sigil");
            Tooltip.SetDefault(@"A ragged old tablet said to contain the dark magic of a new moon
Summons Yamata
Only Usable at night");
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
                    line2.overrideColor = new Color(45, 46, 70);
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
			SpawnBoss(player, "Yamata", "Yamata; Dread Nightmare");
			Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
            if (!AAWorld.downedYamata && !Main.expertMode)
            {
                Main.NewText("You DARE enter my territory, Terrarian?! NYEHEHEHEHEH..! Big mistake..!", new Color(45, 46, 70));
            }
            if (!AAWorld.downedYamataA && Main.expertMode)
            {
                Main.NewText("You DARE enter my territory, Terrarian?! NYEHEHEHEHEH..! Big mistake..!", new Color(45, 46, 70));
            }
            if (!Main.expertMode && AAWorld.downedYamata)
            {
                Main.NewText("Back for more..?! This time you won’t be so lucky you little whelp..!", new Color(45, 46, 70));
            }
            if (Main.expertMode && AAWorld.downedYamata)
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

		public void SpawnBoss(Player player, string name, string displayName)
		{
			if (Main.netMode != 1)
			{
				int bossType = mod.NPCType(name);
				if(NPC.AnyNPCs(bossType)){ return; } //don't spawn if there's already a boss!
				int npcID = NPC.NewNPC((int)player.Center.X, (int)player.Center.Y, bossType, 0);
				Main.npc[npcID].Center = player.Center - new Vector2(MathHelper.Lerp(-100f, 100f, (float)Main.rand.NextDouble()), 800f);
				Main.npc[npcID].netUpdate2 = true;
				string npcName = (!string.IsNullOrEmpty(Main.npc[npcID].GivenName) ? Main.npc[npcID].GivenName : displayName);
				if (Main.netMode == 0){ Main.NewText(Language.GetTextValue("Announcement.HasAwoken", npcName), 175, 75, 255, false); }else 
				if (Main.netMode == 2)
				{
					NetMessage.BroadcastChatMessage(NetworkText.FromKey("Announcement.HasAwoken", new object[]
					{
						NetworkText.FromLiteral(npcName)
					}), new Color(175, 75, 255), -1);
				}
			}
		}	

		public override void UseStyle(Player p) { BaseMod.BaseUseStyle.SetStyleBoss(p, item, true, true); }
		public override bool UseItemFrame(Player p) { BaseMod.BaseUseStyle.SetFrameBoss(p, item); return true; }		
	}
}