using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;

namespace AAMod.Items.Boss.Anubis.Forsaken
{
    public class ArtifactOfGuilt : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Artifact of Guilt");
            Tooltip.SetDefault(@"Taking damage builds a charge in the Artifact
Reaching a charge of 250 will summon an ''Eye of the Forsaken'' and reset the charge value
You will get major damage and speed boosts while Eye is active");
            
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 34;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = 11;
            item.accessory = true;
            item.expert = true;
            item.expertOnly = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<AAPlayer>().artifactGuilt = true;
        }
		
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			Player player = Main.player[Main.myPlayer];
			string text1 = Language.GetTextValue("Mods.AAMod.Common.ArtifactOfGuiltInfo") + " " + player.GetModPlayer<AAPlayer>().artifactGuiltCharge;
            TooltipLine line = new TooltipLine(mod, "text1", text1)
            {
                overrideColor = Color.Yellow
            };
            tooltips.Insert(2,line);
		}
    }
}