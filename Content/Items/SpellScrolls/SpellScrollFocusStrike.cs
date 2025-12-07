using Terrafirma.Common.Mechanics;
using Terrafirma.Common.Templates;
using Terrafirma.Content.Skills.General.FocusStrike;

namespace Terrafirma.Content.Items.SpellScrolls
{
    public class SpellScrollFocusStrike : SkillScroll
    {
        public override Skill SkillToLearn => new FocusStrike();
    }
}
