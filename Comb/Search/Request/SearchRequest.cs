﻿using System.Collections.Generic;

namespace Comb
{
    public class SearchRequest
    {
        public Query Query { get; set; }

        /// <summary>
        /// Use to filter the documents that match the search criteria specified with the <see cref="Query"/>
        /// parameter without affecting the relevance scores of the documents included in the search results.
        /// Only supports structured queries.
        /// </summary>
        public StructuredQuery Filter { get; set; }

        public uint? Start { get; set; }

        public uint? Size { get; set; }

        public ICollection<Sort> Sort { get; set; }

        public ICollection<Return> Return { get; set; }

        public ICollection<Facet> Facets { get; set; }

        public IEnumerable<IExpression> Expressions
        {
            get
            {
                // Find sort expressions
                foreach (var sort in Sort)
                {
                    var expression = sort.Field as IExpression;
                    if (expression != null)
                        yield return expression;
                }
                // Find return expressions
                foreach (var ret in Return)
                {
                    var expression = ret.Field as IExpression;
                    if (expression != null)
                        yield return expression;
                }
            }
        }

        public SearchRequest()
        {
            Sort = new List<Sort>();
            Return = new List<Return>();
            Facets = new List<Facet>();
        }
    }
}