using Terraria;
using Terraria.ID;

namespace AAMod.Items.Boss.Rajah.Supreme
{
    public class Excalihare : BaseAAItem
	{
		public override void SetDefaults()
		{
			item.damage = 400;
			item.melee = true;
			item.width = 80;
			item.height = 80;
			item.useTime = 27;
			item.useAnimation = 27;
			item.useStyle = 1;
            item.value = Item.sellPrice(0, 50, 0, 0);
            item.rare = 9;
            AARarity = 14;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("Excalihare");
            item.scale = 1.1f;
            item.shootSpeed = 14f;
            item.knockBack = 6.5f;
            item.expert = true;
		}
        
		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(mod.BuffType<Buffs.InfinityOverload>(), 120);
        }
    }
}
