using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace AAMod
{
    public class CalamityGlobalNPC : GlobalNPC
    {
        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            if (ModSupport.GetMod("CalamityMod") != null)
			{
                if (npc.type > 580 && npc.modNPC.mod == ModLoader.GetMod("AAMod"))
                {
                    if (item.type > 3930 && item.modItem.mod == ModLoader.GetMod("CalamityMod"))
                    {
                        damage = (int)(damage * .4f);
                    }
                }
            }
        }

        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
            if (ModSupport.GetMod("CalamityMod") != null)
			{
                if (npc.type > 580 && npc.modNPC.mod == ModLoader.GetMod("AAMod"))
                {
                    if (projectile.type > 714 && projectile.modProjectile.mod == ModLoader.GetMod("CalamityMod"))
                    {
                        damage = (int)(damage * .4f);
                    }
                }
            }
		}
    }
}
