﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using Comb.Tests.Support;
using NUnit.Framework;

namespace Comb.Tests.StructuredQueries
{
    public class AndConditionTests
    {
        [SetUp]
        public void SetUp()
        {
            
        }

        [Test]
        public void NullTermsThrowsException()
        {
            Assert.That(() =>
            {
                new AndCondition(null);
            },
            Throws.TypeOf<ArgumentNullException>().ForParameter("operands"));
        }

        [Test]
        public void EmptyTermsThrowsException()
        {
            Assert.That(() =>
            {
                new AndCondition(new Collection<IOperator>());
            },
            Throws.TypeOf<ArgumentOutOfRangeException>().ForParameter("operands"));
        }

        [Test]
        public void FieldIsAddedToOptions()
        {
            var condition = new AndCondition(new[] { new TestCondition("TEST") }, "testfield");
            var option = condition.Options.Single();

            Assert.That(option, Is.Not.Null);
            Assert.That(option.Name, Is.EqualTo("field"));
            Assert.That(option.Value, Is.EqualTo("testfield"));
        }

        [Test]
        public void NullFieldIsIgnored()
        {
            var condition = new AndCondition(new[] { new TestCondition("TEST") });

            Assert.That(condition.Field, Is.Null);
            Assert.That(!condition.Options.Any());
        }

        [Test]
        public void FieldIsAddedToDefinition()
        {
            var condition = new AndCondition(new[] { new TestCondition("TEST") }, "testfield");
            var definition = condition.Definition;

            Assert.That(definition, Is.EqualTo("(and field=testfield TEST)"));
        }

        [Test]
        public void BoostIsAddedToOptions()
        {
            var condition = new AndCondition(new[] { new TestCondition("TEST") }, boost: 984);
            var option = condition.Options.Single();

            Assert.That(option, Is.Not.Null);
            Assert.That(option.Name, Is.EqualTo("boost"));
            Assert.That(option.Value, Is.EqualTo("984"));
        }

        [Test]
        public void NullBoostIsIgnored()
        {
            var condition = new AndCondition(new[] { new TestCondition("TEST") });

            Assert.That(condition.Boost, Is.Null);
            Assert.That(!condition.Options.Any());
        }

        [Test]
        public void BoostIsAddedToDefinition()
        {
            var condition = new AndCondition(new[] { new TestCondition("TEST") }, boost: 8);
            var definition = condition.Definition;

            Assert.That(definition, Is.EqualTo("(and boost=8 TEST)"));
        }

        [Test]
        public void OneTermIsNotWrapped()
        {
            var condition = new AndCondition(new[] { new TestCondition("TEST") });
            var definition = condition.Definition;

            Assert.That(definition, Is.EqualTo("TEST"));
        }

        [Test]
        public void OneTermWithOptionIsWrapped()
        {
            var condition = new AndCondition(new[] { new TestCondition("TEST") }, "somefield");
            var definition = condition.Definition;

            Assert.That(definition, Is.EqualTo("(and field=somefield TEST)"));
        }

        [Test]
        public void ManyTermsAreWrapped()
        {
            var condition = new AndCondition(new[]
            {
                new TestCondition("(omg)"),
                new TestCondition("(its (a) (test))"),
                new TestCondition("ZUBB")
            });
            var definition = condition.Definition;

            Assert.That(definition, Is.EqualTo("(and (omg) (its (a) (test)) ZUBB)"));
        }
    }
}
