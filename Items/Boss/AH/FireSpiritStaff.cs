using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.Items.Boss.AH
{
    public class FireSpiritStaff : BaseAAItem
    {
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Flame Vortex Staff");	
            BaseUtility.AddTooltips(item, new string[] { "Conjures flaming spheres that increase your minion damage", "Each sphere takes up 1 minion slot", "You must have at least 2 open slots for the first summon" });			
		}		

        public override void SetDefaults()
        {
            item.width = 45;
            item.height = 18;
            item.maxStack = 1;
            item.rare = 9;
            AARarity = 12;
            item.value = BaseUtility.CalcValue(0, 20, 0, 0);
            item.useStyle = 1;
            item.useAnimation = 35;
            item.useTime = 35;
            item.UseSound = SoundID.Item20;
            item.autoReuse = true;
            item.noMelee = true;
            item.summon = true;
            item.shoot = mod.ProjType("FireOrbiter");
            item.shootSpeed = 5;			
        }
		
		public override void UseStyle(Player player)
		{
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
			{
				player.AddBuff(mod.BuffType("Orbiters"), 2, true);
			}
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            bool AnyOrbiters = AAGlobalProjectile.AnyProjectiless(mod.ProjectileType<Projectiles.AH.FireOrbiter>());
            int SummonCount = 2;
            if (AnyOrbiters)
            {
                SummonCount = 1;
            }
            for (int Loops = 0; Loops < SummonCount; Loops++)
            {
                Projectile.NewProjectile(position, new Vector2(speedX, speedY), type, damage, knockBack, Main.myPlayer, 0, 0);
            }

            return false;
        }
    }
}