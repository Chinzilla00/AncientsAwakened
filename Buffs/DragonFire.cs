using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class DragonFire : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Dragon Fire");
			Description.SetDefault("Reduced Damage");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			longerExpertDebuff = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
            player.GetModPlayer<AAPlayer>(mod).dragonFire = true;
        }

		
	}
    public class DragonFireDamageReduction : GlobalNPC
    {
        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (Main.player[projectile.owner].HasBuff(mod.BuffType("DragonFire")))
            {
                damage = (int)(damage * .8f); //this is a better way to reduce the player's damage from debuff since this will effect already summoned minions
            }
        }
        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            if (player.HasBuff(mod.BuffType("DragonFire")))
            {
                damage = (int)(damage * .8f);
            }
        }
        public override void ModifyHitPlayer(NPC npc, Player target, ref int damage, ref bool crit)
        {
            if (npc.HasBuff(mod.BuffType("DragonFire")))
            {
                damage -= 10;
                if(damage < 0)
                {
                    damage = 0;
                }
            }
        }
    }
}
