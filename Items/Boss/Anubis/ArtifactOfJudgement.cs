using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Localization;

namespace AAMod.Items.Boss.Anubis
{
    public class ArtifactOfJudgement : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Artifact of Judgement");
            Tooltip.SetDefault(@"Building charge while you are taking damage
Reaching 250 charge will summon an ''Eye of Judgement'' and reset charge value
You will get major damage and speed boosts while Eye is active");
            
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 34;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = 11;
            item.accessory = true;
            
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<AAPlayer>().artifactJudgement = true;
        }
		
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			Player player = Main.player[Main.myPlayer];
			string text1 = "Charge is " + (int)player.GetModPlayer<AAPlayer>().artifactJudgementCharge;
			TooltipLine line = new TooltipLine(mod, "text1", text1);
			line.overrideColor = Color.Yellow;
			tooltips.Insert(5,line);
		}
    }
}