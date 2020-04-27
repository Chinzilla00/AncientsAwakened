using Terraria.ID;

namespace AAMod.Items.Melee
{
    public class Pyrosphere : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pyrosphere");			
		}		
		
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 30;
            item.maxStack = 1;
            item.rare = 2;
            item.value = BaseUtility.CalcValue(0, 0, 90, 50);
            item.useStyle = 5;
            item.useAnimation = 45;
            item.useTime = 45;
            item.UseSound = SoundID.Item1;
            item.damage = 15;
            item.knockBack = 7;
            item.melee = true;
            item.shoot = Terraria.ModLoader.ModContent.ProjectileType<Projectiles.Pyrosphere>();
            item.shootSpeed = 10;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.channel = true;		
        }
	}
}