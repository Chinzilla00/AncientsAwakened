using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class DarkmatterJackhammer : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Darkmatter Jackhammer");
        }

		public override void SetDefaults()
		{
			item.damage = 60;
			item.melee = true;
			item.width = 52;
            item.height = 22;
			item.useTime = 7;
			item.useAnimation = 15;
            item.channel = true;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.hammer = 120;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 6;
			item.value = 550000;
            item.UseSound = SoundID.Item23;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("DarkmatterJackhammerPro");
            item.shootSpeed = 40f;
            item.tileBoost += 1;
            item.rare = ItemRarityID.Cyan;
            AARarity = 12;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity12;
                }
            }
        }
    }
}
