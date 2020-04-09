using Terraria;
using Terraria.ID;
using BaseMod;

namespace AAMod.Items.Boss.Anubis.Forsaken
{
    public class SoulCrook : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Crook of the Soul Judge");	
            BaseUtility.AddTooltips(item, new string[] { "Phases through tiles", "Every hit the crook makes heals you when it returns" });			
		}

        public override void SetDefaults()
        {
            item.width = 50;
            item.height = 50;
            item.useStyle = 1;
            item.useAnimation = 15;
            item.useTime = 15;
            item.UseSound = SoundID.Item1;
            item.damage = 160;
            item.knockBack = 10;
            item.melee = true;
            item.autoReuse = true;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.shoot = mod.ProjType("Crook");
            item.shootSpeed = 15;
            item.rare = 11;
        }

        public override bool CanUseItem(Player player)       //this make that you can shoot only 1 boomerang at once
        {
            for (int i = 0; i < 1000; ++i)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == item.shoot)
                {
                    return false;
                }
            }
            return true;
        }
	}
}