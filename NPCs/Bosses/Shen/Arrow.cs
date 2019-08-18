
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace AAMod.NPCs.Bosses.Shen
{
	public class Arrow : ModProjectile
	{
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Arrow");
		}

		public override void SetDefaults()
		{
			projectile.width = 64;
			projectile.light = 2f;
			projectile.height = 64;
			projectile.alpha = 0;
			projectile.timeLeft = 280;
			projectile.penetrate = 1;
			projectile.hostile = false;
			projectile.magic = true;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			
		}
		public int Timer;
		public static volatile float Prooc;
		public static volatile float Proocc;
	
		public bool down;
		
private void LookInDirectionP(Vector2 look)
		{
			float angle = 0.5f * (float)Math.PI;
			if (look.X != 0f)
			{
				angle = (float)Math.Atan(look.Y / look.X);
			}
			else if (look.Y < 0f)
			{
				angle += (float)Math.PI;
			}
			if (look.X < 0f)
			{
				angle += (float)Math.PI;
			}
			projectile.rotation = angle;
		}
	
		
		public override void AI()
		{
			
			Timer++;
			if (Timer >=180){
				projectile.velocity.X = 0;
				projectile.velocity.Y = 0;
			}
			if (Timer == 180){
				
		projectile.alpha = 0;
			}
			if (Timer == 190){
				
		projectile.alpha = 255;
			}
			if (Timer == 200){
				
		projectile.alpha = 0;
			}
			if (Timer == 210){
				
		projectile.alpha = 255;
			}
			if (Timer == 215){
				
		projectile.alpha = 0;
			}
			if (Timer == 220){	
		projectile.alpha = 255;
		
			Prooc = projectile.position.X;
			Prooc = projectile.position.Y;
			
			}
			 
			
			//LookInDirectionP(projectile.velocity);
		   if (Timer <=180){
          for(int i = 0; i < 200; i++)
    {
       NPC target = Main.npc[i];
	   Player player = Main.player[projectile.owner];
       //If the npc is hostile
     
           //Get the shoot trajectory from the projectile and target
           float shootToX = player.position.X + player.width * 0.5f - projectile.Center.X;
           float shootToY = player.position.Y - projectile.Center.Y+400;
           float distance = (float)System.Math.Sqrt(shootToX * shootToX + shootToY * shootToY);

           //If the distance between the live targeted npc and the projectile is less than 480 pixels
           if(distance < 480f && player.active)
           {
               //Divide the factor, 3f, which is the desired velocity
               distance = 3f / distance;
   
               //Multiply the distance by a multiplier if you wish the projectile to have go faster
               shootToX *= distance * 6f;
               shootToY *= distance * 6f;

               //Set the velocities to the shoot values
               projectile.velocity.X = shootToX;
               projectile.velocity.Y = shootToY;
}

}
		   }
		}
		public override void OnHitPlayer(Player player, int damage, bool crit)
        {
           projectile.timeLeft = 0;		//this make so when the projectile/flame hit a npc, gives it the buff  onfire , 80 = 3 seconds
        }
		
		
	}
}

