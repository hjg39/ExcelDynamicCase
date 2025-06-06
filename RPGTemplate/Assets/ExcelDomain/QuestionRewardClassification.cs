using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.ExcelDomain
{
    public enum QuestionRewardClassification
    {
        None = 0,

        EasyAggregates = 10,
        AdvancedAggregates = 11,
        ExpertAggregates = 12,
        DivineAggregates = 13,
         
        AdvancedComplex = 21,

        ExpertBases = 32,

        EasyLookup = 40,
        AdvancedLookup = 41,

        EasyMaths = 50,
        ExpertMaths = 52,

        EasyText = 60,
        AdvancedText = 61,

        BasicManipulation = 70,
        AdvancedManipulation = 71,

        AdvancedDates = 81,
        ExpertDates = 82,
    }
}
