using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using System;

namespace AAMod.Items.Dev.Invoker
{
    [AutoloadEquip(EquipType.HandsOff)]
	public class InvokerBookEX : BaseAAItem
	{
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("The Book of the Law");
            Tooltip.SetDefault(@"A Legendary Book of Aleister 'Mega Therion'.
Increase 87% minion damage
Increase 2 minion slots
Get all of the materials' effect
");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            string text = "";
            text += Language.GetTextValue("Mods.AAMod.Common.InvokerBookEX1");
            
            if(!Main.player[Main.myPlayer].GetModPlayer<InvokerPlayer>().DarkCaligula)
            text += Language.GetTextValue("Mods.AAMod.Common.InvokerBookEX2");
            else
            text += Language.GetTextValue("Mods.AAMod.Common.InvokerBookEX3");

            TooltipLine line = new TooltipLine(mod, "newtooltip", text);
            list.RemoveAt(2);
            list.Insert(2,line);

            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = Color.Gold;
                }
            }
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 20;
            item.rare = 11;
            item.accessory = true;
            item.useStyle = 4;
            item.useTime = 1;
            item.expertOnly = true;
            item.useTime = 30;
            item.useAnimation = 30;
        }

        public override bool CanUseItem(Player player)
		{
            bool canuse = false;
            if(player.GetModPlayer<InvokerPlayer>().SpringInvoker && player.GetModPlayer<InvokerPlayer>().DarkCaligula) 
            {
                canuse = player.GetModPlayer<InvokerPlayer>().DarkCaligula;
            }
            return canuse;
        }

        public override bool UseItem(Player player)
		{
            if(player.GetModPlayer<InvokerPlayer>().SpringInvoker && player.GetModPlayer<InvokerPlayer>().DarkCaligula) 
            {
                player.GetModPlayer<InvokerPlayer>().DarkCaligula = false;
                player.AddBuff(mod.BuffType("InvokedCaligulaSafe"), 3600);
            }
            return true;
        }
        
        public override void UpdateAccessory(Player player, bool hideVisual)
		{
            player.minionDamage += .87f;
            player.maxMinions += 2;
            player.minionKB += 2f;

			player.lifeRegen += 26;
            player.lifeRegenTime += 2;

            player.maxTurrets++;

            player.longInvince = true;
            
            player.buffImmune[46] = true;
            player.noKnockback = true;
            player.fireWalk = true;
            player.buffImmune[33] = true;
            player.buffImmune[36] = true;
            player.buffImmune[30] = true;
            player.buffImmune[20] = true;
            player.buffImmune[32] = true;
            player.buffImmune[31] = true;
            player.buffImmune[35] = true;
            player.buffImmune[23] = true;
            player.buffImmune[22] = true;
            

            InvokerPlayer InvokerPlayer = InvokerPlayer.ModPlayer(player);
            //InvokerPlayer.BanishProjClear = true;  //This need change.
            InvokerPlayer.Thebookoflaw = true;
            InvokerPlayer.SpringInvoker = true;
            if(!hideVisual) InvokerPlayer.InvokerShow = true;
            InvokerPlayer.BanishDamageMult += 4.5f;
            InvokerPlayer.BanishLimit += 5;
        }

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "InvokerBook", 1);
            recipe.AddIngredient(mod, "InvokerHood", 1);
            recipe.AddIngredient(mod, "InvokerRobe", 1);
            recipe.AddIngredient(mod, "InvokerPants", 1);
			recipe.AddIngredient(ItemID.SquireGreatHelm, 1);
            recipe.AddIngredient(ItemID.SquireAltShirt, 1);
			recipe.AddIngredient(ItemID.ShinyStone, 1);
            recipe.AddIngredient(ItemID.FrozenTurtleShell, 1);
            recipe.AddIngredient(ItemID.CrossNecklace, 1);
            recipe.AddIngredient(ItemID.AnkhShield, 1);
            recipe.AddIngredient(ItemID.PaladinsShield, 1);
            recipe.AddIngredient(ItemID.SpectreBar, 60);
            recipe.AddIngredient(mod, "EXSoul", 1);
			recipe.AddTile(mod, "ACS");
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}