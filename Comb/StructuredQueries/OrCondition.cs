using System;
using System.Collections.Generic;

namespace Comb.StructuredQueries
{
    public class OrCondition : GroupCondition
    {
        readonly uint? _boost;
        readonly ICollection<Condition> _terms;

        public uint? Boost
        {
            get { return _boost; }
        }

        public override IEnumerable<Option> Options
        {
            get
            {
                if (_boost.HasValue)
                    yield return new Option("boost", _boost.ToString());
            }
        }

        public override IEnumerable<Condition> Terms
        {
            get { return _terms; }
        }

        public OrCondition(ICollection<Condition> terms, uint? boost = null)
            : base("or")
        {
            if (terms == null)
                throw new ArgumentNullException("terms");

            if (terms.Count < 2)
                throw new ArgumentOutOfRangeException("terms", "An OrCondition must have at least two terms.");

            _boost = boost;
            _terms = terms;
        }
    }
}