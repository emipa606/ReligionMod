﻿using System.Collections.Generic;
using System.Diagnostics;
using Verse;
using Verse.AI;
using RimWorld;

namespace Religion
{
    class JobDriver_HoldLecture : JobDriver
    {
        public Building_Lectern lecture
        {
            get
            {
                return this.job.GetTarget(TargetIndex.A).Thing as Building_Lectern;
            }
        }

        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return this.pawn.Reserve(this.job.targetA, this.job, this.job.def.joyMaxParticipants, 0, (ReservationLayerDef)null, true);
        }

        [DebuggerHidden]
        protected override IEnumerable<Toil> MakeNewToils()
        {
            Toil goToAltar = Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.InteractionCell);

            yield return goToAltar;

            Toil waitingTime = new Toil();
            waitingTime.defaultCompleteMode = ToilCompleteMode.Delay;
            waitingTime.defaultDuration = 740;

            yield return waitingTime;

            Toil preachingTime = new Toil();
            preachingTime.defaultCompleteMode = ToilCompleteMode.Delay;
            preachingTime.defaultDuration = 600;
            preachingTime.tickAction = delegate
            {
                Pawn actor = this.pawn;
                actor.skills.Learn(SkillDefOf.Social, 0.25f);
                actor.GainComfortFromCellIfPossible();
            };

            yield return preachingTime;
            yield break;
        }
    }
}