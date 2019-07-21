using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace AAMod.Items.Dev
{
    public class GrimReaperScythe : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Scythe of the Grim Reaper");
            Tooltip.SetDefault(@"Left click to swing and release homing scythe
Right click to do dashing hit
You are immune during the dash and deal 10x damage in true melee
Dashing ability has 10 seconds CD
'Well, how many Grim Reapers have you met before, mate?'
-Gregg");
        }

		public override void SetDefaults()
		{
			item.autoReuse = true;
			item.useStyle = 1;
			item.useAnimation = 30;
			item.useTime = 30;
			item.knockBack = 5f;
			item.width = 24;
			item.height = 28;
			item.damage = 150;
			item.crit = 14;
			item.scale = 1.15f;
			item.UseSound = SoundID.Item71;
			item.rare = 7;
			item.shoot = mod.ProjectileType("GrimReaperScythe");
			item.shootSpeed = 14f;
			item.value = 500000;
			item.melee = true;
		}
		
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		
		public override bool CanUseItem(Player player)
		{
			int side = player.direction;
			if (player.altFunctionUse != 2)
			{
				item.shoot = mod.ProjectileType("GrimReaperScythe");
				return true;
			}
			if (player.altFunctionUse == 2 && !player.HasBuff(mod.BuffType("ReaperCD")))
			{
				player.AddBuff(mod.BuffType("ReaperImmune"), 60);
				player.AddBuff(mod.BuffType("ReaperCD"), 600);
				item.shoot = mod.ProjectileType("ReaperHitbox");
				player.velocity.X = 26f * side;
				return true;
			}
			else
			{
				return false;
			}
		}
		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (type == mod.ProjectileType("GrimReaperScythe") && player.HasBuff(mod.BuffType("ReaperImmune")))
			{
				damage /= 10;
			}
			return true;
		}
	}
}
