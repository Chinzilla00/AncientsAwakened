using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class Mjolnir : BaseAAItem
    {
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Forged by gnomes for the legendary Asgardian warrior"
			+"\nWield it wisely");
        }

		public override void SetDefaults()
		{
			item.noMelee = true;
			item.useStyle = 1;
			item.shootSpeed = 16f;
			item.damage = 135;
			item.knockBack = 9f;
			item.width = 14;
			item.height = 28;
			item.UseSound = SoundID.Item1;
			item.useAnimation = 10;
			item.useTime = 10;
			item.noUseGraphic = true;
			item.rare = 9;
			item.value = Item.sellPrice(0, 25, 0, 0);
			item.melee = true;
			item.shoot = mod.ProjectileType("Mjolnir");
			item.autoReuse = true;
		}
		
		public override bool CanUseItem(Player player)
		{
			if (player.ownedProjectileCounts[item.shoot] < 1)
			{
				return true;
			}
			return false;
		}
	}
}
