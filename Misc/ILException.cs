using System;

namespace AAMod.Misc
{
    public class ILException : Exception
    {
        public override string Message => "An IL edit has encountered an error. Please contact the devs on the discord, linked below here: https://discord.gg/gGXJXXv";
    }
}
