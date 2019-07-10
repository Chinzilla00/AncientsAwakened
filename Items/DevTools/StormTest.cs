using Microsoft.Xna.Framework;
using Terraria;
namespace AAMod.Items.DevTools
{
    public class StormTest : BaseAAItem
	{
		public override void SetStaticDefaults()
		{	
			DisplayName.SetDefault("[DEV] Storm Test");
            BaseMod.BaseUtility.AddTooltips(item, new string[] { "Tests Shen Doragon's lightning breath" });					
		}			
		
        public override void SetDefaults()
        {
            item.damage = 10000;
            item.melee = true;
            item.width = 64;
            item.height = 70;
            item.useTime = 60;
            item.useAnimation = 60;
            item.useStyle = 1;
            item.knockBack = 4;
            item.value = 0;
            item.rare = 11;
            item.autoReuse = true;
            item.useTurn = true;
            item.expert = true;
            item.shoot = mod.ProjectileType("ChaosLightning");
            item.shootSpeed = 9f;
        }
		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			Vector2 vector = new Vector2(speedX, speedY);
			int L = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("ChaosLightning"), damage, knockBack, player.whoAmI, vector.ToRotation());
			Main.projectile[L].penetrate = -1;
			Main.projectile[L].hostile = false;
			Main.projectile[L].friendly = true;
			Main.projectile[L].melee = true;
			Main.projectile[L].usesLocalNPCImmunity = true;
			Main.projectile[L].localNPCHitCooldown = -1;
			return false;
		}
    }
}