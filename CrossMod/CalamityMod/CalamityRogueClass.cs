using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using System.Reflection;
using AAMod.Items;
using Microsoft.Xna.Framework;
using AAMod.CrossMod;

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
			if (ModLoader.GetMod("CalamityMod") != null)
			{
				float throwingDamage = (float) ModSupport.GetModPlayerConditions("CalamityMod", player, "CalamityPlayer", "throwingDamage", false, false);
				add += throwingDamage - 1f;
			}
		}
		public override void GetWeaponCrit(Player player, ref int crit)
		{
			if (ModLoader.GetMod("CalamityMod") != null)
			{
				int throwingCrit = (int) ModSupport.GetModPlayerConditions("CalamityMod", player, "CalamityPlayer", "throwingCrit", false, false);
				crit = item.crit + throwingCrit;
			}
		}
		public override float UseTimeMultiplier(Player player)
		{
			float num = 1f;
			if (ModLoader.GetMod("CalamityMod") != null)
			{
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
			}
			return num;
		}
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			if (ModLoader.GetMod("CalamityMod") != null)
			{
				TooltipLine tooltipLine = tooltips.FirstOrDefault((TooltipLine x) => x.Name == "Damage" && x.mod == "Terraria");
				if (tooltipLine != null)
				{
					string[] source = tooltipLine.text.Split(new char[]
					{
						' '
					});
					string str = source.First();
					string str2 = source.Last();
					tooltipLine.text = str + " rogue " + str2;
				}
			}
			else
			{
				TooltipLine error = new TooltipLine(mod, "Error", "WARNING: ITEM WILL NOT FUNCTION WITHOUT CALAMITY ENABLED!")
                {
                    overrideColor = new Color(255, 50, 50)
                };
                tooltips.Add(error);
			}
		}
		public override bool ConsumeItem(Player player)
		{
			if (ModLoader.GetMod("CalamityMod") != null)
			{
				bool throwingAmmoCost50 = (bool) ModSupport.GetModPlayerConditions("CalamityMod", player, "CalamityPlayer", "throwingAmmoCost50", false, false);
				bool throwingAmmoCost66 = (bool) ModSupport.GetModPlayerConditions("CalamityMod", player, "CalamityPlayer", "throwingAmmoCost66", false, false);
				return (!throwingAmmoCost50 || Main.rand.Next(1, 101) <= 50) && (!throwingAmmoCost66 || Main.rand.Next(1, 101) <= 66);
			}
			return base.ConsumeItem(player);
		}
    }

	public class RoguePlayer : ModPlayer
	{
		public float throwingDamage
        {
			get
			{
				if (ModLoader.GetMod("CalamityMod") != null)
                {
                    float? stealth = (float?) ModSupport.GetModPlayerConditions("CalamityMod", player, "CalamityPlayer", "throwingDamage", false, false);
                    if (stealth != null) return (float)stealth;
                }
                return 1f;
			}
			set
			{
				if (ModLoader.GetMod("CalamityMod") != null)
                {
					CrossMod.ModSupport.SetModPlayerConditions("CalamityMod", player, "CalamityPlayer", "throwingDamage", value, false, false);
				}
			}
		}

		public float throwingVelocity
        {
			get
			{
				if (ModLoader.GetMod("CalamityMod") != null)
                {
                    float? stealth = (float?) ModSupport.GetModPlayerConditions("CalamityMod", player, "CalamityPlayer", "throwingVelocity", false, false);
                    if (stealth != null) return (float)stealth;
                }
                return 1f;
			}
			set
			{
				if (ModLoader.GetMod("CalamityMod") != null)
                {
					CrossMod.ModSupport.SetModPlayerConditions("CalamityMod", player, "CalamityPlayer", "rogueStealthMax", value, false, false);
				}
			}
		}

		public int throwingCrit
        {
			get
			{
				if (ModLoader.GetMod("CalamityMod") != null)
                {
                    int? stealth = (int?) ModSupport.GetModPlayerConditions("CalamityMod", player, "CalamityPlayer", "throwingCrit", false, false);
                    if (stealth != null) return (int)stealth;
                }
                return 0;
			}
			set
			{
				if (ModLoader.GetMod("CalamityMod") != null)
                {
					CrossMod.ModSupport.SetModPlayerConditions("CalamityMod", player, "CalamityPlayer", "throwingCrit", value, false, false);
				}
			}
		}

		public float rogueStealth
        {
			get
			{
				if (ModLoader.GetMod("CalamityMod") != null)
                {
                    float? stealth = (float?) ModSupport.GetModPlayerConditions("CalamityMod", player, "CalamityPlayer", "rogueStealth", false, false);
                    if (stealth != null) return (float)stealth;
                }
                return 0f;
			}
			set
			{
				if (ModLoader.GetMod("CalamityMod") != null)
                {
					CrossMod.ModSupport.SetModPlayerConditions("CalamityMod", player, "CalamityPlayer", "rogueStealth", value, false, false);
				}
			}
		}

		public float rogueStealthMax
        {
			get
			{
				if (ModLoader.GetMod("CalamityMod") != null)
                {
                    float? stealth = (float?) ModSupport.GetModPlayerConditions("CalamityMod", player, "CalamityPlayer", "rogueStealthMax", false, false);
                    if (stealth != null) return (float)stealth;
                }
                return 0f;
			}
			set
			{
				if (ModLoader.GetMod("CalamityMod") != null)
                {
					CrossMod.ModSupport.SetModPlayerConditions("CalamityMod", player, "CalamityPlayer", "rogueStealthMax", value, false, false);
				}
			}
		}

		public bool StealthStrikeAvailable
        {
			get
			{
				if (ModLoader.GetMod("CalamityMod") != null)
                {
					Mod mod = ModLoader.GetMod("CalamityMod");
					ModPlayer modplayer = player.GetModPlayer(mod, "CalamityPlayer");
					MethodInfo StealthStrike = modplayer.GetType().GetMethod("StealthStrikeAvailable", BindingFlags.Instance | BindingFlags.Public);
                    bool? stealth = (bool?)StealthStrike.Invoke(modplayer, new object[]{});
                    if (stealth != null) return (bool)stealth;
                }
                return false;
			}
		}
	}

	public class RogueItem : GlobalItem
	{
		public override bool InstancePerEntity => true;
		public override bool CloneNewInstances => true;
		public bool rogue;

        public override void SetDefaults(Item item)
		{
			if (ModLoader.GetMod("CalamityMod") != null)
			{
				rogue = (bool) ModSupport.GetModGlobalItemConditions("CalamityMod", item, "CalamityGlobalItem", "rogue", false, false);
				CrossMod.ModSupport.SetModGlobalItemConditions("CalamityMod", item, "CalamityGlobalItem", "rogue", true, false, false);
			}
		}
	}

	public class RogueProj : GlobalProjectile
	{
		public override bool InstancePerEntity => true;
		public override bool CloneNewInstances => true;
		public bool rogue;
		public bool stealthStrike = false;
        public override void SetDefaults(Projectile projectile)
		{
			if (ModLoader.GetMod("CalamityMod") != null)
			{
				rogue = (bool) ModSupport.GetModGlobalProjConditions("CalamityMod", projectile, "CalamityGlobalProjectile", "rogue", false, false);
				CrossMod.ModSupport.SetModGlobalProjConditions("CalamityMod", projectile, "CalamityGlobalProjectile", "rogue", true, false, false);
			}
		}
	}
}