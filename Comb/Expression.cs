﻿namespace Comb
{
    public abstract class Expression
    {
        public static readonly IdExpression Id = new IdExpression();
        public static readonly VersionExpression Version = new VersionExpression();

        public abstract string Name { get; }
    }
}