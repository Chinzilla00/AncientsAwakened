using Terraria;
using Terraria.ID;

namespace AAMod.Items.Boss.Rajah.Supreme
{
    public class BaneOfTheBunnyEX : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Bane of the Slaughterer");
            Tooltip.SetDefault(@"Right click to use as a spear
Left click to use as a javelin
Throwing Javelins right after a spear thrust throws javelins faster for a moment
Bane of the Bunny EX");
		}

		public override void SetDefaults()
		{
            item.damage = 400;
            item.melee = true;
            item.width = 92; 
            item.height = 92;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.channel = true;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.knockBack = 4f;
            item.UseSound = SoundID.Item1;
            item.value = Item.sellPrice(1, 0, 0, 0);
            item.shoot = mod.ProjectileType("BaneEX");
            item.rare = ItemRarityID.Cyan;
            item.expert = true; item.expertOnly = true;
            item.useAnimation = 13;
            item.useTime = 13;
            item.autoReuse = true;
            item.shootSpeed = 12f;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.useAnimation = 11;
                item.useTime = 11;
                item.useStyle = ItemUseStyleID.HoldingOut;
                item.shoot = mod.ProjectileType("BaneEX");  
            }
            else
            {
                item.useAnimation = 13;
                item.useTime = 13;
                item.useStyle = ItemUseStyleID.SwingThrow;
                item.shoot = mod.ProjectileType("BaneTEX");
            }
            return base.CanUseItem(player);
        }
    }
}