using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria;
using System.Collections.Generic;

namespace AAMod.Items.Boss.Rajah
{
    public class RajahSash : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rajah Rabbit's Sash of Vengeance");
            Tooltip.SetDefault(@"Every 10% of health lost gives you 8% extra attack power to your highest damage type boost
40% increased movement speed
Increased Jump Height and Speed
Grants Autojump
Immunity to fall damage");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.value = Item.sellPrice(0, 12, 0, 0);
            item.rare = 9;
            item.accessory = true;
            item.expertOnly = true;
            item.expert = true; item.expertOnly = true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player player = Main.player[item.owner];
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
            Color damageColor = Color.Firebrick;
            string DamageType = "";
            if (modPlayer.MeleeHighest(player))
            {
                DamageType = Lang.RajahSPTooltip("Melee");
                damageColor = Color.Firebrick;
            }
            else if (modPlayer.RangedHighest(player))
            {
                DamageType = Lang.RajahSPTooltip("Ranged");
                damageColor = Color.SeaGreen;
            }
            else if (modPlayer.MagicHighest(player))
            {
                DamageType = Lang.RajahSPTooltip("Magic");
                damageColor = Color.Violet;
            }
            else if (modPlayer.SummonHighest(player))
            {
                DamageType = Lang.RajahSPTooltip("Summoning");
                damageColor = Color.Cyan;
            }
            else if (modPlayer.ThrownHighest(player))
            {
                DamageType = Lang.RajahSPTooltip("Throwing");
                damageColor = Color.DarkOrange;
            }

            string DamageAmmount = (10 * DamageBoost(player)) + "% ";

            TooltipLine DamageToltip = new TooltipLine (mod, "Damage Type", Lang.RajahSPTooltip("CurrentDamageBoost:+") + DamageAmmount + DamageType + Lang.RajahSPTooltip("Damage"))
            {
                overrideColor = damageColor
            };

            tooltips.Add(DamageToltip);
            base.ModifyTooltips(tooltips);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.wingTime > 0)
            {
                player.wingTime += 3;
            }
        }

        public override void UpdateEquip(Player player)
        {
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
            player.autoJump = true;
            Player.jumpHeight = 40;
            player.jumpSpeedBoost += 3.6f;
            player.noFallDmg = true;
            player.moveSpeed *= 1.4f;
            if (modPlayer.MeleeHighest(player))
            {
                player.meleeDamage += DamageBoost(player); 
            }
            else if (modPlayer.RangedHighest(player))
            {
                player.rangedDamage += DamageBoost(player);
            }
            else if (modPlayer.MagicHighest(player))
            {
                player.magicDamage += DamageBoost(player);
            }
            else if (modPlayer.SummonHighest(player))
            {
                player.minionDamage += DamageBoost(player);
            }
            else if (modPlayer.ThrownHighest(player))
            {
                player.thrownDamage += DamageBoost(player);
            }
        }

        public float DamageBoost(Player player)
        {
            if (player.statLife <= player.statLifeMax * .1f)
            {
                return .72f;
            }
            if (player.statLife <= player.statLifeMax * .2f)
            {
                return .64f;
            }
            if (player.statLife <= player.statLifeMax * .3f)
            {
                return .56f;
            }
            if (player.statLife <= player.statLifeMax * .4f)
            {
                return .48f;
            }
            if (player.statLife <= player.statLifeMax * .5f)
            {
                return .4f;
            }
            if (player.statLife <= player.statLifeMax * .6f)
            {
                return .32f;
            }
            if (player.statLife <= player.statLifeMax * .7f)
            {
                return .24f;
            }
            if (player.statLife <= player.statLifeMax * .8f)
            {
                return .16f;
            }
            if (player.statLife <= player.statLifeMax * .9f)
            {
                return .8f;
            }
            return 0f;
        }
    }
}