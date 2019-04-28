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
            item.useStyle = 500;
            item.consumable = false;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Yamata;;
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
                /*if (!AAWorld.downedYamata)
                {
                    if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("You NEED to use that sigil on the altar at the center of the mire! Trust me, nothing bad will happen!", new Color(45, 46, 70), false);
                    return false;
                }*/
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
            int SpawnX = (int)MathHelper.Lerp(-1000, 1000, (float)Main.rand.NextDouble());
            int num = NPC.NewNPC(SpawnX, (int)(player.position.Y - 50), mod.NPCType(name), 0, 0f, 0f, 0f, 0f, 255);
            if (Main.netMode == 2)
            {
                NetMessage.SendData(23, -1, -1, null, num, 0f, 0f, 0f, 0, 0, 0);
            }
        }
	}
}