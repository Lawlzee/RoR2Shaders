using RoR2;
using RoR2Shaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoR2Shaders
{
    public static class Commands
    {
        [ConCommand(commandName = "toggle_stage_report", flags = ConVarFlags.None, helpText = "")]
        public static void ToggleReport(ConCommandArgs args)
        {
            //StageReportPanel.Toggle(InteractableTracker.instance.trackedInteractables);
        }
    }
}
