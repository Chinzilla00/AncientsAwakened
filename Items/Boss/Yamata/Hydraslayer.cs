using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace AAMod.Items.Boss.Yamata   //where is located
{
    public class Hydraslayer : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Amenomuraku");
            Tooltip.SetDefault(@"Used to defeat the multi-headed monstrosities of the abyss
Inflicts Moonrazed");
        }

        
        public override void SetDefaults()
        {
            item.shoot = mod.ProjectileType("PhantomSword");
            item.damage = 220;            
            item.melee = true;            
            item.width = 86;              
            item.height = 86;             
            item.useTime = 13;          
            item.useAnimation = 13;     
            item.useStyle = ItemUseStyleID.SwingThrow;        
            item.knockBack = 3f;      
            item.value = Item.sellPrice(0, 30, 0, 0);
            item.UseSound = SoundID.Item20;      
            item.autoReuse = true;   
            item.useTurn = true;
            item.shootSpeed = 20f;
            item.rare = ItemRarityID.Cyan; AARarity = 13;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {

            float numberProjectiles = 1; // This defines how many projectiles to shot
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                numberProjectiles = 2 + Main.rand.Next(3);
            }

            float rotation = MathHelper.ToRadians(60);
            for (int i = 0; i < numberProjectiles; i++)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    position = new Vector2(player.position.X - ((Main.rand.Next(61) + 40) * player.direction), player.position.Y - (Main.rand.Next(91) - 40)); //this defines the distance of the projectiles form the player when the projectile spawns
                    Vector2 perturbedSpeed = Vector2.Normalize(new Vector2((Main.MouseWorld.X - position.X) + (Main.rand.Next(41) - 20), (Main.MouseWorld.Y - position.Y) + (Main.rand.Next(41) - 20))) * 15f; // This defines the projectile roatation and speed. .4f == projectile speed
                    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
                }
            }
            return false;
        }
    }
}