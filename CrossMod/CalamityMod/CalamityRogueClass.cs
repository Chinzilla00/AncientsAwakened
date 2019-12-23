using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using System.Reflection;
using AAMod.Items;

namespace AAMod
{
    public abstract class RogueWeapon : BaseAAItem
	{
        public virtual void SafeSetDefaults()
		{
		}
		public override void SetDefaults()
		{
			SafeSetDefaults();
			item.melee = false;
			item.ranged = false;
			item.magic = false;
			item.thrown = true;
			item.summon = false;
		}
		public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
		{
			if (ModSupport.GetMod("CalamityMod") != null)
			{
				float throwingDamage = (float) ModSupport.GetModPlayerConditions("CalamityMod", player, "CalamityPlayer", "throwingDamage", false, false);
				add += throwingDamage - 1f;
			}
		}
		public override void GetWeaponCrit(Player player, ref int crit)
		{
			if (ModSupport.GetMod("CalamityMod") != null)
			{
				int throwingCrit = (int) ModSupport.GetModPlayerConditions("CalamityMod", player, "CalamityPlayer", "throwingCrit", false, false);
				crit = item.crit + throwingCrit;
			}
		}
		public override float UseTimeMultiplier(Player player)
		{
			if (ModSupport.GetMod("CalamityMod") != null)
			{
				float num = 1f;
				bool gloveOfPrecision = (bool) ModSupport.GetModPlayerConditions("CalamityMod", player, "CalamityPlayer", "gloveOfPrecision", false, false);
				bool gloveOfRecklessness = (bool) ModSupport.GetModPlayerConditions("CalamityMod", player, "CalamityPlayer", "gloveOfRecklessness", false, false);
				if (gloveOfPrecision)
				{
					num -= 0.2f;
				}
				if (gloveOfRecklessness)
				{
					num += 0.2f;
				}
				return num;
			}
		}
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			if (ModSupport.GetMod("CalamityMod") != null)
			{
				TooltipLine tooltipLine = tooltips.FirstOrDefault((TooltipLine x) => x.Name == "Damage" && x.mod == "Terraria");
				if (tooltipLine != null)
				{
					string[] source = tooltipLine.text.Split(new char[]
					{
						' '
					});
					string str = source.First<string>();
					string str2 = source.Last<string>();
					tooltipLine.text = str + " rogue " + str2;
				}
			}
		}
		public override bool ConsumeItem(Player player)
		{
			if (ModSupport.GetMod("CalamityMod") != null)
			{
				bool throwingAmmoCost50 = (bool) ModSupport.GetModPlayerConditions("CalamityMod", player, "CalamityPlayer", "throwingAmmoCost50", false, false);
				bool throwingAmmoCost66 = (bool) ModSupport.GetModPlayerConditions("CalamityMod", player, "CalamityPlayer", "throwingAmmoCost66", false, false);
				return (!throwingAmmoCost50 || Main.rand.Next(1, 101) <= 50) && (!throwingAmmoCost66 || Main.rand.Next(1, 101) <= 66);
			}
		}
    }

	public class RoguePlayer : ModPlayer
	{
		public float rogueStealth
        {
			get
			{
				if (ModSupport.GetMod("CalamityMod") != null)
                {
                    float? stealth = (float?) ModSupport.GetModPlayerConditions("CalamityMod", player, "CalamityPlayer", "rogueStealth", false, false);
                    if (stealth != null) return (float)stealth;
                }
                return 0f;
			}
			set
			{
				if (ModSupport.GetMod("CalamityMod") != null)
                {
					ModSupport.SetModPlayerConditions("CalamityMod", player, "CalamityPlayer", "rogueStealth", value, false, false);
				}
			}
		}

		public float rogueStealthMax
        {
			get
			{
				if (ModSupport.GetMod("CalamityMod") != null)
                {
                    float? stealth = (float?) ModSupport.GetModPlayerConditions("CalamityMod", player, "CalamityPlayer", "rogueStealthMax", false, false);
                    if (stealth != null) return (float)stealth;
                }
                return 0f;
			}
			set
			{
				if (ModSupport.GetMod("CalamityMod") != null)
                {
					ModSupport.SetModPlayerConditions("CalamityMod", player, "CalamityPlayer", "rogueStealthMax", value, false, false);
				}
			}
		}

		public bool StealthStrikeAvailable
        {
			get
			{
				if (ModSupport.GetMod("CalamityMod") != null)
                {
					Mod mod = ModSupport.GetMod("CalamityMod");
					ModPlayer modplayer = player.GetModPlayer(mod, "CalamityPlayer");
					MethodInfo StealthStrike = modplayer.GetType().GetMethod("StealthStrikeAvailable", BindingFlags.Instance | BindingFlags.Public);
                    bool? stealth = (bool?)StealthStrike.Invoke(modplayer, new object[]{});
                    if (stealth != null) return (bool)stealth;
                }
                return false;
			}
		}
	}
}