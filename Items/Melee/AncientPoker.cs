using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class AncientPoker : BaseAAItem
    {
        public override void SetStaticDefaults()
		{
		DisplayName.SetDefault("Aqua Lance");
		Tooltip.SetDefault("");
		}
        public override void SetDefaults()
        {
            item.damage = 31;
            item.melee = true;
            item.width = 66;
            item.height = 64;
            item.scale = 1.1f;
            item.maxStack = 1;
            item.useTime = 22;
            item.useAnimation = 22;
            item.knockBack = 5f;
            item.UseSound = SoundID.Item1;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.useTurn = true;
			item.autoReuse = true;
            item.useStyle = 5;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = 3;
            item.shoot = mod.ProjectileType("APP");  //put your Spear projectile name
            item.shootSpeed = 4f;
        }
		public override bool CanUseItem(Player player)
		{
			return player.ownedProjectileCounts[item.shoot] < 1; // This is to ensure the spear doesn't bug out when using autoReuse = true
		}
    }
}
