using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace AAMod.Items.DevTools
{
    public class RogueTest : RogueWeapon
    {
        public override string Texture => "AAMod/Items/DevTools/NoodleSword";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("[DEV] Rogue Noodle Sword");
            Tooltip.SetDefault(@"Top 10 op weapons in video games");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 10000;
            item.width = 64;            
            item.height = 70;         
            item.useTime = 17;   
            item.useAnimation = 17;     
            item.useStyle = 1;       
            item.knockBack = 4;   
            item.value = 0;        
            item.rare = 11;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;   
            item.useTurn = true;
            item.expert = true; item.expertOnly = true;
			item.shoot = mod.ProjectileType("Noodle");
			item.shootSpeed = 9f;
            ModSupport.SetModGlobalItemConditions("CalamityMod", item, "CalamityGlobalItem", "rogue", true, false, false); //Set rogue damage
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
            if (ModSupport.GetMod("CalamityMod") != null)
            {
                int num = Projectile.NewProjectile(position, new Vector2(speedX, speedY), mod.ProjectileType("Noodle"), damage, knockBack, player.whoAmI, 0f, 1f);
                ModSupport.SetModGlobalProjConditions("CalamityMod", Main.projectile[num], "CalamityGlobalProjectile", "rogue", true, false, false);
                if (player.GetModPlayer<RoguePlayer>().StealthStrikeAvailable) //Stealth Strike
                {
                    float scaleFactor = 15f;
                    int num5 = 25;
                    Projectile.NewProjectile(player.position, new Vector2(1f, 0f) * scaleFactor, mod.ProjectileType("Noodle"), num5, 2f, player.whoAmI, 0f, 0f);
                    Projectile.NewProjectile(player.position, new Vector2(0f, 1f) * scaleFactor, mod.ProjectileType("Noodle"), num5, 2f, player.whoAmI, 0f, 0f);
                    Projectile.NewProjectile(player.position, new Vector2(-1f, 0f) * scaleFactor, mod.ProjectileType("Noodle"), num5, 2f, player.whoAmI, 0f, 0f);
                    Projectile.NewProjectile(player.position, new Vector2(0f, -1f) * scaleFactor, mod.ProjectileType("Noodle"), num5, 2f, player.whoAmI, 0f, 0f);
                    Projectile.NewProjectile(player.position, Vector2.Normalize(new Vector2(1f, 1f)) * scaleFactor, mod.ProjectileType("Noodle"), num5, 2f, player.whoAmI, 0f, 0f);
                    Projectile.NewProjectile(player.position, Vector2.Normalize(new Vector2(1f, -1f)) * scaleFactor, mod.ProjectileType("Noodle"), num5, 2f, player.whoAmI, 0f, 0f);
                    Projectile.NewProjectile(player.position, Vector2.Normalize(new Vector2(-1f, -1f)) * scaleFactor, mod.ProjectileType("Noodle"), num5, 2f, player.whoAmI, 0f, 0f);
                    Projectile.NewProjectile(player.position, Vector2.Normalize(new Vector2(-1f, 1f)) * scaleFactor, mod.ProjectileType("Noodle"), num5, 2f, player.whoAmI, 0f, 0f);
                }
                return false;
            }
            return true;
        }

        public override void UpdateInventory(Player player)
        {
            if (ModSupport.GetMod("CalamityMod") == null)
            {
                item.TurnToAir();
            }
        }
    }
}
