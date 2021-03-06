﻿using RimWorld;
using Verse;

namespace Religion
{
    internal class ThoughtWorker_SameReligion : ThoughtWorker
    {
        protected override ThoughtState CurrentSocialStateInternal(Pawn p, Pawn other)
        {
            if (!p.RaceProps.Humanlike)
            {
                return false;
            }

            if (!RelationsUtility.PawnsKnowEachOther(p, other))
            {
                return false;
            }

            if (other.def != p.def)
            {
                return false;
            }

            if (!p.story.traits.allTraits.Any(x => x.def is TraitDef_ReligionTrait))
            {
                return false;
            }

            var t = p.story.traits.allTraits.Find(x => x.def is TraitDef_ReligionTrait).def;
            if (other.story.traits.HasTrait(t))
            {
                return true;
            }

            return false;
        }
    }
}