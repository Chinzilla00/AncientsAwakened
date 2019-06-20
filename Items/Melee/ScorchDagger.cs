using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class ScorchDagger : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Scorch Dagger");
		}
		public override void SetDefaults()
		{
			item.damage = 26;
			item.melee = true;
			item.width = 34;
			item.height = 34;
			item.useTime = 7;
			item.useAnimation = 7;
			item.useStyle = 3;
			item.knockBack = 3;
			item.value = 2000;
			item.rare = 1;
			item.UseSound = SoundID.Item1;
			item.autoReuse = false;
        }
		
		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 300);
        }
	}
}
